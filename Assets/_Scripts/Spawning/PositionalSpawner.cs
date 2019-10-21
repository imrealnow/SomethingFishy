using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionalSpawner : MonoBehaviour
{
    public float frequency;
    public GameObject prefab;
    public SharedVector3 spawnPosition;

    private PoolManager poolManager;
    private PrefabPool prefabPool;
    private Cooldown spawnCooldown;

    private bool isSpawning = true;

    void Start()
    {
        spawnCooldown = new Cooldown(frequency);
        poolManager = FindObjectOfType<PoolManager>();
        prefabPool = poolManager.AddPool(prefab);
    }

    void Update()
    {
        if (!spawnCooldown.IsOnCooldown && isSpawning)
        {
            GameObject spawnedObj = prefabPool.GetUnusedObject();
            if(spawnPosition != null)
                spawnedObj.transform.position = spawnPosition.Value;
            else
                spawnedObj.transform.position = transform.position;
            prefabPool.ResetPoolObject(spawnedObj);
            spawnedObj.SetActive(true);
            spawnCooldown.IsOnCooldown = true;
        }
    }

    public void SetSpawning(bool shouldSpawn)
    {
        isSpawning = shouldSpawn;
    }
}
