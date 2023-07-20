using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Range Setting", menuName ="Scriptable/Settings/Range Settings")]
public class RangeSettings : ScriptableObject
{
    public float meleeRange;
    public float rangedClose;
    public float rangedMid;
}
