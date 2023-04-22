using UnityEngine;

public class HPRestorePotion : Impacts
{
    [SerializeField] private float _value;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "HP";
        currentImpact._value = _value;
        currentImpact._duration = 0;
    }
}
