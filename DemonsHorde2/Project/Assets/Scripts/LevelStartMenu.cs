using System;
using UnityEngine;

public class LevelStartMenu : MonoBehaviour
{
	public string LevelInstructions = "";
	public GUIStyle customGuiStyle;
	int groupWidth = 500;
	int groupHeight = 260;
	int buttonWidth = 120;
	int buttonHeight = 120;
	int numRows = 0;
	public Vector2 scrollPosition = Vector2.zero;
	public Vector2 touchPosition = Vector2.zero;
	public string Objective1 = "Objective- Kill all skeletons to complete the level!";
	public string Objective2 = "Objective- Find the key to unlock the treasure chest!";
	public string Objective3 = "Objective- Unlock the treasure chest to find the gold!";
	public string Objective4 = "Objective- Collect as much gold as you can for a high score!";

	public string[] TextToDisplay;

	void Start () 
	{
		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		customGuiStyle.normal.textColor = Color.white;
		customGuiStyle.fontSize = 20;
		customGuiStyle.wordWrap = true;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; 
		
		customGuiStyle.alignment = TextAnchor.MiddleLeft;
//		customGuiStyle.fixedWidth = Screen.width - 80;

	}

	void Update () 
	{
		
	}

	void FixedUpdate () 
	{
		
	}

	/// <summary>
	/// Write out the instructions for the level
	/// </summary>
	void OnGUI()
	{
		var screenWidth = Screen.width;
		var screenHeight = Screen.height;
		
		//var groupX = ( screenWidth - groupWidth ) / 2;
		var groupX = ( screenWidth - groupWidth ) / 2;
		var groupY = ( screenHeight - groupHeight ) / 2;
		
//		GUI.BeginGroup(new Rect( groupX , groupY- buttonHeight, groupWidth, groupHeight ) );
		//GUI.backgroundColor = Color.blue;
		
		//logo
		//drop shadow
//		customGuiStyle.normal.textColor = Color.black;
//		GUI.Box(new Rect( 3, 3, 450, 30 ), "Happy Plane" ,customGuiStyle);
//		
//		customGuiStyle.normal.textColor = Color.green;
//		GUI.Box(new Rect( 0, 0, 450, 30 ), "Happy Plane" ,customGuiStyle);
		
		//levels
		//drop shadow
//		customGuiStyle.normal.textColor = Color.black;
//		GUI.Box(new Rect( 3, 43, 450, 30 ), "Levels" ,customGuiStyle);
//		
//		customGuiStyle.normal.textColor = Color.green;
//		GUI.Box(new Rect( 0, 40, 450, 30 ), "Levels" ,customGuiStyle);
		
//		GUI.EndGroup();

		//scrollPosition = GUI.BeginScrollView(new Rect( groupX , groupY, groupWidth+20, buttonHeight * 2), scrollPosition, new Rect(groupX , groupY, groupWidth, numRows * buttonHeight));
		
		//start of the button group
//		GUI.BeginGroup(new Rect( 350, 100, 850, 550 ) );
//		
//		//customGuiStyle.normal.textColor = Color.blue;
//		//GUI.Box(new Rect( 0, 0, 850, 575 ), "" );
//		
////		GUI.skin.button.normal.background = (Texture2D)buttonContent.image;
////		GUI.skin.button.hover.background = (Texture2D)buttonContent.image;
////		GUI.skin.button.active.background = (Texture2D)buttonContent.image;
//		
//
//		//customGuiStyle.alignment = TextAnchor.MiddleCenter;
//
//		customGuiStyle.normal.textColor = Color.white;
//		GUI.Box(new Rect( 20, 75, 400, 30 ), Objective1 ,customGuiStyle);
//
//		customGuiStyle.normal.textColor = Color.white;
//		GUI.Box(new Rect( 20, 200, 400, 30 ), Objective2 ,customGuiStyle);
//
//		customGuiStyle.normal.textColor = Color.white;
//		GUI.Box(new Rect( 20, 350, 400, 30 ), Objective3 ,customGuiStyle);
//
//		customGuiStyle.normal.textColor = Color.white;
//		GUI.Box(new Rect( 20, 475, 400, 30 ), Objective4 ,customGuiStyle);
//
//		GUI.EndGroup();


		
		//GUI.EndScrollView();





		GUILayout.BeginArea (new Rect(200, 70, Screen.width-160, Screen.height-100));
		scrollPosition = GUILayout.BeginScrollView(new Vector2(0,0), GUILayout.Width (Screen.width-170), GUILayout.Height (Screen.height-110));
		/*changes made in the below 2 lines */
		
		//GUI.skin.box.wordWrap = true;     // set the wordwrap on for box only.
		
		
		for(int x = 0; x < TextToDisplay.Length; x++)
		{
			//GUILayout.BeginHorizontal("box");
			//GUILayout.Button("I'm the first button");//this is the image if there is any
			GUILayout.Label(TextToDisplay[x],customGuiStyle);        // just your message as parameter.
			//GUILayout.EndHorizontal();
		}
		
		GUILayout.EndScrollView ();

		GUILayout.EndArea();
	}
}


