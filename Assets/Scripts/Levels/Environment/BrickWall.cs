using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private int quantity;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 position = collision.gameObject.transform.position;
            for (int i = 0; i < quantity; i++)
            {
                Instantiate(_brickPrefab,position ,Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
