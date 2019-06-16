using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {
	float rotateSpeed = 15f; //Your speed

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotateSpeed * Time.deltaTime, 0f, 0f); //It never stops rotating.
	}
}
