using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] HeroAttackConfig _config;
    Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        StartCoroutine("ArrowAttack");
    }

    private Vector3 AutoAim()
    {
        Collider[] colliderArray = Physics.OverlapSphere(_transform.position, 50f, LayerMask.GetMask("Enemy"));
        Vector3 bestDirection = new Vector3(3f, 0, 2f).normalized;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = _transform.position;
        foreach (Collider col in colliderArray)
        {
            Vector3 directionToTarget = col.gameObject.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestDirection = directionToTarget.normalized;
            }
        }

        return bestDirection;
    }

    private IEnumerator ArrowAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameObject attack = AttackPool.attackPoolSharedInstance.GetPooledObjectOrCreateIfNotAvailable(_config.AttackPrefab, _config.AttackName);
            attack.transform.position = _transform.position + new Vector3(0, 1, 0);
            Vector3 aim = AutoAim();
            attack.GetComponent<Arrow>().SetDirection(aim);
            attack.SetActive(true);
        }

    }
}
