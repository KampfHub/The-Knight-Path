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
            collision.gameObject.GetComponent<Player>().GetImpact(currentImpact);
            Destroy(gameObject);
        }
    }
    public Impact GetDefaultPotionImpact(string buttonName)
    {
        Impact impact = new Impact();
        switch (buttonName)
        {
            case "btnPotion_1":
                {
                    impact._type = "Boost";
                    impact._name = "Power";
                    impact._value = 1.5f;
                    impact._duration = 10f;
                    return impact;
                }
            case "btnPotion_2":
                {
                    impact._type = "Boost";
                    impact._name = "Speed";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    return impact;
                }
            case "btnPotion_3":
                {
                    impact._type = "Boost";
                    impact._name = "JumpForce";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    return impact;
                }
            case "btnPotion_4":
                {
                    impact._type = "Boost";
                    impact._name = "HP";
                    impact._value = 33.3f;
                    impact._duration = 0f;
                    return impact;
                }
            case "btnPotion_5":
                {
                    impact._type = "Boost";
                    impact._name = "Defence";
                    impact._value = 30f;
                    impact._duration = 0f;
                    return impact;
                }
            case "btnPotion_6":
                {
                    impact._type = "Boost";
                    impact._name = "Immortal";
                    impact._value = 0f;
                    impact._duration = 10f;
                    return impact;
                }
            default: return impact;
        }
    }
}
