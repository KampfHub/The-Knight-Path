using UnityEngine;

public class GOGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _obstacles;
    private GameObject[] enemiesPoints, obstaclePoints, uselessObjects;
    private bool gameShopEnabled;
    private void Start()
    {
        LoadData();
        if (gameShopEnabled) DesrtoyUselessObjects("Potion");
        if (!gameShopEnabled) DesrtoyUselessObjects("Coin");
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
    private void DesrtoyUselessObjects(string tag)
    {
        uselessObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < uselessObjects.Length; i++)
        {
            Destroy(uselessObjects[i]);
        }
    }
    private bool isNotNullOrEmptyCheck(GameObject[] go)
    {
        return go is null || go.Length == 0 ? false : true;
    }
    private void LoadData()
    {
        GameObject gameDataController = GameObject.Find("GameDataController");
        if (gameDataController is not null)
        {
            SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
            gameShopEnabled = saveData._gameShopEnable;
        }
    }
}
