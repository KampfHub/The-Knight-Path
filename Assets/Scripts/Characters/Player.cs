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
    [SerializeField] private float _raycastlengthForJump;
    private GameObject _hpBarUI;
    private GameObject _defenceBarUI;
    private GameObject _HUD;
    private bool isAttacikng;
    private void Awake()
    {
        NullObjectsCheck();
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
        if (currentHP >= 0 && isAttacikng == false) InputsListener();
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
    public override void HealthWidgetTrigger()
    {
        float formattingHPValueToWidget = currentHP / maxHP;
        float formattingDefenceValueToWidget = currentDefence / maxDefence;
        if(_hpBarUI is not null) _hpBarUI.GetComponent<ProgressBar>().ÑhangeWidgetValue(formattingHPValueToWidget);
        if(_defenceBarUI is not null) _defenceBarUI.GetComponent<ProgressBar>().ÑhangeWidgetValue(formattingDefenceValueToWidget);
    }
    public override void EffectWidgetTrigger(string effectType, float duration)
    {
        if (_HUD is not null) _HUD.GetComponent<HUD>().SetIconInSlot(effectType, duration);
    }

    private void NullObjectsCheck()
    {
        if (_hpBarUI is null) _hpBarUI = GameObject.Find("HPBar");
        if (_defenceBarUI is null) _defenceBarUI = GameObject.Find("DefenseBar");
        if (_HUD is null) _HUD = GameObject.Find("HUD");
        if (_raycastlengthForJump == 0) raycastlengthForJump = 0.8f;
    }
}