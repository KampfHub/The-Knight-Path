using UnityEngine;

public class SpeedPotion : Impacts
{
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "Speed";
        currentImpact._value = _value;
        currentImpact._duration = _duration;
    }
}
