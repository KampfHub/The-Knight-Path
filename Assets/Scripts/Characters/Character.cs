using UnityEngine;

public class Character : MonoBehaviour, IAlive
{
    public float maxHP { get; set; }
    public float speed { get; set; }
    protected float currentHP { get; set; }
    protected float maxDefence { get; set; }
    protected float currentDefence { get; set; }
    protected float raycastlengthForJump { get; set; }
    protected bool immortalState { get; set; }
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
    public void MoveTo(Vector2 direction)
    {
        AnimatorController("isWalking", true);
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
    public void Die()
    {
        gameObject.layer = 9;
        AnimatorController("isDead");
        OptionalDead();
    }
    protected void Move(Vector2 direction) => MoveTo(direction);
    protected void Attack(float attackRange, float attackPower)
    {
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
    protected void EndAttackNotify() => EndAttack(); // called in Animation "Attack"
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
            if (currentHP <= 0) Die();
            AnimatorController("isHurting");
            HealthWidgetTrigger();
            OptionalGetHit();
        }
    }
    protected void AnimatorController(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    protected void AnimatorController(string triggerName, bool state)
    {
        animator.SetBool(triggerName, state);
    }
    protected virtual void OptionalDead() { } 
    protected virtual void OptionalGetHit() { }
    protected virtual void InThePit() { }
    protected virtual void UsePotion() { }
    protected virtual void ActivateObstacle(string obstacleName) { }
    protected virtual void EndAttack() { }
    public virtual void EffectWidgetTrigger(string effectType, float duration) { }
    public virtual void HealthWidgetTrigger() { }
    protected bool RaycastCheck(Vector2 direction, float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distance, layer);
        return hit.collider is null ? false : true;
    }
    protected bool CheckOnPit() => transform.position.y <= -5 ? true : false;
  
}
