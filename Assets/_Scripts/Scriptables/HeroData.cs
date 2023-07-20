using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroData", menuName ="Units/Heroes/Hero Data")]
public class HeroData : ScriptableObject
{
    public string HeroName;
    public GameObject HeroPrefab;
    public int BaseHealth;
    public int BaseSpeed;
}
