using UnityEngine;
using System.Collections;

public class AndroidPhotoGalleryHelperClass : MonoBehaviour {

	private static AndroidPhotoGalleryHelperClass _instance;
	string theError = "";
	string theError2 = "";
	public AndroidJavaObject AndroidPhotoGallery;

	private AndroidJavaObject testobj = null;
	private AndroidJavaObject playerActivityContext = null;

	public static AndroidPhotoGalleryHelperClass Instance
	{
		get
		{
			if(_instance == null)
				_instance = new AndroidPhotoGalleryHelperClass();
			
			return _instance;
		}
	}
	
	public AndroidPhotoGalleryHelperClass()
	{
		AndroidPhotoGallery = new AndroidJavaObject ("com.snicklefritz.utility.AndroidPhotoGallery");

		// First, obtain the current activity context
		using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
		}

		AndroidPhotoGallery.Call("SetCurrentActivity",playerActivityContext);
	}
	
	public void Open()
	{
		AndroidPhotoGallery.Call("Open");
	}

	public void Insert(string ImagePath)
	{
		AndroidPhotoGallery.Call("Insert", ImagePath);
	}
	
	void OnApplicationQuit()
	{
		_instance = null;
	}
}
