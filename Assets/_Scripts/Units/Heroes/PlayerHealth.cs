using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int _health;
    [SerializeField] HeroData _heroData;
    private void Awake()
    {
        _health = _heroData.BaseHealth;
    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        if (_health <= 0) Die();

    }

    private void Die()
    {
        //do something
        Debug.Log("Game Over");
    }
}

