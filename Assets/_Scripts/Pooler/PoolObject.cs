using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {
    public string OriginalName { set { _originalName = value; gameObject.name = value; } }
    public PrefabPool prefabPool;
    private string _originalName;
    
    void OnDisable()
    {
        gameObject.name = _originalName + "(Not used)";
    }

    void OnEnable()
    {
        gameObject.name = _originalName + "(Used)";
    }

    public void ReturnToPool()
    {
        prefabPool.PutBackInPool(gameObject);
    }
}
