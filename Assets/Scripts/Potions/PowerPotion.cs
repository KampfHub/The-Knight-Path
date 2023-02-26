using UnityEngine;

public class PowerPotion : Impacts
{
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "Power";
        currentImpact._value = _value;
        currentImpact._duration = _duration;
    }
}
