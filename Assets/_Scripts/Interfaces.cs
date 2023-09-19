using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damageValue);
}

public interface IPlayerController
{
    public Vector2 input { get; }
    public Vector3 position { get; }
}

public interface ISkill
{
    public void SkillLevelUp(string skillName);
    public List<string> GetSkillNames();
}

public interface IAutoAim
{
    void SetDirection(Vector3 direction);
}

public interface IExperience
{
    public void GainExperience(int expAmount);

}

public interface IHealth
{
    public int GetHealth();
    public int GetMaxHealth();
}

public interface IEnemy
{
    public IEnumerator Attack();
    public void StartAttack();
    public void StopAttack();
    public void SetReferences(IDamageable playerHealth, EnemyData enemyData);
}

public interface ISkillComponent
{
    public void SetSkillConfig(Skill skill);
    public void SetPosition(Vector3 playerPosition);
}