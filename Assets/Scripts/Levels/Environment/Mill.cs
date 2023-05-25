using UnityEngine;

public class Mill : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _clockwise;
    private void Update()
    {
        gameObject.transform.Rotate(0,0,GetRotation() * _speed);
    }
    private float GetRotation()
    {
        if(_clockwise) return -Time.deltaTime;
        return Time.deltaTime;
    }
}
