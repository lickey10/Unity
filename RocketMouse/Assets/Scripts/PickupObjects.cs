using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PickupObjects
{
    public PickupItem[] PickupItems;
    public float MinXProximity = -1f;
    public float MaxXProximity = -1f;
    public int DensityMin = -1;
    public int DensityMax = -1;
}
