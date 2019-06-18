using UnityEngine;
using System.Collections;

public class cannon : MonoBehaviour {

	public GameObject projectilePrefab;
	public float speed = 5.0f;
	public Joystick joystick;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow) || joystick.position.x > 0)
		{
			//transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
			transform.Rotate(new Vector3(0,speed * Time.deltaTime,0));

		}
		if(Input.GetKey(KeyCode.LeftArrow) || joystick.position.x < 0)
		{
			//transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
			transform.Rotate(new Vector3(0,-speed * Time.deltaTime,0));

		}
		if(Input.GetKey(KeyCode.DownArrow) || joystick.position.y < 0)
		{
			//transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
			transform.Rotate(new Vector3(speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.UpArrow) || joystick.position.y > 0)
		{
			//transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
			transform.Rotate(new Vector3(-speed * Time.deltaTime,0,0));
		}
	}

	void FixedUpdate () {
//		if (Input.GetButtonDown("Fire1")) {
//			GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
//			projectile.rigidbody.AddRelativeForce(new Vector3(0, 0, 2000));
//		}
	}

	void LateUpdate() {
//		float x = Input.GetAxis("Mouse X") * 2;
//		float y = -Input.GetAxis("Mouse Y");
//		
//		// vertical tilting
//		float yClamped = transform.eulerAngles.x + y;
//		transform.rotation = Quaternion.Euler(yClamped, transform.eulerAngles.y, transform.eulerAngles.z);
//		
//		// horizontal orbiting
//		transform.RotateAround(new Vector3(0, 3, 0), Vector3.up, x);
	}
}
