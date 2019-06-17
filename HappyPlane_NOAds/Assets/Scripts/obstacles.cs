using UnityEngine;
using System.Collections;

public class obstacles : MonoBehaviour {

	public Vector2 velocity = new Vector2(-4, 0);
	public float range = 3.5f;
	//public bool FlyInWave = false;

	float amplitudeX = -50.0f;
	float amplitudeY = 10.0f;
	float omegaX = 0.5f;
	float omegaY = 4.0f;
	float index;
	
	// Use this for initialization
	void Start()
	{
		//move straight left
		rigidbody2D.velocity = velocity;
		transform.position = new Vector3(transform.position.x, transform.position.y - range * Random.value, transform.position.z);
	}

	void Update(){

//		if(FlyInWave)
//		{
//			index += Time.deltaTime;
//			float x = amplitudeX*Mathf.Cos (omegaX*index);      
//			float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
//			if(transform.position.x > 24){
//				transform.eulerAngles = new Vector3(270, -90, transform.position.z);
//			}
//			if(transform.position.x < -24){
//				transform.eulerAngles = new Vector3(270, 90, transform.position.z);
//			}   
//			transform.localPosition= new Vector3(x,y,20);
//			//transform.position= new Vector3(x,y,20);
//		}
	}

	void OnGUI () 
	{

	}
}
