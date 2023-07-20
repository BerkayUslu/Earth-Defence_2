using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPool : MonoBehaviour
{
    public static GroundPool groundPoolSharedInstance;
    public List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int pooledAmount;

    private void Awake()
    {
        groundPoolSharedInstance = this;
    }

    //If there is any unused object it will return it
    public GameObject GetPooledObjectOrCreateIfNotAvailable()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // If no inactive objects are found, instantiate a new one and add it to the pool
        GameObject newObj = Instantiate(objectToPool);
        pooledObjects.Add(newObj);
        pooledAmount++;
        return newObj;
    }

}