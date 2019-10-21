using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseObject : MonoBehaviour
{
    public SharedGameObjectList pauseObjects;

    void Awake()
    {
        pauseObjects.Value.Add(gameObject);
    }

    void OnDestroy()
    {
        pauseObjects.Value.Remove(gameObject);
    }
}
