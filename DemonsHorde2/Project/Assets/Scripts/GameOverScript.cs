using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	public GUIStyle customGuiStyle;
	public Sprite[] Backgrounds;
	public Color TextColor = Color.green;
	public Color TextShadowColor = Color.black;

	int logoX;
	int logoY;


	void Start()
	{
		gamestate.Instance.SetGameRunning(false);
		
		try {
			//save gamestate to player prefs
			//gamestate.Instance.Save("GameState");
			gamestate.Instance.Save();
		} catch (System.Exception ex) {
			string message = ex.Message;
		}

		//set background
		GameObject.FindGameObjectWithTag("background").GetComponent<SpriteRenderer>().sprite = Backgrounds[gamestate.Instance.getActiveLevel() - 1];
	}

	void OnStart()
	{
//		gamestate.Instance.SetGameRunning(false);
//		
//		try {
//			//save gamestate to player prefs
//			gamestate.Instance.Save("GameState");
//		} catch (System.Exception ex) {
//			string message = ex.Message;
//		}

		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		customGuiStyle.normal.textColor = Color.green;
		customGuiStyle.fontSize = 50;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
	}

	// Update is called once per frame
	void Update () {


	}

	void OnGUI()
	{
		logoX = (Screen.width - 300 ) / 2;
		logoY = (Screen.height - 450) / 2;

		//logo
		//drop shadow
		customGuiStyle.normal.textColor = TextShadowColor;
		GUI.Box(new Rect( logoX+3, logoY+3, 450, 30 ), "Game Over!" ,customGuiStyle);
		
		customGuiStyle.normal.textColor = TextColor;
		GUI.Box(new Rect( logoX, logoY, 450, 30 ), "Game Over!" ,customGuiStyle);
	}
}
