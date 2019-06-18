using UnityEngine;
using System.Collections;

public class pictureCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(StaticVariables.Picture != null)
		{
			GetComponent<Renderer>().material.mainTexture = StaticVariables.Picture;

//			#if UNITY_ANDROID
//				float rotationSpeed=0.5f ;// This Must be less than 1 and greater than 0
//				
//				// calculate rotation to be done
//				Quaternion targetRotation  = Quaternion.LookRotation(transform.position); 
//				
//				//NOTE :: If you don't want rotation along any axis you can set it to zero is as :-
//				// Setting Rotation along z axis to zero
//				targetRotation.z=0; 
//				
//				// Setting Rotation along x axis to zero
//				targetRotation.x=90; 
//				
//				targetRotation.y=0; 
//				
//				// Apply rotation
//				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//			#elif UNITY_IPHONE
//			string adUnitId = "";
//			#else 
//			//windows?
//			string adUnitId = "";
//			#endif


		}
		else
			Application.LoadLevel("camera");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}


}
