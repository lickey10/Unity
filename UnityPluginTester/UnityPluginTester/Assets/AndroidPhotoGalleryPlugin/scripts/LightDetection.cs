using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LightDetection : MonoBehaviour {
	public Text LightValue;
	public Text errorValue;
	public GameObject MyObject;

	private LightSensorHelperClass lightSensorHelperClass;
	static string sensorsString = "";
	static int sensorCount = 0;
	static string errorString = "";
	float lightIntensity = -1;



//	static LightDetection()
//	{
//		try {
//			//AndroidJavaClass sensorManagerClass = new AndroidJavaClass("android.hardware.SensorManager");
//
//			//sensorCount = sensorManagerClass.CallStatic<int>("SENSOR_ALL");
//			
//			//ArrayList sensors = sensorManagerClass.Call<ArrayList>("getSensorList",sensorManagerClass.CallStatic<int>("SENSOR_ALL"));
//			
//			//sensorCount = sensors.Count;
//
//
//		} catch (System.Exception ex) {
//			//errorString = ex.Message;
//			errorValue.text = ex.Message;
//		}
//
//		//sensorsString += 
//	}

	// Use this for initialization
	void Start () {
		//LightValue.text = sensorCount.ToString();



			


		lightSensorHelperClass = LightSensorHelperClass.Instance;

//		string checklightSensor = "";
//
//		if(errorValue != null)
//			errorValue.text = errorString;
//
//
//		try {
//			if(lightSensorHelperClass != null)
//			{
//				if(MyObject != null)
//					MyObject.GetComponent<Renderer>().material.color = Color.green;
//			}
//			else if(MyObject != null)
//				MyObject.GetComponent<Renderer>().material.color = Color.red; 
//			
//		} catch (System.Exception ex) {
//			if(errorValue != null)
//				errorValue.text = ex.Message;
//
//			Debug.LogException(ex);
//		}
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetMouseButton(0))
//		{
//			AndroidJavaClass sensorManagerClass = new AndroidJavaClass("android.hardware.Camera");
//		}

		float tempLightIntensity = lightSensorHelperClass.GetLightIntensityValue();

		if(tempLightIntensity != lightIntensity)// there was a change
			MyObject.GetComponent<Renderer>().material.color = Color.green;
		else
			MyObject.GetComponent<Renderer>().material.color = Color.magenta;

		lightIntensity = tempLightIntensity;

		LightValue.text = lightIntensity.ToString();
	}
}

