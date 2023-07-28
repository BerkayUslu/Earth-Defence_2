using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPool : MonoBehaviour
{
    public static AttackPool attackPoolSharedInstance;
    private Transform _playerAttacksTransform;
    private Dictionary<string, List<GameObject>> pooledObjectsDictionaryWithNameKeys;

    private void Awake()
    {
        pooledObjectsDictionaryWithNameKeys = new Dictionary<string, List<GameObject>>();
        attackPoolSharedInstance = this;
    }

    private void Start()
    {
        _playerAttacksTransform = GameObject.Find("Player Attacks").transform;
    }

    public GameObject GetPooledObjectOrCreateIfNotAvailable(GameObject objectToPool, string attackName)//maybea some other thing
    {

        if (!pooledObjectsDictionaryWithNameKeys.ContainsKey(attackName))
        {
            pooledObjectsDictionaryWithNameKeys.Add(attackName, new List<GameObject>());
        }

        foreach (GameObject item in pooledObjectsDictionaryWithNameKeys[attackName])
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }

        GameObject newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        newObj.transform.SetParent(_playerAttacksTransform);
        pooledObjectsDictionaryWithNameKeys[attackName].Add(newObj);
        return newObj;

    }
}
