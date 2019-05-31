using UnityEngine;
using System.Collections;

public class CollisionPropagator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log ( "this = " + this.name );
	}
}
