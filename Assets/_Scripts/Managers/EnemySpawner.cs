using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyData> enemyTypesList;
    [SerializeField] int minEnemySpawnDistance = 40;
    [SerializeField] int maxEnemySpawnDistance = 60; 
    private IPlayerController _player;

    private float lastSpawnTime = -10f;


    private void Awake()
    {
        if (!GameObject.Find("Player").transform.GetChild(0).gameObject.TryGetComponent(out IPlayerController player))
        {
            Debug.LogError("Enemy spawner Could not found in player");
            Destroy(this);
        }
        _player = player;

    }

    private void FixedUpdate()
    {

        EnemySpawnController();
    }

    private void EnemySpawnController()
    {
        if(Time.time - lastSpawnTime > 10)
        {
            for(int i = 0; i < 30; i++)
            {
                SpawnEnemy();
            }
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = EnemyPool.EnemyPoolSharedInstance.GetPooledObjectOrCreateIfNotAvailable(enemyTypesList[1].EnemyPrefab, enemyTypesList[1].EnemyName);
        enemy.transform.position = _player.position + GenerateRandomSpawnPoint();
        enemy.SetActive(true);
    }

    private Vector3 GenerateRandomSpawnPoint()
    {
        int randomMagnitude = Random.Range(minEnemySpawnDistance, maxEnemySpawnDistance);
        int randomAngle = Random.Range(0, 360);
        float xLocation = randomMagnitude * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float yLocation = randomMagnitude * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        return new Vector3(xLocation, 0, yLocation);
    }
}
