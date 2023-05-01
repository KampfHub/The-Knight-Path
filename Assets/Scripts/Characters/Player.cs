using UnityEngine;

public class Player : Character
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
        GUI.GetComponent<GeneralUI>().JumpTrigger += Jump;
        GUI.GetComponent<GeneralUI>().AttackTrigger += Attack;
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
            Attack();
        if (Input.GetKey(KeyCode.D))
            MoveTo(Vector2.right);
        if (Input.GetKeyUp(KeyCode.D))
            isWalking(false);
        if (Input.GetKey(KeyCode.A))
            MoveTo(Vector2.left);
        if (Input.GetKeyUp(KeyCode.A))
            isWalking(false);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (CheckMoveToRightBtnHold())
            MoveTo(Vector2.right);
        if (CheckMoveToLeftBtnHold())
            MoveTo(Vector2.left);
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
    public int GetCoinsValue()
    {
        return coins;
    }
    public void AddCoin()
    {
        coins++;
        soundsController.PlaySound("Coin");
    }
    public void SpendCoins(int value)
    {
        coins -= value;
    }

    private void SetCoinsValue(int value)
    {
        coins = value;
    }
    private void SetDifficultyValue(string value)
    {
        currentDifficulty = value;
    }
    private void Attack()
    {
        if (ActionIsAllowed() && isAttacikng == false)
        {
            LaunchAttack();
            soundsController.PlaySound("Attack");
            isAttacikng = true;
            Invoke("RestoreAttackState", _timeAttack);
        }
    }
    private void Jump()
    {
        if (ActionIsAllowed() && isJumping == false)
        {
            soundsController.PlaySound("Jump");
            animator.SetTrigger("isJumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Invoke("RestoreJumpState", _jumpCooldownTime);
        }
    }
    protected override void InThePit()
    {
        this.PlayerDead();
    }
    protected override void ActivateObstacle(string obstacleName)
    {
        soundsController.PlaySound(obstacleName);
    }
    protected override void UsePorion()
    {
        soundsController.PlaySound("Potion");
    }
    protected override void PlayerDead()
    {
        ClearLinkToPlatform();
        gameObject.SetActive(false);;
        DisableLevelMusic();
        soundsController.PlaySound("Lose", 0.5f);
        GUI.GetComponent<GeneralUI>().ShowLoseWindow();
    }
    private void RestoreAttackState()
    {
        isAttacikng = false;
        isWalking(false);
    }
    private void RestoreJumpState()
    {
        isJumping = false;
    }
    protected override void OptionalGetHit()
    {
        soundsController.PlaySound("Hit");
    }
    protected void RestoreDefaultSpeed()
    {
        speed = _speed;
    }
    protected void RestoreDefaulAttackPower()
    {
        attackPower = _attackPower;
    }
    protected void RestoreDefaulJumpForce()
    {
        jumpForce = _jumpForce;
    }
    protected void RestoreDefaulImmortalState()
    {
        immortalState = false;
    }
    public float GetFormattingHPValueToWidget()
    {
        return currentHP / maxHP;
    }
    public float GetFormattingDefenceValueToWidget()
    {
        return currentDefence / maxDefence;
    }
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
    private void DisableLevelMusic()
    {
        soundsController.MuteLevelMusic();
    }

    public void StopMove() //crutch for ui
    {
        animator.SetBool("isWalking", false);
    }
    public void LockController(bool state) //crutch for ui
    {
        isLockController = state;
    }
    public void LinkToPlatform(GameObject parent) //crutch for platform
    {
        this.transform.SetParent(parent.transform);
    }
    public void ClearLinkToPlatform()
    {
        this.transform.SetParent(null);
    }
}