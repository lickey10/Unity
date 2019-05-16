using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsScreen : MonoBehaviour 
{

	int currentLevel = 0;
	int lastUnlockedLevel = 0;

	[SerializeField] private Text txtLevel;
	[SerializeField] private Text txtSelectedLevel;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		currentLevel = PlayerPrefs.GetInt("currentLevel",1);
		lastUnlockedLevel = PlayerPrefs.GetInt("lastUnlockedLevel",1);
		txtSelectedLevel.text = "LEVEL "+currentLevel;
		txtLevel.text = "Level "+lastUnlockedLevel;
	}

	public void OnHomeButtonPressed() {
		if(InputManager.Instance.canInput()) {
			AudioManager.Instance.PlayButtonClickSound();
			UIController.Instance.CloseSettings();
		}
	}

	public void OnCurrentLevelReduced()
	{
		currentLevel = (currentLevel - 1);
		if(currentLevel <= 0)
		{
			currentLevel = 1;
		}
		PlayerPrefs.SetInt("currentLevel",currentLevel);
		txtSelectedLevel.text = "LEVEL "+currentLevel.ToString();
	}

	public void OnCurrentLevelIncreased()
	{
		currentLevel = (currentLevel + 1);
		if(currentLevel > lastUnlockedLevel)
		{
			currentLevel = lastUnlockedLevel;
		}
		PlayerPrefs.SetInt("currentLevel",currentLevel);
		txtSelectedLevel.text = "LEVEL "+currentLevel.ToString();
	}
}