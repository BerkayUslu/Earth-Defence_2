using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private IPlayerController _player;
    private Animator _anim;

    private void Awake()
    {
        if(!TryGetComponent(out IPlayerController player))
        {
            Debug.LogError("Player Controller Could not found in player animator");
            Destroy(this);
        }
        _player = player;
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
        return _player.input.magnitude != 0 ? Run : Idle;
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Archer Idle");
    private static readonly int Run = Animator.StringToHash("Archer Run");

    #endregion
}