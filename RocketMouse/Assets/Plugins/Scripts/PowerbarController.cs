using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerbarController : MonoBehaviour
{
    private GUIBarScript GUIBar;
    private bool buttonDown = false;
    public float Power = 0;
    private float valueMax = 100;
    private float tempPower = 0;

    void Start()
    {
        GUIBar = GetComponent<GUIBarScript>();
    }

    void Update()
    {
        if (GUIBar == null)
        {
            return;
        }

        if (buttonDown)
        {
            tempPower = tempPower + .01f;

            if (tempPower > valueMax)
                tempPower = valueMax;

            
        }
        else
        {
            tempPower = 0;
        }

        Power = tempPower;

        GUIBar.Value = Power;
    }

    public void AddPower()
    {
        buttonDown = true;
    }

    public void SubtractPower()
    {
        buttonDown = false;
    }
}
