using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnOffLights()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Lights");

        foreach(GameObject light in lights)
        {
            //light.GetComponent<LightController>().TurnLightOff();
            //light.SetActive(false);
        }
    }
}
