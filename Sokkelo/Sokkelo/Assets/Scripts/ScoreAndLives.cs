using UnityEngine;
using System.Collections;
using System;

public class ScoreAndLives : MonoBehaviour {
	//public GUIText GuiText; // drag here the GUIText from Hierarchy view

	GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
		gameOverPanel = GameObject.Find ("Game Over");
		gameOverPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	//display score and lives
	void OnGUI () 
	{
		GUI.color = Color.black;
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.fontSize = 25;

		//display timer
		GUI.Label(new Rect ((Screen.width-100)/2, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Timer: "+ gamestate.Instance.GetTimer(), labelStyle);

		//display coins
		labelStyle.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect (Screen.width-130, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Coins: "+ gamestate.Instance.GetCoinsCollected() +" ", labelStyle);

		//display torches lit
		GUI.Label(new Rect (45, 10 + gamestate.Instance.GetBannerHeight(), 100, 50), "Torches: "+ gamestate.Instance.GetTorchesLit() +"/4", labelStyle);

		if (!gamestate.Instance.IsGameRunning())
			gameOverPanel.SetActive (true);
	}

	public void RestartLevel()
	{
		//hide game over panel
		gameOverPanel.SetActive (false);

		//reset all temp values 
		gamestate.Instance.Reset ();

		//restart the level
		Application.LoadLevel (Application.loadedLevel);
	}
}
