using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour 
{
	public Text txtLevel;

	private void OnEnable() {
		txtLevel.text = "Level "+PlayerPrefs.GetInt("lastUnlockedLevel",1).ToString();
	}
	public void OnPlayButtonPressed()
	{
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			UIController.Instance.LoadGamePlay();
		}
	}

	public void OnSettingButtonPressed() {
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			UIController.Instance.OpenSettings();
		}
	}
}
