using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [SerializeField] float _rightBound;
    [SerializeField] float _leftBound;
    [SerializeField] float _speed = 1;
    private bool directionXState;
    void Update()
    {
        if (gameObject.transform.position.x >= _rightBound) directionXState = false;
        if (gameObject.transform.position.x <= _leftBound) directionXState = true;
        MovePlatform();
    }

    private void MovePlatform()
    {
        gameObject.transform.Translate(GetXMoveDirection(), 0, 0);
    }
    private float GetXMoveDirection()
    {
        if (directionXState) return Time.deltaTime * _speed;
        if (directionXState == false) return -Time.deltaTime * _speed;
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
