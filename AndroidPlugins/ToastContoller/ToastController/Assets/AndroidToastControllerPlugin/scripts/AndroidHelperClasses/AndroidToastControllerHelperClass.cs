using UnityEngine;
using System.Collections;

public class AndroidToastControllerHelperClass : MonoBehaviour {

	private static AndroidToastControllerHelperClass _instance;
	public AndroidJavaObject AndroidToastController;

	private AndroidJavaObject testobj = null;
	private AndroidJavaObject playerActivityContext = null;

	public static AndroidToastControllerHelperClass Instance
	{
		get
		{
			if(_instance == null)
				_instance = new AndroidToastControllerHelperClass();
			
			return _instance;
		}
	}
	
	public AndroidToastControllerHelperClass()
	{
		AndroidToastController = new AndroidJavaObject ("com.snicklefritz.utility.ToastControl");

		// First, obtain the current activity context
		using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
		}

		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				AndroidToastController.Call("SetCurrentContext",playerActivityContext);
			}));
	}

	public void DisplayText(string message)
	{		
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				AndroidToastController.Call("DisplayText",message);
			}));
	}
	
	public void DisplayText(string message, bool toastDisplayLengthIsLong)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayText",message,toastDisplayLengthIsLong, 0, 0);
		}));
	}
	
	public void DisplayTextBottomCenter(string message, bool toastDisplayLengthIsLong)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextBottomCenter",message,toastDisplayLengthIsLong);
		}));
	}
	
	public void DisplayTextTopCenter(string message, bool toastDisplayLengthIsLong)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextTopCenter",message,toastDisplayLengthIsLong);
		}));
	}
	
	public void DisplayTextLeftCenter(string message, bool toastDisplayLengthIsLong)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextLeftCenter",message,toastDisplayLengthIsLong);
		}));
	}
	
	public void DisplayTextRightCenter(string message, bool toastDisplayLengthIsLong)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextRightCenter",message,toastDisplayLengthIsLong);
		}));
	}
	
	public void DisplayTextBottomCenterWOffset(string message, bool toastDisplayLengthIsLong, int xOffset, int yOffset)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextBottomCenterWOffset",message,toastDisplayLengthIsLong, xOffset, yOffset);
		}));
	}
	
	public void DisplayTextTopCenterWOffset(string message, bool toastDisplayLengthIsLong, int xOffset, int yOffset)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextTopCenterWOffset",message,toastDisplayLengthIsLong, xOffset, yOffset);
		}));
	}
	
	public void DisplayTextLeftCenterWOffset(string message, bool toastDisplayLengthIsLong, int xOffset, int yOffset)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextLeftCenterWOffset",message,toastDisplayLengthIsLong, xOffset, yOffset);
		}));
	}
	
	public void DisplayTextRightCenterWOffset(string message, bool toastDisplayLengthIsLong, int xOffset, int yOffset)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayTextRightCenterWOffset",message,toastDisplayLengthIsLong, xOffset, yOffset);
		}));
	}
	
	public void DisplayText(string message, bool toastDisplayLengthIsLong, int gravity)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayText",message,toastDisplayLengthIsLong, gravity);
		}));
	}
	
	public void DisplayText(string message, bool toastDisplayLengthIsLong, int gravity, int xOffset, int yOffset)
	{
		playerActivityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
		                                                                    {
			AndroidToastController.Call("DisplayText",message,toastDisplayLengthIsLong, gravity, xOffset, yOffset);
		}));
	}
	
	void OnApplicationQuit()
	{
		_instance = null;
	}
}
