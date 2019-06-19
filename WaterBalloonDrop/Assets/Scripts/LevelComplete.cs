using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {
	public GUIStyle customGuiStyle;
	public Sprite[] Backgrounds;
	
	int logoX;
	int logoY;

	// Use this for initialization
	void Start () {
		customGuiStyle = new GUIStyle();
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		customGuiStyle.normal.textColor = Color.green;
		customGuiStyle.fontSize = 50;
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
		customGuiStyle.alignment = TextAnchor.MiddleCenter;

		
		logoX = (Screen.width - 500 ) / 2;
		logoY = (Screen.height - 300) / 2;

		//setBackgroundForLevel();

		//gamestate.Instance.setActiveLevel(gamestate.Instance.getActiveLevel()+1);
	}

	void setBackgroundForLevel()
	{
		GameObject.FindGameObjectWithTag("background").GetComponent<SpriteRenderer>().sprite = Backgrounds[gamestate.Instance.getActiveLevel() - 1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{

		customGuiStyle.normal.textColor = Color.green;
		GUI.Box(new Rect( logoX, 55, 450, 30 ), "Score:"+ gamestate.Instance.ScoreCurrentLevel +"/"+ gamestate.Instance.ScoreGoalForCurrentLevel ,customGuiStyle);

		customGuiStyle.normal.textColor = Color.red;
		GUI.Box(new Rect( logoX-2, 53, 450, 30 ), "Score:"+ gamestate.Instance.ScoreCurrentLevel +"/"+ gamestate.Instance.ScoreGoalForCurrentLevel ,customGuiStyle);
		
//		customGuiStyle.normal.textColor = Color.green;
//		GUI.Box(new Rect( logoX, logoY, 450, 30 ), "Level Complete!" ,customGuiStyle);


	}

	public void LoadNextLevel()
	{
		gamestate.Instance.setActiveLevel (gamestate.Instance.getActiveLevel () + 1);
		gamestate.Instance.SetLives (10);
		gamestate.Instance.ScoreCurrentLevel = 0;

		if (gamestate.Instance.getActiveLevel () > gamestate.Instance.GetNumberOfLevels ())//speed up the game
			Time.timeScale += ((float).05 * gamestate.Instance.getActiveLevel ());

		if (gamestate.Instance.getActiveLevel () <= gamestate.Instance.GetNumberOfLevels ()) {//they advance
			Application.LoadLevel ("level" + gamestate.Instance.getActiveLevel ());
		}
		else
			Application.LoadLevel ("level" + gamestate.Instance.GetNumberOfLevels());
	}
}
