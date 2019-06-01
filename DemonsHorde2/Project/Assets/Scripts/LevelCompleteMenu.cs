using System;
using UnityEngine;

public class LevelCompleteMenu : MonoBehaviour
{
	public GUIStyle customGuiStyle;
	int groupWidth = 500;
	int groupHeight = 260;
	int buttonWidth = 120;
	int buttonHeight = 120;
	int numRows = 0;
	public Vector2 scrollPosition = Vector2.zero;
	public Vector2 touchPosition = Vector2.zero;
	public string Objective1 = "Skeletons killed: ";
	public string Objective2 = "Coins collected: ";
	public string Objective3 = "Score this level: ";
	public string Objective4 = "";

	void Start () 
	{
		if(Objective1 == "Skeletons killed: ")
			Objective1 += gamestate.Instance.currentLevelKills;

		if(Objective2 == "Coins collected: ")
			Objective2 += gamestate.Instance.currentLevelCoins;

		if(Objective3 == "Score this level: ")
			Objective3 += gamestate.Instance.GetCurrentLevelScore();

		gamestate.Instance.ResetLevel();
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
		GUI.BeginGroup(new Rect( 350, 100, 850, 550 ) );
		
		//customGuiStyle.normal.textColor = Color.blue;
		//GUI.Box(new Rect( 0, 0, 850, 575 ), "" );
		
//		GUI.skin.button.normal.background = (Texture2D)buttonContent.image;
//		GUI.skin.button.hover.background = (Texture2D)buttonContent.image;
//		GUI.skin.button.active.background = (Texture2D)buttonContent.image;
		
		customGuiStyle.fontSize = 30;
		//customGuiStyle.alignment = TextAnchor.MiddleCenter;

		customGuiStyle.normal.textColor = Color.white;
		GUI.Box(new Rect( 20, 75, 400, 30 ), Objective1 ,customGuiStyle);

		customGuiStyle.normal.textColor = Color.white;
		GUI.Box(new Rect( 20, 200, 400, 30 ), Objective2 ,customGuiStyle);

		customGuiStyle.normal.textColor = Color.white;
		GUI.Box(new Rect( 20, 350, 400, 30 ), Objective3 ,customGuiStyle);

		customGuiStyle.normal.textColor = Color.white;
		GUI.Box(new Rect( 20, 475, 400, 30 ), Objective4 ,customGuiStyle);

		
//		for(int x = 0; x < numRows; x++)
//		{
//			// Begin a Row
//			GUILayout.BeginHorizontal();
//			
//			// Use a for loop to create the row contents
//			for (int y = 0; (x*MaxColumns) + y < gamestate.Instance.GetNumberOfLevels(); y++)
//			{
//				buttonContent.text = ((x*MaxColumns) + y+1).ToString();
//				
//				if(((x*MaxColumns) + y+1) > gamestate.Instance.getLevelProgress())//locked
//				{
//					customGuiStyle.normal.textColor = Color.gray;
//					GUI.DrawTexture(new Rect(y*buttonWidth+buttonPadding,x*buttonHeight+buttonPadding,buttonWidth,buttonHeight),Border);
//					
//					
//					if (GUI.Button(new Rect(y*buttonWidth+buttonPadding,x*buttonHeight+buttonPadding,buttonWidth-5,buttonHeight-5),((x*MaxColumns)+(y+1)).ToString(),customGuiStyle))
//						SoundEffectsHelper.Instance.MakeLevelLockedSound();
//					
//					//draw lock
//					GUI.DrawTexture(new Rect((y*buttonWidth+buttonPadding)+15,(x*buttonHeight+buttonPadding)+buttonHeight/1.5f,buttonWidth/4f,buttonHeight/4f),Lock);
//					
//				}
//				else //unlocked
//				{
//					customGuiStyle.normal.textColor = Color.blue;
//					GUI.DrawTexture(new Rect(y*buttonWidth+buttonPadding,x*buttonHeight+buttonPadding,buttonWidth,buttonHeight),Border);
//					
//					if (GUI.Button(new Rect(y*buttonWidth+buttonPadding,x*buttonHeight+buttonPadding,buttonWidth-5,buttonHeight-5),((x*MaxColumns)+(y+1)).ToString(),customGuiStyle))
//					{
//						int selectedLevel = (x*MaxColumns)+(y+1);
//						gamestate.Instance.setActiveLevel(selectedLevel);
//						Application.LoadLevel("level"+ selectedLevel.ToString());
//						SoundEffectsHelper.Instance.MakeLevelStartedSound();
//					}
//				}
//				
//				if(y + 1 >= MaxColumns)
//					break;
//			}
//			
//			// End a Row
//			GUILayout.EndHorizontal();
//			
//			if(x*MaxColumns > gamestate.Instance.GetNumberOfLevels())
//				break;
//		}
		
		GUI.EndGroup();


		
		//GUI.EndScrollView();
	}
}


