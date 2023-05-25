using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    [SerializeField] float _upperBound;
    [SerializeField] float _lowerBound;
    [SerializeField] float _speed = 1;
    private bool directionYState;
    void Update()
    {
        if (gameObject.transform.position.y >= _upperBound) directionYState = false;
        if (gameObject.transform.position.y <= _lowerBound) directionYState = true;
        MovePlatform();
    }

    private void MovePlatform()
    {
        gameObject.transform.Translate(0, GetYMoveDirection(), 0);
    }
    private float GetYMoveDirection()
    {
        if (directionYState) return Time.deltaTime * _speed;
        if (directionYState == false) return -Time.deltaTime * _speed;
        return 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LinkToPlatform(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ClearLinkToPlatform();
        }
    }
}
