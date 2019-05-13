using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformObjects
{
    public GameObject[] Platforms;
    public float MinXProximity = -1f;
    public float MaxXProximity = -1f;
    public float MinYProximity = -1f;
    public float MaxYProximity = -1f;
    public int Density = -1;
}
