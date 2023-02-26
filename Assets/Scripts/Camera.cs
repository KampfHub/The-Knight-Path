using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float _smooth = 5.0f;
    private GameObject _player;
    private Vector3 _cameraOffset = new Vector3(0, 2, -5);

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _cameraOffset = transform.position - _player.transform.position;
    }

    //void LateUpdate()
    //{
    //    transform.position = _player.transform.position + _cameraOffset;
    //}

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + _cameraOffset, Time.deltaTime * _smooth);
    }
}
