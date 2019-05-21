using UnityEngine;
using System.Collections;

public class createBubblesScript : MonoBehaviour {
	public Transform Bubble;
	public static bool CrazyMode = false;
	public GUIStyle guiStyle;
	public GUISkin MenuSkin;

	// Use this for initialization
	void Start () {
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 20;

		float bubbleImageX = Bubble.gameObject.GetComponent<Renderer>().bounds.size.x;
		float bubbleImageY = Bubble.gameObject.GetComponent<Renderer>().bounds.size.y;
		float screenHeight = Screen.height/100;
		float screenWidth = Screen.width/100;
		
		for (float y = 0 + (screenHeight*-1); y < screenHeight+2; y+= bubbleImageY)
		{ 
			for (float x = 0 + (screenWidth*-1); x < screenWidth+1; x+= bubbleImageX)
			{ 
				Transform newBubble = (Transform)Instantiate(Bubble,new Vector3 (x, y, 1), Quaternion.identity); 
			} 
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//GUI.skin = MenuSkin;

		GUILayout.BeginArea(new Rect(0,80,100,75));

		if (GUILayout.Button("Reset",MenuSkin.GetStyle("button"), GUILayout.Height(40)))
	    {
	        Application.LoadLevel(Application.loadedLevel);
	    }

		GUILayout.FlexibleSpace();

		CrazyMode = GUILayout.Toggle(CrazyMode,"Crazy Mode");

		GUILayout.EndArea();
	}
}
