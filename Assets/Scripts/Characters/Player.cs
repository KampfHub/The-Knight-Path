using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
public delegate void PlayerTrigger();
public delegate void LoadConteiner(int value);
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
    private bool isAttacikng, isLockController;
    private void Awake()
    {
        CheckAndSetEmptyValues();
        GUI = GameObject.Find("GUI");
        GUI.GetComponent<GeneralUI>().JumpTrigger += Jump;
        GUI.GetComponent<GeneralUI>().AttackTrigger += Attack;
        GUI.GetComponent<GeneralUI>().UploadingCoins += SetCoinsValue;
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
        if (GUI.GetComponentInChildren<MoveToRightButton>().ButtonIsHold())
            MoveTo(Vector2.right);
        if (GUI.GetComponentInChildren<MoveToLeftButton>().ButtonIsHold())
            MoveTo(Vector2.left);
    }
    public void StopMove() //crutch for ui
    {
        animator.SetBool("isWalking", false);
    }
    public void SaveGame(int level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/SaveData.dat");
        SaveData saveData = new SaveData();
        saveData.level = level;
        saveData.coins = coins;
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
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
    public void SetCoinsValue(int value)
    {
        coins = value;
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
    private bool PlayerIsAlive()
    {
        return currentHP <= 0 || isAttacikng || isLockController ? false : true;
    }
}