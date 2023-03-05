using UnityEngine;

public class GOGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    GameObject[] enemiesPoints;
    private void Start()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        enemiesPoints = GameObject.FindGameObjectsWithTag("EmptyEnemy");
        for(int i = 0; i < enemiesPoints.Length; i++)
        {
            Vector3 position = enemiesPoints[i].transform.position;
            Destroy(enemiesPoints[i]);
            Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);
        }
    }
}
