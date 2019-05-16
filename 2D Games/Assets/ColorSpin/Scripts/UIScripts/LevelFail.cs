using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFail : MonoBehaviour 
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
			UIController.Instance.LoadHomeScreenFromLevelFail();
		}
	}

	//Restarts game.
	public void OnReplayButtonPressed()
	{
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			GamePlay.Instance.ReplayLevel();
			UIController.Instance.CloseLevelFail();
		}
	}

	///Use this code if you want to use rescue fuctionality, this code will continue game from the state whereit was left on level fail.
	public void OnRescuButtonPressed()
	{
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			GamePlay.Instance.ReplayLevel(true);
			UIController.Instance.CloseLevelFail();
		}
	}
}
