using UnityEngine;

public class StaticSpikes : MonoBehaviour
{
    [SerializeField] private float _damageValue;
    [SerializeField] private float _cooldown = 1;
    private Impact currentImpact;
    private void Start()
    {
        currentImpact._type = "Decrease";
        currentImpact._name = "Spike";
        currentImpact._value = _damageValue;
        currentImpact._duration = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && AdditionalCheck())
        {
            collision.gameObject.GetComponent<Character>().GetImpact(currentImpact);
            collision.gameObject.GetComponent<Character>().HealthWidgetTrigger();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("EnableSpikes", _cooldown);
        }
    }
    private bool AdditionalCheck()
    {
        if(transform.parent.GetComponentInParent<MillBlocker>() is not null)
        {
            return transform.parent.GetComponentInParent<MillBlocker>().GetMillState();
        }
        return true;
    }
    private void EnableSpikes()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
