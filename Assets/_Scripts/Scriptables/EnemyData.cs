using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy Data", menuName ="Units/Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public GameObject EnemyPrefab;
    public int BaseHealth;
    public int BaseSpeed;
    public int AttackPower;
    public int ExperiencePoints;
    public float AttackFrequency;
    public EnemyRange enemyRange;
}

public enum EnemyRange
{
    Melee,
    RangedClose,
    RangedMid
}

