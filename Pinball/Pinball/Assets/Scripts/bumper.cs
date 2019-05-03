using UnityEngine;
using System.Collections;

public class bumper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        ContactPoint cp = c.contacts[0];
        // calculate with addition of normal vector
        c.rigidbody.velocity = c.rigidbody.velocity + cp.normal * 2.0f * c.rigidbody.velocity.magnitude;
        // myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;

        // calculate with Vector3.Reflect
        c.rigidbody.velocity = Vector3.Reflect(c.rigidbody.velocity, cp.normal);
        //myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);

        // bumper effect to speed up ball
        c.rigidbody.velocity += cp.normal * 0.2f;
        //myRigidbody.velocity += cp.normal * 2.0f;
    }
}
