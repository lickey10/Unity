using UnityEngine;
using System.Collections;

public class CameraControlsHelperClass : MonoBehaviour {

	private static CameraControlsHelperClass _instance;
	private AndroidJavaObject playerActivityContext = null;

	public AndroidJavaObject CameraControls;

	public static CameraControlsHelperClass Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new CameraControlsHelperClass();

				//forces unity to write camera permissions to AndroidManifest.xml
				WebCamTexture wtc;
			}

			return _instance;
		}
	}

	public CameraControlsHelperClass()
	{
		try {
			CameraControls = new AndroidJavaObject("com.snicklefritz.cameraplugin.CameraContoller");
			
			// First, obtain the current activity context
			using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			
			
			CameraControls.Call("SetCurrentContext",playerActivityContext);
		} catch (System.Exception ex) {
			Debug.LogException(ex);
		}
	}

	public int GetCameraPlaneID()
	{
		return CameraControls.Call<int>("getCameraPlaneID");
	}


	void OnApplicationQuit()
	{
		_instance = null;
	}
}
