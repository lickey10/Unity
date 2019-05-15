using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour 
{
	[SerializeField] private Text txtLevel;
	
	void OnEnable()
	{
		int currentLevel = PlayerPrefs.GetInt("currentLevel",1);
		txtLevel.text = "Level " + currentLevel.ToString();
	}

	public void OnHomeButtonPressed() {
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			UIController.Instance.LoadHomeScreenFromPause();
		}
	}

	public void OnReplayButtonPressed()
	{
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			GamePlay.Instance.ReplayLevel();
			UIController.Instance.ClosePauseScreen();
		}
	}

	public void OnContinueButtonPressed()
	{
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			UIController.Instance.ClosePauseScreen();
		}
	}
}
