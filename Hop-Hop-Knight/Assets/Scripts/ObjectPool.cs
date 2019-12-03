using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<PoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    public Transform parent;
    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach (PoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                obj.transform.SetParent(parent);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag==tag)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
