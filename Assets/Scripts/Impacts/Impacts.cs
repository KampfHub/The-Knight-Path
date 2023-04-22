using UnityEngine;

public struct Impact
{
    public string _type { get; set; }
    public string _name {get; set;}
    public float _value { get; set;}
    public float _duration { get; set;}
}

public class Impacts : MonoBehaviour
{
    protected Impact currentImpact = new Impact();
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().GetImpact(currentImpact);
            collision.gameObject.GetComponent<Character>().HealthWidgetTrigger();
            Destroy(gameObject);
        }
    }
}
