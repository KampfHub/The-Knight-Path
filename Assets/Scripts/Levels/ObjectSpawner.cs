using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _GO;
    [SerializeField] private float _axisX;
    [SerializeField] private float _axisY;
    private GameObject spawnedObject;
    void Start()
    {
        if (GONullCheck(_GO)) spawnedObject = _GO;
    }

    public void RockSpawn()
    {
        SpawnObject();
    }
    public void DestroyAndSpawn()
    {
        Destroy(gameObject);
        SpawnObject();
    }
    private void SpawnObject()
    {
        if (GONullCheck(spawnedObject))
        {
            Vector3 spawnPoint = new Vector3(_axisX, _axisY, 0);
            Instantiate(spawnedObject, spawnPoint, Quaternion.identity);
        }
    }
    private bool GONullCheck(GameObject go)
    {
        return go is not null ?  true : false;
    }
}
