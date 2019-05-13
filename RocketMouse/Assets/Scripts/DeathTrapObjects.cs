using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeathTrapObjects
{
    public DeathTrapItem[] DeathTrapItems;
    public float MinXProximity = -1f;
    public float MaxXProximity = -1f;
    public int DensityMin = 0;
    public int DensityMax = 0;
}
