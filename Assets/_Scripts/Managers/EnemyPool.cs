using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool EnemyPoolSharedInstance;
    private Transform _enemyUnitsTransform;
    private Dictionary<string, List<GameObject>> pooledObjectsDictionaryWithNameKeys;
    private IPlayerController _playeMovement;
    private IDamageable _playerHealth;

    private void Awake()
    {
        pooledObjectsDictionaryWithNameKeys = new Dictionary<string, List<GameObject>>();
        EnemyPoolSharedInstance = this;
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        _playeMovement = player.GetComponent<IPlayerController>();
        _playerHealth = player.GetComponent<IDamageable>();
        _enemyUnitsTransform = GameObject.Find("Enemy Units").transform;
    }

    public GameObject GetPooledObjectOrCreateIfNotAvailable(GameObject objectToPool, string unitName)//maybea some other thing
    {

        if (!pooledObjectsDictionaryWithNameKeys.ContainsKey(unitName))
        {
            pooledObjectsDictionaryWithNameKeys.Add(unitName, new List<GameObject>());
        }

        foreach (GameObject item in pooledObjectsDictionaryWithNameKeys[unitName])
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }

        GameObject newObj = Instantiate(objectToPool);
        newObj.GetComponent<Enemy>().SetPlayerPlayerReference(_playeMovement, _playerHealth);
        newObj.transform.SetParent(_enemyUnitsTransform);
        pooledObjectsDictionaryWithNameKeys[unitName].Add(newObj);
        return newObj;
        
    }
}
