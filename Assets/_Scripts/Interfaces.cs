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