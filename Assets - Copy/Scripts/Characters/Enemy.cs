using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _sightLenght;
    private GameObject playerRef { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHP = _maxHP;
        currentHP = maxHP;
        speed = _speed;
        attackPower = _attackPower;
        attackRange = _attackRange;
    }
    void Update()
    {
        if (playerRef is not null && playerRef.layer != 9 && this.currentHP > 0)
        {
            if (RaycastCheck(GetCurrentDirection(),attackRange, playerLayer))
            {
                isWalking(false);
                LaunchAttack();
            }
            if (RaycastCheck(GetCurrentDirection(), attackRange, playerLayer) == false)
            {
                MoveTo(GetCurrentDirection());
                CheckPlayerBehind();
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") playerRef = collision.gameObject;
    }
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
}
