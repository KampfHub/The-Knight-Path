using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float _knockoutTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Dragon>().StoneFellOnHead(_knockoutTime);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground555")
        {
            Destroy(gameObject);
        }
    }
}
