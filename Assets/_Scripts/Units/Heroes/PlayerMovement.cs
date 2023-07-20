using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPlayerController
{
    private Vector2 _playerInput;
    private Transform _transform;
    private Rigidbody _rb;
    private int heroSpeed;
    [SerializeField] HeroData heroData;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float rotationRate = 0.25f;

    public Vector2 input { get { return _playerInput; } }
    public Vector3 position { get { return _transform.position; } }

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
        heroSpeed = heroData.BaseSpeed;
    }

    private void Update()
    {
        if (!IsThereInput()) return;
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector3(_playerInput.x, 0, _playerInput.y) * heroSpeed;
    }

    private void RotatePlayer()
    {
        Quaternion lookTowards = Quaternion.LookRotation(new Vector3(_playerInput.x, 0, _playerInput.y));
        _transform.rotation = Quaternion.Lerp(_transform.rotation, lookTowards, rotationRate);
    }

    public void GetControllerInput(InputAction.CallbackContext context)
    {
        _playerInput = context.ReadValue<Vector2>();
    }

    private bool IsThereInput()
    {
        return _playerInput.magnitude != 0;
    }
}

