using System.Collections;
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
    //public List<GameObject> pooledObjects;

    public List<ObjectPoolItem> itemsToPool;

    public Dictionary<GameObject, List<GameObject>> pooledObjects;

	void Awake () {
        SharedInstance = this;
	}

    public void Start()
    {
        pooledObjects = new Dictionary<GameObject, List<GameObject>>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            pooledObjects[item.objectToPool] = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects[item.objectToPool].Add(obj);
            }
        }        
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        for (int i = 0; i < pooledObjects[prefab].Count; i++)
        {
            if(!pooledObjects[prefab][i].activeInHierarchy)
            {
                return pooledObjects[prefab][i];
            }
        }

        foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.gameObject == prefab)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects[prefab].Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}
