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
    private int _health;
    private int _power;
    private float _attackRange;
    private Vector3 directionVector;
    private IPlayerController _playerMovement;
    private IDamageable _playerHealth;
    private IExperience _playerExperience;
    private SphereCollider _collider;
    private CapsuleCollider _bodyCollider;
    private bool died = false;
    private bool isAttacking = false;
    private bool isInAttackRange = false;
 

    private void Awake()
    {
        _bodyCollider = GetComponentInChildren<CapsuleCollider>();
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        _speed = _enemyData.BaseSpeed;
        _health = _enemyData.BaseHealth;
        _power = _enemyData.AttackPower;
    }

    private void OnEnable()
    {
        _bodyCollider.enabled = true;
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
        _playerExperience.GainExperience(_enemyData.ExperiencePoints);
        _bodyCollider.enabled = false;
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
    public void SetPlayerPlayerReference(IPlayerController playerMovement, IDamageable playerHealth, IExperience playerExperience)
    {
        _playerMovement = playerMovement;
        _playerHealth = playerHealth;
        _playerExperience = playerExperience;
    }

    private IEnumerator Attack()
    {
        while (isInAttackRange)
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
