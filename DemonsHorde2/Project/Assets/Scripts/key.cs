using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {

		gamestate.Instance.GUIMessage("You got the Key!");

		SoundEffectsHelper.Instance.MakePickupSound();
		gamestate.Instance.GotKey = true;
        Destroy(gameObject);
	}
}
