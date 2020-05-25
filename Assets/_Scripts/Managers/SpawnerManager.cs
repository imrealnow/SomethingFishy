using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public RunningSet spawnerSet;

    public void SetAllSpawnersSpawning(bool shouldSpawn)
    {
        foreach(GameObject spawnerObj in spawnerSet.GetSet())
        {
            PropSpawner spawnerComponent = spawnerObj.GetComponent<PropSpawner>();
            spawnerComponent.SetSpawning(shouldSpawn);
        }
    }
    
}
