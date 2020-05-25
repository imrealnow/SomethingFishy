using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public Bounds bounds;
    public Collider2D spawnAvoider;
    public GameObject objectToEmit;
    public float frequency;

    public ScrollManager scrollManager;
    private PoolManager poolManager;
    private PrefabPool prefabPool;

    private float startSpeedPercentage;
    private bool isSpawning = true;
    private Coroutine spawner;

    void Awake()
    {
        startSpeedPercentage = scrollManager.GetSpeedPercentage();
        bounds.center = transform.position;
        poolManager = FindObjectOfType<PoolManager>();
        prefabPool = poolManager.AddPool(objectToEmit);
    }

    private void OnEnable()
    {
        spawner = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        StopCoroutine(spawner);
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(frequency * Mathf.Min((startSpeedPercentage / scrollManager.GetSpeedPercentage()), 2f));
        while (isSpawning)
        {
            bounds.center = transform.position;
            GameObject spawnedObj = prefabPool.GetUnusedObject();
            Vector2 spawnPosition = GetSpawnPosition();
            if (spawnAvoider != null)
            {
                while (spawnAvoider.OverlapPoint(spawnPosition))
                {
                    spawnPosition = GetSpawnPosition();
                }
            }
            spawnedObj.transform.position = spawnPosition;

            prefabPool.ResetPoolObject(spawnedObj);
            spawnedObj.SetActive(true);

            // scale spawn frequency to scroll speed
            yield return new WaitForSeconds(frequency * Mathf.Min((startSpeedPercentage / scrollManager.GetSpeedPercentage()), 2f));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bounds.extents * 2);
    }

    private Vector2 GetSpawnPosition()
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

    public void SetSpawning(bool shouldSpawn)
    {
        if (shouldSpawn)
            spawner = StartCoroutine(SpawnLoop());
        else
            StopCoroutine(spawner);

        isSpawning = shouldSpawn;
    }
}
