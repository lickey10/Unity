using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DecorationObjects
{
    public DecorationItem[] Decorations;//trees, rocks, etc
    public float MinXProximity = -1f;
    public float MaxXProximity = -1f;
    //public int DensityMax = -1;
}
