using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {
	public Texture2D ButtonBackground;
	public string LevelToLoad = "";
	int X = (Screen.width - 240 ) / 2;
	int Y = (Screen.height + 120) / 2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI()
	{
		if(ButtonBackground != null)
		{

			if (
				GUI.Button(
				// Center in X, 1/3 of the height in Y
				new Rect(
				X,
				Screen.height - 125,
				240,
				120
				)
				,ButtonBackground, new GUIStyle()
				)
				)
			{
				if(LevelToLoad.Length > 0)
					Application.LoadLevel(LevelToLoad);
				else if(!gamestate.Instance.IsGameRunning())
				{
					//the game isn't running so reset variables and go to the logo screen
					gamestate.Instance.Reset();
					Application.LoadLevel("logo");
				}
				else
				{
					//load next level
					if(gamestate.Instance.getActiveLevel() > gamestate.Instance.GetNumberOfLevels())//they have won the game
					{
						Application.LoadLevel("youWon");
					}
					else if(gamestate.Instance.getLives() == 0)//game over
					{
						Application.LoadLevel("gameOver");
					}
					else if(gamestate.Instance.GetNumberOfLevels() > 1 && gamestate.Instance.getActiveLevel() == 1)//load level menu
						Application.LoadLevel("LevelMenu");
					else
					{
						lightsOut.Instance.ResetLights();
						Application.LoadLevel("level"+ gamestate.Instance.getActiveLevel() +"StartMenu");
					}
				}


			}
		}
	}

}
