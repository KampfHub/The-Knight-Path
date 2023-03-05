using UnityEngine;

public class Spikes : Impacts
{
    [SerializeField] private float _value;
    void Start()
    {
        currentImpact._type = "Decrease";
        currentImpact._name = "Spike";
        currentImpact._value = _value;
        currentImpact._duration = 0;
    }
}
