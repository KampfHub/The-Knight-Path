using UnityEngine;

public class Character : MonoBehaviour
{

    protected float maxHP { get; set; }
    protected float currentHP { get; set; }
    protected float maxDefence { get; set; }
    protected float currentDefence { get; set; }
    protected float attackPower { get; set; }
    protected float attackRange { get; set; }
    protected float speed { get; set; }
    protected float jumpForce { get; set; }
    protected float raycastlengthForJump { get; set; }
    protected bool immortalState { get; set; }
    private Vector2 currentDirection
    {
        get { return vector;}
        set{ if (value == null) vector = Vector2.left; vector = value;}
    }
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected const int playerLayer = 8;
    protected const int groundLayer = 64;
    protected const int environmentLayer = 128;
    protected const int enemyLayer = 256;
    protected const int deadLayer = 512; 
    private Vector2 vector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    protected void MoveTo(Vector2 direction)
    {
        currentDirection = direction;
        isWalking(true);
        if (direction == Vector2.left)
        {
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
            spriteRenderer.flipX = true;
        }
        if(direction == Vector2.right) 
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
        }
    }
    protected void isWalking(bool state)
    {
        animator.SetBool("isWalking", state);
    }
    protected void Jump()
    {
        if (RaycastCheck(Vector2.down, raycastlengthForJump, groundLayer))
        {
            animator.SetTrigger("isJumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    protected void LaunchAttack()
    {
        animator.SetTrigger("isAttacking");
    }
    protected void EndAttack()
    {
        RaycastHit2D raycast = Physics2D.Raycast(rb.position, currentDirection, attackRange, GetTargetLayer());
        if (raycast.collider is not null)
        {
            var enemyScript = raycast.collider.gameObject.GetComponent<Character>();
            if (enemyScript is not null) enemyScript.GetHit(attackPower);
        }
    }
    private int GetTargetLayer()
    {
        if (gameObject.layer == 3) return enemyLayer;
        return playerLayer;
    }
    public void GetHit(float damage)
    {
        if (immortalState == false)
        {
            float residualDamage = damage - currentDefence;
            if (currentDefence > 0 && currentDefence < damage) { currentHP -= residualDamage; currentDefence = 0; }
            if (currentDefence <= 0) currentHP -= damage; 
            if (currentDefence >= damage) currentDefence -= damage;
            animator.SetTrigger("isHurting");
            if (currentHP <= 0) Dead();
            HealthWidgetTrigger();
        }
    }
    public virtual void HealthWidgetTrigger() { }
    public virtual void EffectWidgetTrigger(string effectType, float duration) { }
    private void Dead()
    {
        gameObject.layer = 9;
        animator.SetTrigger("isDead");
    }
    protected bool RaycastCheck(Vector2 direction, float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distance, layer);

        if (hit.collider is null) return false;
            return true;
 
    }
    public void GetImpact(Impact impact)
    {
        EffectWidgetTrigger(impact._name, impact._duration);
        if (impact._type == "Boost")
        {
            switch (impact._name)           //CancelInvoke("ImpactFinished");
            {
                case "HP": currentHP += impact._value; break;
                case "Defence": currentDefence += impact._value; break;
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
                case "Trap": GetHit(impact._value); Invoke("RestoreDefaultSpeed", impact._duration); speed -= speed * (impact._value / 100); break;
                case "Poison": Invoke("RestoreDefaulAttackPower", impact._duration); attackPower *= impact._value; break;
            }
        }
    }
}
