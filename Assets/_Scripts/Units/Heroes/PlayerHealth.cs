using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealth
{
    [SerializeField] int _health;
    [SerializeField] HeroData _heroData;
    [SerializeField] int _maxHealth;
    private void Awake()
    {
        _health = _heroData.BaseHealth;
        _maxHealth = _health;
    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        if (_health <= 0) Die();

    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetHealth()
    {
        return _health;
    }

    private void Die()
    {
        //do something
        Debug.Log("Game Over");
    }
}

