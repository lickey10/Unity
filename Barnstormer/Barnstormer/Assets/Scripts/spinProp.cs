using UnityEngine;
using System.Collections;

public class spinProp : MonoBehaviour {
	float rotateSpeed = 200f; //Your speed

	public GameObject Prop;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Prop.transform.Rotate (0f, rotateSpeed * Time.deltaTime, 0f); //It never stops rotating.
	}

	void OnGUI()
	{
		//Prop.rigidbody.AddRelativeTorque(0,500,0); 
	}
}
