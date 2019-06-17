using UnityEngine;
using System.Collections;

public class MainCam : MonoBehaviour
{
	int cammode = 0;
	bool wireframe = false;
	public Transform target; //target of the camera
	public float yadd = 10.0f; //default altitude
	public float distance = 25.0f; //default zoom
	public float zoomStep = 1.0f; //speed of zooming and dezooming
	float x, y, speed = 0.0f;
	Vector3 distanceVector;
	
	//Move the camera to its initial position
	void Start ()
	{
		distanceVector = new Vector3(0.0f,yadd,-distance);
		Vector2 angles = this.transform.localEulerAngles;
		x = angles.x;
		y = angles.y;
		
		Quaternion rotation = Quaternion.Euler(y,x,0.0f);
		Vector3 position = rotation * distanceVector + target.position;
		transform.rotation = rotation;
		transform.position = position;
	}
	
	
	//Gui
	void OnGUI ()
	{
		
		if (GUI.Button (new Rect (115,40,80,20), "Reset")) Application.LoadLevel(0); //reset button
		
		if (GUI.Button (new Rect (5,0,80,20), "Fullscreen")) //fullscreen button
		{
			Screen.fullScreen = !Screen.fullScreen; 
		}
		
		if (wireframe == true) //wireframe mode button
		{
			if (GUI.Button (new Rect (5,135,190,30), "Wireframe mode : ON"))
				
				wireframe = false;
		}
		else
		{
			if (GUI.Button (new Rect (5,135,190,30), "Wireframe mode : OFF"))
				wireframe = true;
		}
		
		if (cammode == 0) // free cam button
		{
			if (GUI.Button (new Rect (115, 0, 80, 20), "Cam Free")) cammode = 1;
		}
		else if (cammode == 1) // chase cam button
		{
			if (GUI.Button (new Rect (115, 0, 80, 20), "Cam Chase ")) cammode = 2;
		}
		else // locked cam button
		{
			if (GUI.Button (new Rect (115, 0, 80, 20), "Cam Lock ")) cammode = 0;
		}
	}
	
	void LateUpdate ()
	{
		// Zoom or dezoom using mouse wheel
		if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f)
		{
			distance -= zoomStep;
			distanceVector = new Vector3(0.0f,yadd,-distance);
			
		}
		else if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f)
		{
			distance += zoomStep;
			distanceVector = new Vector3(0.0f,yadd,-distance);
		}
		
		
		if(cammode ==0) // free cam
		{
			// rotate the camera when the middle mouse button is pressed
			if(Input.GetKey(KeyCode.Mouse2))
			{
				x += Input.GetAxis("Mouse X") * 5.0F;
				y += -Input.GetAxis("Mouse Y")* 5.0F;
			}
			
			Quaternion rotation = Quaternion.Euler(y,x,0.0f);
			Vector3 position = rotation * distanceVector + target.position;
			transform.rotation = rotation;
			transform.position = position;
		}
		else if(cammode ==1)// chase cam
		{
			
			// rotate the camera when the middle mouse button is pressed
			if(Input.GetKey(KeyCode.Mouse2)) y -= Input.GetAxis("Mouse Y")*5.0F;
			
			if(Input.GetKey(KeyCode.Mouse2)) x += Input.GetAxis("Mouse X")*10.0f;
			// smooth reset camera rotation
			else x = Mathf.SmoothDampAngle(x,target.eulerAngles.y,ref speed,0.5f);
			
			Quaternion rotation = Quaternion.Euler(target.eulerAngles.z+y, x, 0.0f);
			Vector3 position = rotation * distanceVector + target.position;
			transform.rotation = rotation;
			transform.position = position;
		}
		else // locked cam
			transform.LookAt(target.transform);
	}
	
	
	//wireframe mode
	void OnPreRender()
	{
		if (wireframe == true) GL.wireframe = true;
		else GL.wireframe = false;
	}
	void OnPostRender()
	{
		GL.wireframe = false;
	}
	
	
}
