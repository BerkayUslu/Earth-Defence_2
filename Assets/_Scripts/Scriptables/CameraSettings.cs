using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Camera Setting", menuName = "Scriptable/Settings/Camera Settings")]
public class CameraSettings : ScriptableObject 
{
    public Vector3 GameStartPosition;
    public Vector3 GameStartRotation;

    public Vector3 MenuPosition;
    public Vector3 MenuRotation;
}
