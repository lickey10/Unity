using UnityEngine;
using System.Collections;

public class PlaneControlP47 : MonoBehaviour {
	float speed = 80.0f;
	bool easyMode = false;
	float Alt = 0f;
	float Spd = 0f;
	public GUISkin guiSkin;
	public GUIContent item0 = new GUIContent("");
	public GUIContent item1 = new GUIContent("");
	public GUIContent item2 = new GUIContent("Alt...");
	public Collider Ground;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit[] hits;
		//Alt=transform.position.y + 200f;
		Spd=GetComponent<Rigidbody>().velocity.magnitude;
		Vector3 camChaseSpot = transform.position - transform.up +
				Vector3.forward * -5.0f;
		float chaseBias = 0.9f;

		Camera.main.transform.position = chaseBias * Camera.main.transform.position +
			(1.0f-chaseBias)*camChaseSpot;
		//Camera.main.transform.LookAt(transform.position + transform.forward * 20.0f);
		Camera.main.transform.LookAt((transform.position - transform.up * 50.0f));

		speed -= transform.up.y;

		if(speed < 40.0f) {
			speed = 40.0f;
		}

		hits = Physics.RaycastAll (transform.position, -transform.up, 1000.0f);
		foreach( RaycastHit hit in hits ) {
			if(  hit.collider == Ground )
				Alt = transform.position.y - hit.point.y;   
		}

		float controlEffect = speed/120.0f;

//		if(transform.position.y < Terrain.activeTerrain.SampleHeight(transform.position)) {
//			speed = 0.0f;
//			transform.position =
//				new Vector3(transform.position.x,
//				Terrain.activeTerrain.SampleHeight(transform.position),
//				            transform.position.z);
//		}

		transform.position += transform.up * Time.deltaTime * speed * -1;

		//transform.position += transform.forward * Time.deltaTime * speed;

//		transform.Rotate(controlEffect*Input.GetAxis("Vertical"),0.0f,
//		                 -controlEffect*Input.GetAxis("Horizontal"));

		if(easyMode)
		{
			float turnPower = 0;

			if(Input.GetAxis("Vertical") > Input.GetAxis("Horizontal"))
				turnPower = Input.GetAxis("Vertical");
			else
				turnPower = Input.GetAxis("Horizontal");

			transform.Rotate(controlEffect*(turnPower*-1),controlEffect*Input.GetAxis("Horizontal"),0.0f);

			   //keep from rolling
			if(transform.rotation.y > 140)
				transform.rotation.Set(transform.rotation.x,140f,transform.rotation.z,transform.rotation.w);
			else if(transform.rotation.y > -140)
				transform.rotation.Set(transform.rotation.x,-140f,transform.rotation.z,transform.rotation.w);
		}
		else//advanced
			transform.Rotate(controlEffect*Input.GetAxis("Vertical"),controlEffect*Input.GetAxis("Horizontal"),0.0f);
	}

	void OnGUI(){ 
		//GUI.skin = guiSkin;
		GUI.Box(new Rect(0, Screen.height-100, 200, 100),"Plane");
		GUI.Box(new Rect(70, Screen.height-80, 120, 70), item0);
		GUI.Box(new Rect(10, Screen.height-80, 50, 70), item1);
		GUI.Label(new Rect(20, Screen.height-80, 100, 50), "Alt...");
		GUI.Label(new Rect(15, Screen.height-60, 100, 50), "Speed");
		GUI.Label(new Rect(80, Screen.height-80, 100, 50), Alt.ToString());
		GUI.Label(new Rect(80, Screen.height-60, 100, 50), Spd.ToString());
	}
}
