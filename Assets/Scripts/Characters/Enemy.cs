using Unity.VisualScripting;
using UnityEngine;
public delegate void EnemyTrigger();
class Enemy : Character
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _sightLenght;
    private bool isAttackCooldown;
    private SoundsController soundsController;
    private GameObject playerRef { get; set; }
    public event EnemyTrigger EnemyDead, EnemyHit;
    
    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
        soundsControllerRef = GameObject.Find("SoundsController");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundsController = soundsControllerRef.GetComponent<SoundsController>();
        maxHP = _maxHP;
        currentHP = maxHP;
        speed = _speed;
        attackPower = _attackPower;
        attackRange = _attackRange;
        isAttackCooldown = true;
        RotationCorrection();
    }
    private void Update()
    {
        if (playerRef is not null && playerRef.layer != 9 && this.currentHP > 0)
        {   
            if (RaycastCheck(Vector2.left, _sightLenght, playerLayer) || RaycastCheck(Vector2.right, _sightLenght, playerLayer))
            {
                if (RaycastCheck(GetCurrentDirection(), attackRange, playerLayer) == false)
                {
                    MoveTo(GetCurrentDirection());
                    CheckPlayerBehind();
                }
                if (RaycastCheck(GetCurrentDirection(),attackRange, playerLayer))
                {
                    isWalking(false);
                    Attack();
                }
            }
            else TargetLoss();
        }
        if (CheckOnPit()) InThePit();
    }
    protected override void InThePit()
    {
        Destroy(gameObject);
    }
    private void Attack()
    {
        if (isAttackCooldown) 
        {
            LaunchAttack();
            isAttackCooldown= false; 
        }
    }
    private void RestoreCooldown()
    {
        isAttackCooldown = true;
    }
    protected override void OptionalEndAttack() 
    {
        Invoke("RestoreCooldown", _attackCooldownTime);
    }
    protected override void OptionalDead()
    {
        if(EnemyDead is not null) EnemyDead();
    }
    protected override void OptionalGetHit() 
    { 
        if(EnemyHit is not null) EnemyHit();
        soundsController.PlaySound("Hit", 0.7f);
    }
    private void TargetLoss() => isWalking(false);    
    private Vector2 GetCurrentDirection()
    {
        if (spriteRenderer.flipX) return Vector2.left;
        return Vector2.right;
    }
    private void CheckPlayerBehind()
    {
        if(spriteRenderer.flipX == false)
        {
            if (RaycastCheck(Vector2.left, _sightLenght, playerLayer)) spriteRenderer.flipX = true;
        }
        if(spriteRenderer.flipX == true)
        {
            if (RaycastCheck(Vector2.right, _sightLenght, playerLayer)) spriteRenderer.flipX = false;
        }
    }
    private void RotationCorrection()
    {

        if(transform.rotation.y != 0)
        {
            transform.Rotate(0, -180, 0);
            spriteRenderer.flipX = false;
        }
    }
    public float GetHPRatio() => currentHP / maxHP;
}
