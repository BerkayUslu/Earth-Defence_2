using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _transform;
    private Vector3 distanceBtwCameraAndTarget;
    private IPlayerController _target;
    [SerializeField] GameObject followTarget;
    [SerializeField] CameraSettings _cameraSettings;

    private void Awake()
    {
        if (!followTarget.TryGetComponent(out IPlayerController target))
        {
            Debug.LogError("Camera controller can not find target");
            Destroy(this);
        }
        _target = target;
        _transform = transform;
    }

    private void Start()
    {
        _transform.position = _cameraSettings.GameStartPosition;
        _transform.rotation = Quaternion.Euler(_cameraSettings.GameStartRotation);
        distanceBtwCameraAndTarget = _transform.position - _target.position;
    }

    private void LateUpdate()
    {
        FollowTheTarget();
    }

    private void FollowTheTarget()
    {
        _transform.position = _target.position + distanceBtwCameraAndTarget;
    }
}
