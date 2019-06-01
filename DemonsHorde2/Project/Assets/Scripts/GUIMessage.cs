using UnityEngine;
using System.Collections;

public class GUIMessage : MonoBehaviour {
	public string[] TextToDisplay;
	public GUIStyle customGuiStyle;
	public float GrowSpeed = 0.06f;
	public int StartDelay = 0;
//	public int PositionX = -1;
//	public int PositionY = -1;
	public int MaxFontSize = 60;
	public int StartFontSize = 1;
	public bool HideAfterHitMaxSize = true;
	public bool DisplayTextShadow = false;
	public Color TextColor = Color.white;
	public Color TextShadowColor = Color.black;

	private bool hideText = false;
	Vector2 scrollPosition = new Vector2();

	public void DisplayText(string theText)
	{
		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		//		customGuiStyle.normal.textColor = Color.green;
		customGuiStyle.fontSize = StartFontSize;
		customGuiStyle.wordWrap = true;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
		
		customGuiStyle.alignment = TextAnchor.MiddleCenter;

		TextToDisplay = new string[1] {theText};
		InvokeRepeating("increaseTextSize",StartDelay,GrowSpeed);
	}

	// Use this for initialization
	void Start () {
		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
//		customGuiStyle.normal.textColor = Color.green;
		customGuiStyle.fontSize = StartFontSize;
		customGuiStyle.wordWrap = true;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
		
		customGuiStyle.alignment = TextAnchor.MiddleCenter;


//		if(PositionX == -1)
//			PositionX = Screen.width/2-400;
//
//		if(PositionY == -1)
//			PositionY = Screen.height/2-30;

		//DisplayText(TextToDisplay[0]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(!hideText)
		{
			customGuiStyle.normal.textColor = TextColor;


			//GUI.Box(new Rect( PositionX, PositionY, 450, 30 ), TextToDisplay ,customGuiStyle);

			//drop shadow
			if(DisplayTextShadow)
			{
//				customGuiStyle.normal.textColor = TextShadowColor;
//				GUI.Box(new Rect( PositionX+3, PositionY + 3, 450, 30 ), TextToDisplay ,customGuiStyle);
			}

			//GUILayout.BeginArea (new Rect(0, 0, Screen.width-100, Screen.height-50));
			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width-10), GUILayout.Height (Screen.height-10));
			/*changes made in the below 2 lines */

			//GUI.skin.box.wordWrap = true;     // set the wordwrap on for box only.


			for(int x = 0; x < TextToDisplay.Length; x++)
			{
				//GUILayout.BeginHorizontal("box");
				//GUILayout.Button("I'm the first button");//this is the image if there is any
				GUILayout.TextArea(TextToDisplay[x],customGuiStyle);        // just your message as parameter.
				//GUILayout.EndHorizontal();
			}
			
			GUILayout.EndScrollView ();
			
			//GUILayout.EndArea();
		}
	}

	void increaseTextSize()
	{
		customGuiStyle.fontSize +=2;

		if(customGuiStyle.fontSize >= MaxFontSize)
		{
			CancelInvoke("increaseTextSize");

			if(HideAfterHitMaxSize)
				Destroy(this);
		}
	}
}
