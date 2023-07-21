using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Config", menuName = "Units/Heroes/Hero Attack Config")]
public class HeroAttackConfig : ScriptableObject
{
    public string AttackName;
    public bool EnableAutoAim;
    public bool AOETypeAttack;
    public float DamageAreaRadius;
    public int damage;
    public int Speed;
    public int DamageIncreaseWithLevel;
    public float SpeedIncreaseWithLevel;
    public float CooldownDecreasePrecentage;
    public float BaseCooldownTime;
    public GameObject AttackPrefab;
}
