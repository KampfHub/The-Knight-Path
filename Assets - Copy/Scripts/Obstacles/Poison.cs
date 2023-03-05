using UnityEngine;

public class Poison : Impacts
{
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    void Start()
    {
        currentImpact._type = "Decrease";
        currentImpact._name = "Poison";
        currentImpact._value = _value;
        currentImpact._duration = _duration;
    }
}
