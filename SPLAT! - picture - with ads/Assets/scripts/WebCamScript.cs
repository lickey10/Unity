using UnityEngine;
using System.Collections;

public class WebCamScript : MonoBehaviour {

	public string deviceName;
	WebCamTexture wct;

	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		deviceName = devices[0].name;
		wct = new WebCamTexture(deviceName, 300, 400, 12);
		GetComponent<Renderer>().material.mainTexture = wct;
		wct.Play();

//		Toast toast = new Toast();
//		toast.TextToDisplay = "Touch to take a picture";
//		toast.TextPosition = new Vector2(Screen.width/2-100,Screen.height/2-50);
//		toast.BeginDisplayingToast();


		GUIStyle toastStyle = new GUIStyle();
		toastStyle.normal.textColor = Color.white;
		toastStyle.fontSize = 20;
		GUI.backgroundColor = Color.black;


		GameObject tempToastObject = new GameObject();
		
		tempToastObject.AddComponent<Toast>();
		
		tempToastObject.GetComponent<Toast>().TextToDisplay = "Touch to take a picture";
		tempToastObject.GetComponent<Toast>().TextPosition = new Vector2(Screen.width/2-100,Screen.height/2-50);
		tempToastObject.GetComponent<Toast>().TextDirection = new Vector2(0, 0);
		tempToastObject.GetComponent<Toast>().TextSpeed = 0;
		tempToastObject.GetComponent<Toast>().TextGUIStyle = toastStyle;     

		tempToastObject.GetComponent<Toast>().BeginDisplayingToast();
		
		DestroyObject(tempToastObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{
			TakePhoto();
		}

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	void TakePhoto()
	{
		Texture2D photo = new Texture2D(wct.width, wct.height);
		photo.SetPixels(wct.GetPixels());
		photo.Apply();
		
//		//Encode to a PNG
//		byte[] bytes = photo.EncodeToPNG();
//		//Write out the PNG. Of course you have to substitute your_path for something sensible
//		System.IO.File.WriteAllBytes(@"D:\utilities\Programming\Unity\MyProjects\3d_cannon\screenShots\photo.png", bytes);
		
		StaticVariables.Picture = photo;
		
		wct.Stop();
		
		Application.LoadLevel("main");
	}
}
