using UnityEngine;

public class PooledSpawner : MonoBehaviour
{
    public SharedVector3 spawnPosition;
    public GameObject prefab;

    private PoolManager poolManager;
    private PrefabPool prefabPool;

    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        prefabPool = poolManager.AddPool(prefab);
    }

    public void InstantiatePrefab()
    {
        GameObject spawnedObj = prefabPool.GetUnusedObject();

        if (spawnPosition != null)
            spawnedObj.transform.position = spawnPosition.Value;
        else
            spawnedObj.transform.position = transform.position;
        prefabPool.ResetPoolObject(spawnedObj);
        spawnedObj.SetActive(true);
    }
}
