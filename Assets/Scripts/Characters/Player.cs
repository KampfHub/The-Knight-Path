using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Unity.VisualScripting;

public delegate void PlayerTrigger();
public delegate void LoadCoinConteiner(int value);
public delegate void LoadTextConteiner(string value);
[System.Serializable]
public class Player : Character
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _maxDefence;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _timeAttack;
    [SerializeField] private float _raycastlengthForJump;
    private GameObject GUI;
    private int coins;
    private string currentDifficulty;
    private bool isAttacikng, isLockController;
    private void Awake()
    {
        CheckAndSetEmptyValues();
        GUI = GameObject.Find("GUI");
        GUI.GetComponent<GeneralUI>().JumpTrigger += Jump;
        GUI.GetComponent<GeneralUI>().AttackTrigger += Attack;
        GUI.GetComponent<GeneralUI>().UploadingCoins += SetCoinsValue;
        GUI.GetComponent<GeneralUI>().UploadingDifficultyValue += SetDifficultyValue;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
    void Update()
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
                saveData.level = level;
                saveData.coins = coins;
                saveData.difficulty = currentDifficulty;
                gameDataController.GetComponent<GameDataController>().SaveData(saveData);
            }
        }
    }
    public void LevelCompleteTrigger(int newAvailableLevel)
    {   
        isLockController = true;
        StopMove();
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
        Debug.Log($"Coins = {coins.ToString()}");
    }
    public void SpendCoins(int value)
    {
        coins -= value;
        Debug.Log($"Coins = {coins.ToString()}");
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
        if (RaycastCheck(Vector2.down, raycastlengthForJump, groundLayer))
        {
            LaunchAttack();
            isAttacikng = true;
            Invoke("RestoreAttakState", _timeAttack);
        }
    }
    private void Jump()
    {
        if (RaycastCheck(Vector2.down, raycastlengthForJump, groundLayer))
        {
            animator.SetTrigger("isJumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    protected override void InThePit()
    {
        this.PlayerDead();
    }
    protected override void PlayerDead()
    {
        ClearLinkToPlatform();
        gameObject.SetActive(false);
        GUI.GetComponent<GeneralUI>().ShowLoseWindow();
    }
    private void RestoreAttakState()
    {
        isAttacikng = false;
        isWalking(false);
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
    private bool PlayerIsAlive()
    {
        return currentHP <= 0 || isAttacikng || isLockController ? false : true;
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