using UnityEngine;

abstract class Character : MonoBehaviour
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
    private Vector2 currentDirection { get; set; }
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected GameObject soundsControllerRef;
    protected readonly int playerLayer = 8;
    protected readonly int groundLayer = 64;
    protected readonly int environmentLayer = 128;
    protected readonly int enemyLayer = 256;
    protected readonly int deadLayer = 512;

    private void Start()
    {
        soundsControllerRef = GameObject.Find("SoundsController");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    protected void isWalking(bool state) => animator.SetBool("isWalking", state);
    protected void LaunchAttack() => animator.SetTrigger("isAttacking");
    protected void EndAttack()
    {
        OptionalEndAttack();
        RaycastHit2D raycast = Physics2D.Raycast(rb.position, GetCurrentDirection(), attackRange, GetTargetLayer());
        if (raycast.collider is not null)
        {
            var enemyScript = raycast.collider.gameObject.GetComponent<Character>();
            if (enemyScript is not null)
            {
                enemyScript.GetHit(attackPower);
            }
        }
    }
    private Vector2 GetCurrentDirection() => spriteRenderer.flipX ? Vector2.left : Vector2.right;
    private int GetTargetLayer() => gameObject.layer == 3 ? enemyLayer : playerLayer;
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
            OptionalGetHit();
        }
    }
    
    protected abstract void OptionalDead();
    protected abstract void OptionalGetHit();
    protected abstract void InThePit();
    protected virtual void UsePotion() { }
    protected virtual void ActivateObstacle(string obstacleName) { }
    protected virtual void OptionalEndAttack() { }
    public virtual void EffectWidgetTrigger(string effectType, float duration) { }
    public virtual void HealthWidgetTrigger() { }
    protected void Dead()
    {
        gameObject.layer = 9;
        animator.SetTrigger("isDead");
        OptionalDead();
    }
    protected bool RaycastCheck(Vector2 direction, float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distance, layer);
        return hit.collider is null ? false : true;
    }
    protected bool CheckOnPit() => transform.position.y <= -5 ? true : false;
    public void GetImpact(Impact impact)
    {
        EffectWidgetTrigger(impact._name, impact._duration);
        if (impact._type == "Boost")
        {
            UsePotion();
            switch (impact._name)           
            {
                case "HP":
                    {// impact.value get in percentages 
                        currentHP += maxHP * (impact._value / 100);
                        if(currentHP > maxHP) currentHP = maxHP;
                        Debug.Log(currentHP);
                        break;
                    }   
                case "Defence":
                    {
                        currentDefence += impact._value; 
                        if(currentDefence > maxDefence) currentDefence = maxDefence;
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
}
