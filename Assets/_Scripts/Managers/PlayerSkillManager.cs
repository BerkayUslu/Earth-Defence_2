using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour, ISkill
{
    [SerializeField] List<Skill> skillCatalogue;
    private PlayerAttack _playerAttack;

    private void Awake()
    {
    }

    private void Start()
    {
        _playerAttack = GameObject.Find("Player").GetComponentInChildren<PlayerAttack>();
        HeroAttackConfig[] array = snail.SearchAssets.SearchAssetsForScriptableObjectInstances<HeroAttackConfig>();
        foreach (HeroAttackConfig config in array)
        {
            skillCatalogue.Add(new Skill(config));
        }
    }

    public List<string> GetSkillNames()
    {
        List<string> temp = new List<string>();
        foreach(Skill skill in skillCatalogue)
        {
            temp.Add(skill.skillConfig.AttackName);
        }
        return temp;
    }

    public void SkillLevelUp(string skillName)
    {
        //This will be accesed from UI
        foreach (Skill skill in skillCatalogue)
        {
            if(skill.skillConfig.AttackName == skillName)
            {
                skill.LevelUp();
                _playerAttack.UpdateTheAttack(skill);
                return;
            }
        }
    }

}

[System.Serializable]
public class Skill
{
    //base stats for skill
    public HeroAttackConfig skillConfig;
    //Changing stats
    public int level = 0;
    public float DamageAreaRadius;
    public int Damage;
    public float Speed;
    public float Cooldown;

    public Skill(HeroAttackConfig _config)
    {
        skillConfig = _config;
        DamageAreaRadius = skillConfig.DamageAreaRadius;
        Damage = skillConfig.damage;
        Speed = skillConfig.Speed;
        Cooldown = skillConfig.BaseCooldownTime;
    }

    public void LevelUp()
    {
        level++;
        DamageAreaRadius += DamageAreaRadius * (skillConfig.DamageRadiusIncreaseWithLevel / 100);
        Cooldown -= Cooldown * (skillConfig.CooldownDecreasePrecentageWithLevel / 100);
        Damage += skillConfig.DamageIncreaseWithLevel;
        Speed += Speed + skillConfig.SpeedIncreaseWithLevel;
    }

}