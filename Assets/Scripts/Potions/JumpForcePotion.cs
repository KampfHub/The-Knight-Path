using UnityEngine;

public class JumpForcePotion : Impacts
{
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "JumpForce";
        currentImpact._value = _value;
        currentImpact._duration = _duration;
    }
}
