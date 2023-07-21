using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] RangeSettings _rangeSettings;
    [SerializeField] EnemyData _enemyData;
    private Transform _transform;
    private Rigidbody _rb;
    private int _speed;
    public int _health;
    private int _power;
    private float _attackRange;
    private Vector3 directionVector;
    private IPlayerController _playerMovement;
    private IDamageable _playerHealth;
    private SphereCollider _collider;
    private bool died = false;
    private bool isAttacking = false;
    private bool isInAttackRange = false;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        _speed = _enemyData.BaseSpeed;
        _health = _enemyData.BaseHealth;
        _power = _enemyData.AttackPower;
    }

    private void OnEnable()
    {
        died = false;
        isAttacking = false;
        isInAttackRange = false;
        _health = _enemyData.BaseHealth;
    }

    private void Start()
    {
        SetAttackRange();
    }

    private void FixedUpdate()
    {

        if (died) return;
        if (isInAttackRange && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine("Attack");
        }
        else if(!isInAttackRange)
        {
            if (isAttacking) Debug.Log("Attack stoped");
            FollowEnemy();
            SetRotation();
            isAttacking = false;
            StopAllCoroutines();
        }
    }

    private void SetRotation()
    {
        Quaternion direciton = Quaternion.LookRotation(directionVector);
        _transform.rotation = direciton;
    }

    private void FollowEnemy()
    {
        directionVector = (_playerMovement.position - _transform.position).normalized;
        _rb.velocity = directionVector * _speed;

    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        if (_health <= 0) StartCoroutine("Die");

    }

    public bool IsItAttacking() { return isAttacking; }
    public bool IsItDead() { return died; }

    private IEnumerator Die()
    {
        _rb.velocity = Vector3.zero;
        died = true;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && (_playerMovement.position - _transform.position).magnitude > _attackRange)
        {
            isInAttackRange = false;
        }
    }

    private void SetAttackRange()
    {
        _attackRange = GetRange();
        _collider.radius = _attackRange;
    }
    //Set these when creating object
    public void SetPlayerPlayerReference(IPlayerController playerMovement, IDamageable playerHealth)
    {
        _playerMovement = playerMovement;
        _playerHealth = playerHealth;
    }

    IEnumerable Attack()
    {
        while (!isInAttackRange)
        {
            _playerHealth.TakeDamage(_power);
            yield return new WaitForSeconds(_enemyData.AttackFrequency);
        }
    }

    private float GetRange()
    {
        switch (_enemyData.enemyRange)
        {
            case EnemyRange.Melee:
                return _rangeSettings.meleeRange;
            case EnemyRange.RangedClose:
                return _rangeSettings.rangedClose;
            case EnemyRange.RangedMid:
                return _rangeSettings.rangedMid;
        }
        Debug.LogError("Cannot get range info at the enmy script");
        return 0f;
    }
}
