using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEmitter : MonoBehaviour
{

    public Bounds bounds;
    public Collider2D spawnAvoider;
    public GameObject objectToEmit;
    public float frequency;

    private Cooldown spawnCooldown;
    private PoolManager poolManager;
    private PrefabPool prefabPool;

    private bool isSpawning = true;

    void Start()
    {
        spawnCooldown = new Cooldown(frequency);
        bounds.center = transform.position;
        poolManager = FindObjectOfType<PoolManager>();
        prefabPool = poolManager.AddPool(objectToEmit);
    }

    void Update()
    {
        bounds.center = transform.position;
        if (!spawnCooldown.IsOnCooldown && isSpawning)
        {
            GameObject spawnedObj = prefabPool.GetUnusedObject();
            Vector2 spawnPosition = GetSpawnPosition();
            if(spawnAvoider != null)
            {
                while(spawnAvoider.OverlapPoint(spawnPosition))
                {
                    spawnPosition = GetSpawnPosition();
                }
            }
            spawnedObj.transform.position = spawnPosition;

            prefabPool.ResetPoolObject(spawnedObj);
            spawnedObj.SetActive(true);
            spawnCooldown.IsOnCooldown = true;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bounds.extents*2);
    }

    private Vector2 GetSpawnPosition()
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

    public void SetSpawning(bool shouldSpawn)
    {
        isSpawning = shouldSpawn;
    }
}
