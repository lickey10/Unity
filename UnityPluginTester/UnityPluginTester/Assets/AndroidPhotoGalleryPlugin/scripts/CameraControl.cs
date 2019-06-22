using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private CameraControlsHelperClass cameraControlHelperClass;
	private int cameraPlaneID = -1;
	public Text TxtMessage;
	
	void Start()
	{
		TxtMessage.text = "Starting CameraControl";
		Debug.Log("Starting CameraControl");
		cameraControlHelperClass = CameraControlsHelperClass.Instance;

	}
	
	void Update()
	{
		if(Input.GetMouseButton(0))
		{
				#if UNITY_ANDROID
			TxtMessage.text = "Found Touch";
			Debug.Log("Found Touch");
			cameraPlaneID = cameraControlHelperClass.GetCameraPlaneID();
			Debug.Log("cameraPlaneID = "+ cameraPlaneID.ToString());
			TxtMessage.text = "cameraPlaneID = "+ cameraPlaneID.ToString();
				#endif
		}
	}

	
}
