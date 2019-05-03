using SgLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightGroupController : MonoBehaviour {

    public bool DoLightsShift = true;
    public int GroupBonusPoints = 50;
    public bool GroupHasBeenLit = false;
    public GameObject[] CorrespondingLights;

    LightController[] lightsInGroup;

    // Use this for initialization
    void Start () {
        lightsInGroup = GetComponentsInChildren<LightController>().OrderBy(o => o.GroupOrder).ToArray();
    }
	
	// Update is called once per frame
	void Update () {

        //check to see if all the lights are lit
        if (!lightsInGroup.Where(l => l.On == false).Any())
        {
            Flash(3);

            ScoreManager.Instance.AddScore(GroupBonusPoints);

            if (!CorrespondingLights.Where(x => x.GetComponent<LightController>().On).Any())//the group hasn't been lit before
                CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().TurnLightOn());
            else //the group has already been lit once
            {
                CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().TurnLightOff());

                ScoreManager.Instance.AddScore(GroupBonusPoints);
            }
        }
    }

    public void ShiftLightsRight()
    {
        if (DoLightsShift)
        {
            bool previousLightOn = false;
            bool tempPreviousLightOn = false;

            LightController[] lights = GetComponentsInChildren<LightController>().OrderBy(o => o.GroupOrder).ToArray();

            previousLightOn = lights.Last().On;

            foreach (LightController light in lights)
            {
                tempPreviousLightOn = light.On;
                light.On = previousLightOn;
                previousLightOn = tempPreviousLightOn;
            }
        }
    }

    public void ShiftLightsLeft()
    {
        if (DoLightsShift)
        {
            bool previousLightOn = false;
            bool tempPreviousLightOn = false;
            
            LightController[] lights = GetComponentsInChildren<LightController>().OrderByDescending(o => o.GroupOrder).ToArray();

            previousLightOn = lights.Last().On;

            foreach (LightController light in lights)
            {
                tempPreviousLightOn = light.On;
                light.On = previousLightOn;
                previousLightOn = tempPreviousLightOn;
            }
        }
    }

    /// <summary>
    /// Check if the group is lit up
    /// </summary>
    public void CheckIfGroupIsLit()
    {
        //if all lights are lit add the score
        if (!lightsInGroup.Where(x => !x.On).Any())
        {
            ScoreManager.Instance.AddScore(GroupBonusPoints);
            
            lightsInGroup.Where(x => x.On).ToList().ForEach(x => x.TurnLightOff());

            GroupHasBeenLit = true;

            if(CorrespondingLights != null && CorrespondingLights.Length > 0)
            {
                var light = CorrespondingLights.Where(x => !x.GetComponent<LightController>().On).FirstOrDefault();

                if (light != null)
                    light.GetComponent<LightController>().TurnLightOn();
            }
        }
    }
    
    public void Flash(int timesToFlash)
    {
        lightsInGroup.ToList().ForEach(x => x.GetComponent<LightController>().Flash(timesToFlash));
    }

    public void Flash(int timesToFlash, float delay)
    {
        lightsInGroup.ToList().ForEach(x => x.GetComponent<LightController>().Flash(timesToFlash, delay));
    }

    public void FlashRightToLeft(int timesToFlash)
    {
        LightController[] lights = GetComponentsInChildren<LightController>().OrderByDescending(o => o.GroupOrder).ToArray();

        flashLightsWithDelay(lights, timesToFlash, 0.2f);
    }

    public void FlashLeftToRight(int timesToFlash)
    {
        flashLightsWithDelay(lightsInGroup, timesToFlash, 0.2f);
    }

    IEnumerator turnLightOff(LightController light)
    {
        yield return new WaitForSeconds(0);

        StartCoroutine(turnLightOff(light, 1));
    }

    IEnumerator turnLightOff(LightController light, float delay)
    {
        yield return new WaitForSeconds(delay);

        light.TurnLightOff();
    }

    IEnumerator turnLightOn(LightController light)
    {
        yield return new WaitForSeconds(0);

        light.TurnLightOn();
    }

    IEnumerator turnLightOn(LightController light, float delay)
    {
        yield return new WaitForSeconds(delay);

        light.TurnLightOn();
    }

    IEnumerator flashLightsWithDelay(LightController[] lights, int timesToFlash, float delay)
    {
        foreach (LightController light in lights)
        {
            light.Flash(timesToFlash,1);

            yield return new WaitForSeconds(delay);
        }
    }
}
