using UnityEngine;

public class GOGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _obstacles;
    private GameObject[] enemiesPoints;
    private GameObject[] obstaclePoints;
    private void Start()
    {
        if (isNotNullOrEmptyCheck(_enemies)) SwapObjects("EmptyEnemy", enemiesPoints, _enemies);
        if (isNotNullOrEmptyCheck(_obstacles)) SwapObjects("EmptyObstacle", obstaclePoints, _obstacles);
    }
    private void SwapObjects(string tagReplaceableObjects, GameObject[] originalObjects, GameObject[] newObjects)
    {
        originalObjects = GameObject.FindGameObjectsWithTag(tagReplaceableObjects);
        for(int i = 0; i < originalObjects.Length; i++)
        {
            Vector3 position = originalObjects[i].transform.position;
            Quaternion rotation = originalObjects[i].transform.rotation;
            Destroy(originalObjects[i]);
            Instantiate(newObjects[Random.Range(0, newObjects.Length)], position, rotation);
        }
    }
    private bool isNotNullOrEmptyCheck(GameObject[] go)
    {
        return go is null || go.Length == 0 ? false : true;
    }
}
