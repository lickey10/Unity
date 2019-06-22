using UnityEngine;
using System.Collections;

public class LightSensorHelperClass : MonoBehaviour {

	private static LightSensorHelperClass _instance;
	string theError = "";
	string theError2 = "";
	public AndroidJavaObject LightSensor;

	private AndroidJavaObject testobj = null;
	private AndroidJavaObject playerActivityContext = null;

	public static LightSensorHelperClass Instance
	{
		get
		{
			if(_instance == null)
				_instance = new LightSensorHelperClass();
			
			return _instance;
		}
	}
	
	public LightSensorHelperClass()
	{
		//try {
		LightSensor = new AndroidJavaObject ("com.snicklefritz.sensorplugin.LightSensor");


			// First, obtain the current activity context
			using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
			}


			LightSensor.Call("SetCurrentActivity",playerActivityContext);

			// Pass the context to a newly instantiated TestUnityProxy object
//			using (var pluginClass = new AndroidJavaClass("com.example.testcode.TestUnityProxy")) {
//				if (pluginClass != null) {
//					//testobj = pluginClass.CallStatic<AndroidJavaObject>("instance");
//					testobj.Call("setContext", playerActivityContext);
//
//				}
//			}

//		} catch (System.Exception ex) {
//			theError = ex.Message;
//
//			theError2 = ex.Message;
//			Debug.LogException(ex);
//		}
	}
	
	public float GetLightIntensityValue()
	{
		//LightSensor.RegisterInterest(.Call("ToggleLight");
		//return LightSensor.Get<float>("GetLightValue");
		return LightSensor.Call<float>("GetLightValue");
	}
	
	void OnApplicationQuit()
	{
		_instance = null;
	}
}
