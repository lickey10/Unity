using UnityEngine;
using System.Collections;

public class CameraFullControlsHelperClass : MonoBehaviour {

	private static CameraFullControlsHelperClass _instance;

	public AndroidJavaObject CameraFullControls;

	public static CameraFullControlsHelperClass Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new CameraFullControlsHelperClass();

				//forces unity to write camera permissions to AndroidManifest.xml
				WebCamTexture wtc;
			}

			return _instance;
		}
	}

	public CameraFullControlsHelperClass()
	{
		CameraFullControls = new AndroidJavaObject("com.snicklefritz.cameraplugin.CameraContoller");
	}

	public void ToggleLight()
	{
		CameraFullControls.Call("ToggleLight");
	}

	public void LightOff()
	{
		CameraFullControls.Call("LightOff");
	}

	public void LightOn()
	{
		CameraFullControls.Call("LightOn");
	}

	void OnApplicationQuit()
	{
		_instance = null;
	}
}
