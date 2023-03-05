using UnityEngine;

public class ImmortalPotion : Impacts
{
    [SerializeField] private float _duration;
    void Start()
    {
        currentImpact._type = "Boost";
        currentImpact._name = "Immortal";
        currentImpact._value = 0;
        currentImpact._duration = _duration;
    }
}
