using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : MonoBehaviour, ISkillComponent
{
    private float triggerSphereCastTime;
    private bool attackCasted = false;
    private Transform _transform;
    private Skill _skillCOnfig;
    private float _lifeTime = 1.5f;
    private float _deathTime;
    private ParticleSystem.ShapeModule _particleShape;
    private LayerMask enemyLayerMask;
    private Vector3 center;
    private LineRenderer _line;



    private void Awake()
    {
        _particleShape = GetComponent<ParticleSystem>().shape;
        _line = GetComponent<LineRenderer>();
        _transform = transform;
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    public void SetPosition(Vector3 playerPosition)
    {
        _transform.position = playerPosition;
    }


    public void SetSkillConfig(Skill skill) { _skillCOnfig = skill; CreateCircle();}

    private void OnEnable()
    {
        _deathTime = Time.time + _lifeTime;
        triggerSphereCastTime = Time.time + 0.5f;
        center = new Vector3(_transform.position.x, 0, _transform.position.z);
        if (_particleShape.scale != null) return;
        _particleShape.scale = new Vector3(_skillCOnfig.skillConfig.DamageAreaRadius * 10, _skillCOnfig.skillConfig.DamageAreaRadius * 10, 1);
    }

    private void OnDisable()
    {
        attackCasted = false;
    }


    private void Update()
    {

        if (Time.time >= triggerSphereCastTime && !attackCasted)
        {
            attackCasted = true;
            Collider[] collisions = Physics.OverlapSphere(center, _skillCOnfig.skillConfig.DamageAreaRadius, enemyLayerMask);
            foreach(Collider col in collisions)
            {
                if(col.tag == "EnemyBody")
                    col.GetComponentInParent<IDamageable>().TakeDamage(_skillCOnfig.skillConfig.damage);

            }

        }


        if (Time.time > _deathTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void CreateCircle()
    {
        for(int i = 0; i< 360; i++)
        {
            float xPos = _transform.position.x + _skillCOnfig.skillConfig.DamageAreaRadius * Mathf.Cos(Mathf.Deg2Rad * i);
            float zPos = _transform.position.z + _skillCOnfig.skillConfig.DamageAreaRadius * Mathf.Sin(Mathf.Deg2Rad * i);
            _line.SetPosition(i, new Vector3(xPos, 0, zPos));
        }
    }

}
