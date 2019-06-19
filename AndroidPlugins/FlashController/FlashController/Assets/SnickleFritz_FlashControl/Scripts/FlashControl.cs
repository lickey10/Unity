using UnityEngine;
using System.Collections;

public class FlashControl : MonoBehaviour {

	private bool isLightOn = false;
	private FlashControlsHelperClass flashControlHelperClass;
	
	void Start()
	{
		#if UNITY_ANDROID
			flashControlHelperClass = FlashControlsHelperClass.Instance;
		#endif
	}

	void OnMouseDown()
	{
		GetComponent<Renderer>().material.color = Color.red;

		try 
		{
			#if UNITY_ANDROID
			flashControlHelperClass.ToggleLight();
			#endif
		} 
		catch (System.Exception ex) 
		{
			Debug.Log(ex.Message);
		}
	}

	void OnMouseUp()
	{
		GetComponent<Renderer>().material.color = Color.blue; //C#
	}
}
