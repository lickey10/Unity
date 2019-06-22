using UnityEngine;
using System.Collections;

public class FlashControl : MonoBehaviour {

	public GameObject MyCube;

	private bool isLightOn = false;
	private FlashControlsHelperClass flashControlHelperClass;
	
	void Start()
	{
		flashControlHelperClass = FlashControlsHelperClass.Instance;

		if(flashControlHelperClass == null && MyCube != null)
			MyCube.GetComponent<Renderer>().material.color = Color.red; 
		else if(MyCube != null)
			MyCube.GetComponent<Renderer>().material.color = Color.green; 
		
		string checkFlashControl = "";
	}
	
	void Update()
	{
		if(Input.GetMouseButton(0))
		{
//			if(MyCube != null)
//				MyCube.GetComponent<Renderer>().material.color = Color.red; 
			
//			try 
//			{
				#if UNITY_ANDROID
			//if(flashControlHelperClass != null)
				flashControlHelperClass.ToggleLight();
				#endif
				
//			} 
//			catch (System.Exception ex) 
//			{
//				Debug.Log(ex.Message);
//				string errorMessage = ex.Message;
//			}

			if(MyCube != null)
				MyCube.GetComponent<Renderer>().material.color = Color.cyan; 
		}
		else 
		{
			if(flashControlHelperClass == null && MyCube != null)
				MyCube.GetComponent<Renderer>().material.color = Color.red; 
			else if(MyCube != null)
				MyCube.GetComponent<Renderer>().material.color = Color.green;
		}
	}

	public void ToggleLight()
	{
//		if(MyCube != null)
//			MyCube.GetComponent<Renderer>().material.color = Color.red;
//		
		try 
		{
			#if UNITY_ANDROID
			flashControlHelperClass.ToggleLight();
			#endif
			
		} 
		catch (System.Exception ex) 
		{
			Debug.Log(ex.Message);
			string errorMessage = ex.Message;
		}
	}
}
