using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PrefabPool
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolAmount;
    [SerializeField]
    private bool canExpand;

    private GameObject _parentObject;
    private PoolManager _poolManager;

    private List<GameObject> poolList = new List<GameObject>();
    private List<IPoolable> resetableComponents = new List<IPoolable>();

    public List<GameObject> PoolList
    {
        get { return poolList; }
    }

    public PrefabPool(GameObject prefabToPool, int poolSize, PoolManager poolManager, bool expandable = false, GameObject parentObject = null)
    {
        _poolManager = poolManager;

        if (parentObject == null)
        {
            _parentObject = new GameObject(prefabToPool.name + "Pool");
            _parentObject.transform.SetParent(_poolManager.transform);
        }
        else
            _parentObject = parentObject;

        prefab = prefabToPool;
        poolAmount = poolSize;
        canExpand = expandable;
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Object.Instantiate(prefabToPool);
            obj.transform.SetParent(_parentObject.transform);
            PoolObject objectNamer = obj.GetComponent<PoolObject>();
            if (objectNamer == null)
                objectNamer = obj.AddComponent<PoolObject>();
            objectNamer.prefabPool = this;
            objectNamer.OriginalName = prefab.name;
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetUnusedObject()
    {
        foreach (var obj in poolList) // look for inactive objects
        {
            if (!obj.activeInHierarchy) 
            {
                ResetPoolObject(obj);
                obj.SetActive(true);
                return obj; // give the gameobject to the method that called it
            }
        }
        if (canExpand)
        {
            poolAmount++;
            GameObject obj = Object.Instantiate(prefab);
            obj.transform.SetParent(_parentObject.transform);
            obj.SetActive(false);
            resetableComponents = obj.GetComponents<IPoolable>().ToList();
            if (resetableComponents.Count > 0)
            {
                foreach (IPoolable resetableComponent in resetableComponents)
                {
                    resetableComponent.Reuse();
                }
            }
            poolList.Add(obj);
            PoolObject objectNamer = obj.GetComponent<PoolObject>();
            if(objectNamer == null)
                objectNamer = obj.AddComponent<PoolObject>();
            objectNamer.prefabPool = this;
            objectNamer.OriginalName = prefab.name;
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ShrinkPool(int byAmount = -1)
    {
        foreach (GameObject obj in poolList)
        {
            if (!obj.activeInHierarchy)
            {
                if (byAmount < 0) // -1 just means remove all unused objects
                {
                    poolList.Remove(obj);
                }
                else if (byAmount > 0)
                {
                    byAmount--;
                    poolList.Remove(obj);
                }
                else if (byAmount == 0)
                    return;
            }
        }
    }

    public void PutBackInPool(GameObject pooledObject)
    {
        pooledObject.transform.SetParent(_parentObject.transform);
        pooledObject.SetActive(false);
    }

    public void ResetPool()
    {
        foreach(GameObject repooledObject in poolList)
        {
            PutBackInPool(repooledObject);
        }
    }

    public void ResetPoolObject(GameObject obj)
    {
        resetableComponents = obj.GetComponents<IPoolable>().ToList(); // get a list of all the components that need to be reinitialised
        if (resetableComponents.Count > 0)
        {
            foreach (IPoolable resetableComponent in resetableComponents)
            {
                resetableComponent.Reuse(); // reset those components
            }
        }
    }
}

