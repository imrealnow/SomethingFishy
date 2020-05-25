using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionalSpawner : MonoBehaviour
{
    public float frequency;
    public GameObject prefab;
    public Vector3Reference spawnPosition;

    private PoolManager poolManager;
    private PrefabPool prefabPool;
    private Cooldown spawnCooldown = new Cooldown();

    private bool isSpawning = true;

    void Start()
    {
        spawnCooldown.ChangeDuration(frequency);
        poolManager = FindObjectOfType<PoolManager>();
        prefabPool = poolManager.AddPool(prefab);
    }

    void Update()
    {
        if (isSpawning)
        {
            if (spawnCooldown.TryUseCooldown())
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
    }

    public void SetSpawning(bool shouldSpawn)
    {
        isSpawning = shouldSpawn;
    }
}
