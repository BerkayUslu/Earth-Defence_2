using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IAutoAim, ISkillComponent
{
    private Transform _transform;
    private Skill _attackConfig;
    private Vector3 _direction;
    private float lifeTime = 5f;
    private float deathTime;
    private int rotationSpeed = 20;
    private bool isItOnUI = false;

    private void Awake()
    {
        _transform = transform;

    }

    private void OnEnable()
    {
        AdjustRotation();

        deathTime = Time.time + lifeTime;
    }

    private void AdjustRotation()
    {
        if (_direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
            targetRotation = Quaternion.Euler(90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = targetRotation;
        }
    }

    public void SetPosition(Vector3 playerPosition)
    {
        _transform.position = playerPosition + Vector3.up ;
    }

    public void SetSkillConfig(Skill skill) { _attackConfig = skill; }

    private void RotateArrowAroundItself()
    {
        _transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void FixedUpdate()
    {
        RotateArrowAroundItself();
        if (isItOnUI) return;
        OnMove();

        if (Time.time > deathTime) gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 direction) { _direction = direction; }


    private void OnMove()
    {
        _transform.position += _direction * _attackConfig.Speed / 4;
    }

    public void SetIsItOnUI() { isItOnUI = true; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBody")
        {
            //do damage
            other.transform.parent.GetComponent<IDamageable>().TakeDamage(_attackConfig.skillConfig.damage);
            gameObject.SetActive(false);
        }
    }


}
