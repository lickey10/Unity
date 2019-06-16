using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "Boundary")
			Destroy(other.gameObject);
		else if(other.gameObject.name == "BoundaryWarning")
		{

		}
	}
}
