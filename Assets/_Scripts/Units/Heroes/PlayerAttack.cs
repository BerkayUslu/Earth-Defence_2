using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAttack : MonoBehaviour
{
    private Dictionary<string, Coroutine> skillCoroutineDictionary;
    Transform _transform;

    private void Awake()
    {
        skillCoroutineDictionary = new Dictionary<string, Coroutine>();
        _transform = transform;
    }

    private void StartAttack(Skill skill)
    {

            Coroutine temp = StartCoroutine(AutoAimAttack(skill));
            skillCoroutineDictionary.Add(skill.skillConfig.AttackName, temp);

    }

    public void UpdateTheAttack(Skill skill)
    {
        //called from manager to start or update a skill
        string name = skill.skillConfig.AttackName;
        if (skillCoroutineDictionary.ContainsKey(name))
        {
            StopCoroutine(skillCoroutineDictionary[name]);
            skillCoroutineDictionary.Remove(name);
        }
        StartAttack(skill);
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

    private Vector3 GenerateRandomLocation(float radius, float yPos = 0f)
    {
        int angle = Random.Range(0, 360);
        int xPos = (int)(radius * Mathf.Cos(angle * Mathf.Deg2Rad));
        int zPos = (int)(radius * Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector3(xPos, yPos, zPos);
    }



    private IEnumerator AutoAimAttack(Skill skill)
    {
        while (true)
        {
            yield return new WaitForSeconds(skill.Cooldown);
            GameObject attack = AttackPool.attackPoolSharedInstance.GetPooledObjectOrCreateIfNotAvailable(skill.skillConfig.AttackPrefab, skill.skillConfig.AttackName);
            if (!skill.skillConfig.AOETypeAttack)
            {
                Vector3 aim = AutoAim();
                attack.GetComponent<IAutoAim>().SetDirection(aim);
                attack.GetComponent<ISkillComponent>().SetPosition(_transform.position);
            }
            else if(skill.skillConfig.RandomLocationAttack)
            {
                attack.GetComponent<ISkillComponent>().SetPosition(_transform.position + GenerateRandomLocation(skill.skillConfig.RandomSpawnRadius, 10.67f));
            }
            attack.GetComponent<ISkillComponent>().SetSkillConfig(skill);
            attack.SetActive(true);
        }

    }
}

