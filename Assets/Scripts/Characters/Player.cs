using UnityEngine;

class Player : Character, IPlayable, ICombat
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _maxDefence;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _timeAttack;
    [SerializeField] private float _jumpCooldownTime;
    [SerializeField] private float _raycastlengthForJump;
    public float jumpForce { get; set; }
    public float attackPower { get; set; }
    public float attackRange { get; set; }
    private GameObject GUI;
    private int coins;
    private SoundsController soundsController;
    private string currentDifficulty;
    private SaveData dataBuffer;
    private bool isAttacikng, isJumping, isLockController;
    private void Awake()
    {
        CheckAndSetEmptyValues();
        soundsControllerRef = GameObject.Find("SoundsController");
        GUI = GameObject.Find("GUI");
        GUI.GetComponentInChildren<JumpPressed>().JumpTrigger += Jump;
        GUI.GetComponent<GeneralUI>().AttackTrigger += LaunchAttack;
        GUI.GetComponent<GeneralUI>().UploadingCoins += SetCoinsValue;
        GUI.GetComponent<GeneralUI>().UploadingDifficultyValue += SetDifficultyValue;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundsController = soundsControllerRef.GetComponent<SoundsController>();
        maxHP = _maxHP;
        currentHP = maxHP;
        maxDefence = _maxDefence;
        currentDefence = 0;
        attackPower = _attackPower;
        attackRange = _attackRange;
        speed = _speed;
        jumpForce = _jumpForce;
        raycastlengthForJump = _raycastlengthForJump;
        immortalState = false;
        HealthWidgetTrigger();
        LoadDataInBuffer();
    }
    private void Update()
    {
        if (PlayerIsAlive()) InputsListener();
        if (CheckOnPit()) InThePit();
    }
    private void InputsListener()
    {
        if (Input.GetKeyDown(KeyCode.E))
            LaunchAttack();
        if (Input.GetKey(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKeyUp(KeyCode.D))
            AnimatorController("isWalking",false);
        if (Input.GetKey(KeyCode.A))
            Move(Vector2.left);
        if (Input.GetKeyUp(KeyCode.A))
            AnimatorController("isWalking", false);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (CheckMoveToRightBtnHold())
            Move(Vector2.right);
        if (CheckMoveToLeftBtnHold())
            Move(Vector2.left);
    }

    public void SaveGame(int level)
    {
        GameObject gameDataController = GameObject.Find("GameDataController");
        if (gameDataController is not null)
        {
            if (level > GUI.GetComponent<GeneralUI>().GetAvailableLevel())
            {
                SaveData saveData = new SaveData();
                saveData._level = level;
                saveData._coins = coins;
                saveData._difficulty = currentDifficulty;
                saveData._language = dataBuffer._language;
                saveData._soundsEnable = dataBuffer._soundsEnable;
                saveData._gameShopEnable = dataBuffer._gameShopEnable;
                gameDataController.GetComponent<GameDataController>().SaveData(saveData);
            }
        }
    }
    public void LevelCompleteTrigger(int newAvailableLevel)
    {   
        isLockController = true;
        StopMove();
        soundsController.PlaySound("Win", 0.5f);
        DisableLevelMusic();
        GUI.GetComponent<GeneralUI>().AvailableLevelUpgrade(newAvailableLevel);
        GUI.GetComponent<GeneralUI>().ShowWinWindow(); 
    }
    public void AddCoin()
    {
        coins++;
        soundsController.PlaySound("Coin");
    }
    public int GetCoinsValue() => coins;
    public void SpendCoins(int value) => coins -= value;
    private void SetCoinsValue(int value) => coins = value;
    private void SetDifficultyValue(string value) => currentDifficulty = value;
    public void LaunchAttack()
    {
        AnimatorController("isAttacking");
        if (ActionIsAllowed() && isAttacikng == false)
        {
            soundsController.PlaySound("Attack");
            isAttacikng = true;
            Invoke("RestoreAttackState", _timeAttack);
        }
    }
    protected override void EndAttack()
    {
        Attack(attackRange, attackPower);
    }
    public void Jump()
    {
        if (ActionIsAllowed() && isJumping == false)
        {
            soundsController.PlaySound("Jump");
            animator.SetTrigger("isJumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Invoke("RestoreJumpState", _jumpCooldownTime);
        }
    }
    public void GetImpact(Impact impact)
    {
        EffectWidgetTrigger(impact._name, impact._duration);
        HealthWidgetTrigger();
        if (impact._type == "Boost")
        {
            UsePotion();
            switch (impact._name)
            {
                case "HP":
                    {// impact.value get in percentages 
                        currentHP += maxHP * (impact._value / 100);
                        if (currentHP > maxHP) currentHP = maxHP;
                        Debug.Log(currentHP);
                        break;
                    }
                case "Defence":
                    {
                        currentDefence += impact._value;
                        if (currentDefence > maxDefence) currentDefence = maxDefence;
                        break;
                    }
                case "Power": Invoke("RestoreDefaulAttackPower", impact._duration); attackPower *= impact._value; break;
                case "Speed": Invoke("RestoreDefaultSpeed", impact._duration); speed *= impact._value; break;
                case "JumpForce": Invoke("RestoreDefaulJumpForce", impact._duration); jumpForce *= impact._value; break;
                case "Immortal": Invoke("RestoreDefaulImmortalState", impact._duration); immortalState = true; break;
            }
        }
        if (impact._type == "Decrease")
        {
            switch (impact._name)
            {
                case "Spike": GetHit(impact._value); break;
                case "Trap":
                    {
                        GetHit(impact._value); Invoke("RestoreDefaultSpeed", impact._duration);
                        speed -= speed * (impact._value / 100);
                        ActivateObstacle("BearTrap");
                        break;
                    }

                case "Poison":
                    {
                        Invoke("RestoreDefaulAttackPower", impact._duration);
                        attackPower *= impact._value;
                        ActivateObstacle("Poison");
                        break;
                    }
            }
        }
    }
    protected override void InThePit() => this.OptionalDead();
    protected override void ActivateObstacle(string obstacleName) => soundsController.PlaySound(obstacleName);
    protected override void UsePotion() => soundsController.PlaySound("Potion");
    protected override void OptionalGetHit() => soundsController.PlaySound("Hit");
    protected override void OptionalDead()
    {
        soundsController.PlaySound("Lose", 0.5f);
        ClearLinkToPlatform();
        gameObject.SetActive(false);;
        DisableLevelMusic();
        GUI.GetComponent<GeneralUI>().ShowLoseWindow();
    }
    private void RestoreAttackState()
    {
        isAttacikng = false;
        AnimatorController("isWalking", false);
    }
    private void RestoreJumpState() => isJumping = false;
   
    protected void RestoreDefaultSpeed() => speed = _speed;

    protected void RestoreDefaulAttackPower() => attackPower = _attackPower;

    protected void RestoreDefaulJumpForce() => jumpForce = _jumpForce;
    protected void RestoreDefaulImmortalState() => immortalState = false;
    public float GetFormattingHPValueToWidget() => currentHP / maxHP;
    public float GetFormattingDefenceValueToWidget() => currentDefence / maxDefence;

    public override void HealthWidgetTrigger()
    {
        if (GUI is not null) GUI.GetComponent<HUD>().ÑhangeWidgetValue();
    }
    public override void EffectWidgetTrigger(string effectType, float duration)
    {
        if (GUI is not null) GUI.GetComponent<HUD>().SetIconInSlot(effectType, duration);
    }
    private void CheckAndSetEmptyValues()
    {
        if (_raycastlengthForJump == 0) raycastlengthForJump = 0.8f;
    }
    private bool CheckMoveToRightBtnHold()
    {
        if (GUI.GetComponentInChildren<MoveToRightButton>() is not null)
        {
            return GUI.GetComponentInChildren<MoveToRightButton>().ButtonIsHold();
        }
        else return false;
    }
    private bool CheckMoveToLeftBtnHold()
    {
        if (GUI.GetComponentInChildren<MoveToLeftButton>() is not null)
        {
            return GUI.GetComponentInChildren<MoveToLeftButton>().ButtonIsHold();
        }
        else return false;
    }
    private bool ActionIsAllowed()
    {
        return RaycastCheck(Vector2.down, raycastlengthForJump, groundLayer) 
            || RaycastCheck(Vector2.down, raycastlengthForJump, environmentLayer) 
            ?  true : false;
    }
    private bool PlayerIsAlive()
    {
        return currentHP <= 0 || isAttacikng || isLockController ? false : true;
    }
    private void LoadDataInBuffer()
    {
        if (GameObject.Find("GameDataController") is not null)
        {
            GameObject gameDataController = GameObject.Find("GameDataController");
            if (gameDataController is not null)
            {
                SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
                dataBuffer._soundsEnable = saveData._soundsEnable;
                dataBuffer._gameShopEnable = saveData._gameShopEnable;
                dataBuffer._language = saveData._language;
            }
        }
    }
    private void DisableLevelMusic() => soundsController.MuteLevelMusic();
    public void StopMove() => animator.SetBool("isWalking", false);
    public void LockController(bool state) => isLockController = state;
    public void LinkToPlatform(GameObject parent) => this.transform.SetParent(parent.transform);
    public void ClearLinkToPlatform() => this.transform.SetParent(null);
}