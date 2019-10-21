using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    private int defaultPoolSize = 20;
    [SerializeField]
    private bool expandableByDefault = true;

    private Dictionary<GameObject, PrefabPool> _poolDictionary = new Dictionary<GameObject, PrefabPool>();

    public PrefabPool AddPool(GameObject objectIdentifier, PrefabPool pool)
    {
        _poolDictionary.Add(objectIdentifier, pool);
        return pool;
    }

    public PrefabPool AddPool(GameObject prefab)
    {
        PrefabPool poolToAdd = new PrefabPool(prefab, defaultPoolSize, this, expandableByDefault);
        _poolDictionary.Add(prefab, poolToAdd);
        return poolToAdd;
    }

    public PrefabPool GetPool(GameObject objectIdentifier)
    {
        PrefabPool pool;
        if (_poolDictionary.TryGetValue(objectIdentifier, out pool))
        {
            return pool;
        }
        else
            return null;
    }

    public void RemovePool(GameObject objectIdentifier)
    {
        _poolDictionary.Remove(objectIdentifier);
    }

    public void ClearPools()
    {
        _poolDictionary.Clear();
    }
}
