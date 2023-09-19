using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour, IEnemy
{
    private IDamageable _playerHealth;
    private EnemyData _enemyData;

    public IEnumerator Attack()
    {
        while (true)
        {
            _playerHealth.TakeDamage(_enemyData.AttackPower);
            yield return new WaitForSeconds(_enemyData.AttackFrequency);
        }
    }

    public void SetReferences(IDamageable playerHealth, EnemyData enemyData)
    {
        _playerHealth = playerHealth;
        _enemyData = enemyData;
    }

    public void StartAttack()
    {
        StartCoroutine("Attack");
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
