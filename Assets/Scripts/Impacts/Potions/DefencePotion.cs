using UnityEngine;

public class DefencePotion : Impacts
{
    [SerializeField] private float _value;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "Defence";
        currentImpact._value = _value;
        currentImpact._duration = 0;
    }
}
