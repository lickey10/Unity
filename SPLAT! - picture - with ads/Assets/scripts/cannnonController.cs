using UnityEngine;
using System.Collections;

public class cannnonController : MonoBehaviour {

	MouseLook mouseLook;

	// Use this for initialization
	void Start () {
		mouseLook = FindObjectOfType<MouseLook>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(transform.position.x,mouseLook.rotationY,transform.position.z);
	}
}
