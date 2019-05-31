using UnityEngine;
using System.Collections;

public class pinHider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //hide all the pins at startup
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pin");
        
        foreach(GameObject pin in pins)
        {
            pin.GetComponent<SpriteRenderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
