using UnityEngine;
using System.Collections;

public class ScoreAndLives : MonoBehaviour {
	public int BannerHeight = -1;
	public int ScoreGoalForLevel = -1;

	// Use this for initialization
	void Start () {
		if (BannerHeight > -1)
			gamestate.Instance.SetBannerHeight(BannerHeight);

		if(ScoreGoalForLevel <= 0 || gamestate.Instance.getActiveLevel() > gamestate.Instance.GetNumberOfLevels())
			ScoreGoalForLevel = gamestate.Instance.getActiveLevel() * 200;
			
		gamestate.Instance.ScoreGoalForCurrentLevel = ScoreGoalForLevel;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.Instance.getLives () < 1)//level is over
			Invoke("endLevel",2f);
	}

	void endLevel()
	{
		//the level is over
		//level complete
		if(gamestate.Instance.ScoreCurrentLevel >= ScoreGoalForLevel)//they advance
			Application.LoadLevel("levelComplete");
		else //game over
			Application.LoadLevel("gameover");
	}

	//display score and lives
	void OnGUI () 
	{
		//GUI.color = Color.black;
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.fontSize = 25;
		GUI.color = Color.yellow;//this isn't affecting anything but without it the color doesn't change on the text
		labelStyle.normal.textColor = Color.green;

		//display score for level
		GUI.Label(new Rect (5, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Level Score: "+ gamestate.Instance.ScoreCurrentLevel +"/"+ ScoreGoalForLevel.ToString(), labelStyle);

		//display score
		GUI.Label(new Rect ((Screen.width-100)/2-100, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Score: "+ gamestate.Instance.getScore(), labelStyle);

		//display high score
		GUI.Label(new Rect ((Screen.width-100)/2+100, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Hi Score: "+ gamestate.Instance.getHighScore(), labelStyle);

		//display balloons info
		labelStyle.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect (Screen.width-105, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Balloons: "+ gamestate.Instance.getLives() +" ", labelStyle);


	}
}
