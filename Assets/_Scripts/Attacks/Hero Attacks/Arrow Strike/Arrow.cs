using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] HeroAttackConfig _attackConfig;
    private Vector3 _direction;
    private float lifeTime = 5f;
    private float deathTime;
    private int rotationSpeed = 20;
    private bool isItOnUI = false;

    private void Awake()
    {
        _transform = transform;

    }

    //LEarnnnn
    private void OnEnable()
    {
        if (_direction != Vector3.zero)
        {
            // Calculate the desired rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

            // Preserve the X-axis rotation at 90 degrees
            targetRotation = Quaternion.Euler(90f, _transform.rotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Apply the new rotation to the player
            transform.rotation = targetRotation;
        }

        deathTime = Time.time + lifeTime;
    }

    private void RotateArrow()
    {
        _transform.Rotate(Vector3.up * rotationSpeed);
    }

    // 
    private void FixedUpdate()
    {
        RotateArrow();
        if (isItOnUI) return;
        OnMove();

        if (Time.time > deathTime) StartCoroutine("Deactivate");
    }

    public void SetDirection(Vector3 direction) { _direction = direction; }

    public void SetConfig(HeroAttackConfig config)
    {
        _attackConfig = config;
    }

    private void OnMove()
    {
        _transform.position += _direction/2 * _attackConfig.Speed;
    }

    public void SetIsItOnUI() { isItOnUI = true; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBody")
        {
            //do damage
            other.transform.parent.GetComponent<IDamageable>().TakeDamage(_attackConfig.damage);
            StartCoroutine("Deactivate");
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.06f);
        gameObject.SetActive(false);
    }
}
