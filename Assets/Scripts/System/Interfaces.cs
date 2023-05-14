using UnityEngine;

interface IAlive
{
    float speed { get; set; }
    float maxHP { get; set; }
    void MoveTo(Vector2 direction);
    void Die();
}

interface ICombat
{
    float attackPower { get; set; }
    float attackRange { get; set; }
    void LaunchAttack();
    void GetHit(float damageValue);

}

interface IPlayable
{
    float jumpForce { get; set; }
    void Jump();
    void GetImpact(Impact impact);
}