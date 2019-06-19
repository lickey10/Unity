using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {
	public GameObject InfoPanel;
	public Text TextArea;
	public Text TextAreaShadow;
	public Text LevelText;
	public Text LevelTextShadow1;
	public Text LevelTextShadow2;
	public Text LevelTextShadow3;
	public Text LevelTextShadow4;


	// Use this for initialization
	void Start () {
		gamestate.Instance.DisplayingMenu = true;

		TextArea.text = TextArea.text.Replace("$NumBalloons$", gamestate.Instance.getLives().ToString());
		TextArea.text = TextArea.text.Replace("$Points$", gamestate.Instance.ScoreGoalForCurrentLevel.ToString());

		TextAreaShadow.text = TextArea.text.Replace("$NumBalloons$", gamestate.Instance.getLives().ToString());

		if(gamestate.Instance.ScoreGoalForCurrentLevel < 0)
			gamestate.Instance.ScoreGoalForCurrentLevel = gamestate.Instance.getActiveLevel() * 200;

		TextAreaShadow.text = TextArea.text.Replace("$Points$", gamestate.Instance.ScoreGoalForCurrentLevel.ToString());

		LevelText.text = "Level " + gamestate.Instance.getActiveLevel ();
		LevelTextShadow1.text = "Level " + gamestate.Instance.getActiveLevel ();
		LevelTextShadow2.text = "Level " + gamestate.Instance.getActiveLevel ();
		LevelTextShadow3.text = "Level " + gamestate.Instance.getActiveLevel ();
		LevelTextShadow4.text = "Level " + gamestate.Instance.getActiveLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HideInfoPanel()
	{
		//hide infoPanel
		InfoPanel.SetActive(false);

		gamestate.Instance.DisplayingMenu = false;
	}
}
