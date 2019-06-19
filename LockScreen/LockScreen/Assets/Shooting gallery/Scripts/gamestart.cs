/*
        Unity 3D: Game Start Script Source for State Manager
       
    Copyright 2012 FIZIX Digital Agency
    http://www.fizixstudios.com
       
        For more information see the tutorial at:
    http://www.fizixstudios.com/labs/do/view/id/unity-game-state-manager
       
       
    Notes:
        This script is a basic GUI script to create a new game state; you will need the statemanager.cs
        script.
*/

using UnityEngine;
using System.Collections;

public class gamestart : MonoBehaviour
{
	public Texture2D ButtonBackground;
	public int NumberOfLevels = 1;
	public int Health = 1;
	public int NumberOfLives = 3;
	public int LevelProgress = 1;

	private int activeLevel = 1;
	private gamestate loadedGameState;

	int X = (Screen.width - 240 ) / 2;
	int Y = (Screen.height - 120) / 2;

	void Start () 
	{
		gamestate.Instance.Load("GameState");
		gamestate.Instance.SetGamePaused(true);
	}

	// Our Startscreen GUI
	void OnGUI ()
	{
		if(gamestate.Instance.GetGamePaused())
		{
			if(ButtonBackground != null)
			{
				
				if (
					GUI.Button(
					// Center in X, 1/3 of the height in Y
					new Rect(
					X,
					Y,
					240,
					120
					)
					,ButtonBackground, new GUIStyle()
					)
					)
				{
					startGame();
				}
			}
			else
			{
//				if(GUILayout.Button("Start", new GUIStyle(), GUILayout.Width(300), GUILayout.Height(200))){
//					startGame();
//				}

				if (
					GUI.Button(
					// Center in X, 1/3 of the height in Y
					new Rect(
					X,
					Y,
					240,
					120
					),
					"Start"
					)
					)
				{
					startGame();
				}
			}
		}
	}

	private void startGame()
	{
		print("Starting game");
		
		DontDestroyOnLoad(gamestate.Instance);

		gamestate.Instance.SetGameRunning(true);

//		if(gamestate.Instance.getHighScore() > 0)
//		{
			gamestate.Instance.StartState();
//			gamestate.Instance.StartState(NumberOfLevels,gamestate.Instance.getLives(),gamestate.Instance.getLevelProgress(),activeLevel,gamestate.Instance.GetHealth(), gamestate.Instance.getHighScore());
//		}
//		else
//			gamestate.Instance.StartState(NumberOfLevels,NumberOfLives,LevelProgress,activeLevel,Health,0);

	}
}