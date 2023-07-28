using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Config", menuName = "Units/Heroes/Hero Attack Config")]
public class HeroAttackConfig : ScriptableObject
{
    public string AttackName;
    public bool EnableAutoAim;
    public bool RandomLocationAttack;
    public bool AOETypeAttack;
    public float RandomSpawnRadius;
    public float DamageAreaRadius;
    public int damage;
    public int Speed;
    public int DamageIncreaseWithLevel;
    public float SpeedIncreaseWithLevel;
    public float CooldownDecreasePrecentageWithLevel;
    public float DamageRadiusIncreaseWithLevel;
    public float BaseCooldownTime;
    public GameObject AttackPrefab;
}
