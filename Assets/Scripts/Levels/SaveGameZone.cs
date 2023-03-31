using UnityEngine;

public class SaveGameZone : MonoBehaviour
{
    [SerializeField] private int _level;
    private int level = 1;
    void Start()
    {
        if(_level != 0) level = _level;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().SaveGame(level);
            collision.gameObject.GetComponent<Player>().LevelCompleteTrigger(level);
            Debug.Log("Saved zone!");
        }
    }
}
