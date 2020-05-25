using UnityEngine;

public class PooledSpawner : MonoBehaviour
{
    public Vector3Reference spawnPosition;
    public Vector3Reference positionOffset;
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
            spawnedObj.transform.position = spawnPosition.Value + positionOffset.Value;
        else
            spawnedObj.transform.position = transform.position + positionOffset.Value;

        prefabPool.ResetPoolObject(spawnedObj);
        spawnedObj.SetActive(true);
    }
}
