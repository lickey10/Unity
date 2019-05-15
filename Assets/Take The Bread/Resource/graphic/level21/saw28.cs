using UnityEngine;
using System.Collections;

public class saw28 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameManager2.getInstance().playSfx("saw");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (name == "doorl") {
			transform.Rotate (new Vector3 (0, 0, -5));		
		} else {
			transform.Rotate (new Vector3 (0, 0, 5));		
		}
	}
}
