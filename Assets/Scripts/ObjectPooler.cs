﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}
public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;

    //Could be made faster with separate List For every type of pooled object
    public List<GameObject> pooledObjects;

    public List<ObjectPoolItem> itemsToPool;

	void Awake () {
        SharedInstance = this;
	}

    public void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }        
    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name.Contains(name))
            {
                return pooledObjects[i];
            }
        }
        foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.name.Contains(name))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
