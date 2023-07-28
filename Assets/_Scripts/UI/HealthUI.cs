using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private IPlayerController _playerTransform;
    private Vector3 distanceFromPlayer;
    private RectTransform _transform;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _playerTransform = GameObject.Find("Player").GetComponentInChildren<IPlayerController>();
        if (_playerTransform == null) Debug.Log("player could not found");
        distanceFromPlayer = _transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _transform.position = _playerTransform.position + distanceFromPlayer;
    }
}
