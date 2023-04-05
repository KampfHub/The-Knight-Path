using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().AddCoin();
            Destroy(gameObject);
        }
    }
}
