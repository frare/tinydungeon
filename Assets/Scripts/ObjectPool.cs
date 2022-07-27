using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected int amount = 10;
    [SerializeField] protected bool poolAsChild = false;
    [SerializeField] protected bool dynamicSize = true;
    protected List<GameObject> pooledObjects = new List<GameObject>();



    protected virtual void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newObj = null;
            if (poolAsChild) newObj = Instantiate(prefab, transform);
            else newObj = Instantiate(prefab);
            newObj.SetActive(false);
            
            pooledObjects.Add(newObj);
        }
    }

    public virtual GameObject GetNext()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeSelf) return obj;
        }

        if (dynamicSize)
        {
            GameObject newObj = null;
            if (poolAsChild) 
            {
                newObj = Instantiate(prefab, transform);
            }
            else 
            {
                newObj = Instantiate(prefab);
            }
            
            pooledObjects.Add(newObj);
            return newObj;
        }

        return null;
    }

    public virtual List<GameObject> GetAll()
    {
        return pooledObjects;
    }

    public virtual GameObject GetByIndex(int index)
    {
        return pooledObjects[index];
    }
}