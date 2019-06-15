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
		if(InputManager_colorSpin.Instance.canInput()) {
            AudioManager_colorSpin.Instance.PlayButtonClickSound();
            UIController_colorSpin.Instance.LoadHomeScreenFromPause();
		}
	}

	public void OnReplayButtonPressed()
	{
		if(InputManager_colorSpin.Instance.canInput()) {
            AudioManager_colorSpin.Instance.PlayButtonClickSound();
			GamePlay.Instance.ReplayLevel();
            UIController_colorSpin.Instance.ClosePauseScreen();
		}
	}

	public void OnContinueButtonPressed()
	{
		if(InputManager_colorSpin.Instance.canInput()) {
            AudioManager_colorSpin.Instance.PlayButtonClickSound();
            UIController_colorSpin.Instance.ClosePauseScreen();
		}
	}
}
