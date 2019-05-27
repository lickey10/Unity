using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	public float speed = 300.0f;

	// Use this for initialization
	void Start () {
		// Give the ball some initial movement direction
		//rigidbody2D.velocity = Vector2.right.normalized * speed;//(Vector2.Angle(new Vector2(1,0), new Vector2(1,1)) );

		//Vector2 newBall = (Vector2.Angle(new Vector2(1,0), new Vector2(1,1)));

		//rigidbody2D.velocity = newBall.one.normalized * speed;

		//rigidbody2D.AddForce(transform.forward * speed * Time.deltaTime);


		//rigidbody2D.velocity = Vector2.right.normalized * speed;

		//works - kind of
		//rigidbody2D.velocity = Vector2.one.normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
