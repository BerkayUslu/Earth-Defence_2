using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _anim;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var state = GetState();

        if (state == _currentState) return;

        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (_enemy.IsItDead())
        {
            return Death;
        }
        else
        {
            return _enemy.IsItAttacking() ? Attack : Run;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");

    #endregion
}
