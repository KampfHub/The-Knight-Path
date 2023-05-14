using UnityEngine;

public class Trap : Impacts
{
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentImpact._type = "Decrease";
        currentImpact._name = "Trap";
        currentImpact._value = _value;
        currentImpact._duration = _duration;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("DestroyTrap", 1.2f);
            animator.SetTrigger("isWorked");
            collision.gameObject.GetComponent<Player>().GetImpact(currentImpact);
        }
    }
    private void DestroyTrap()
    {
        Destroy(gameObject);
    }
}