using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_colorSpin : Singleton_colorSpin<UIController_colorSpin>
{
	public GameObject HomeScreen;
    public GameObject GameScreen;
    public GameObject SettingsScreen;
    public GameObject PauseScreen;
    public GameObject LevelFailScreen;

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

    public void LoadGamePlay()
    {
        Debug.Log("Staring gameplay..");
        HomeScreen.Deactivate();
        GameScreen.Activate();
    }

    public void LoadHomeScreenFromLevelFail()
    {
        GameScreen.Deactivate();
        LevelFailScreen.Deactivate();
        HomeScreen.Activate();
    }

    public void LoadHomeScreenFromPause()
    {
        GameScreen.Deactivate();
        PauseScreen.Deactivate();
        HomeScreen.Activate();
    }

    public void RestartGame()
    {
        LevelFailScreen.Deactivate();
    }

    public void OpenSettings()
    {
        SettingsScreen.Activate();
    }

    public void CloseSettings()
    {
        SettingsScreen.Deactivate();
    }

    public void LoadLevelFail()
    {
        LevelFailScreen.Activate();
    }

    public void CloseLevelFail()
    {
        LevelFailScreen.Deactivate();       
    }

    public void LoadPauseScreen()
    {
        PauseScreen.Activate();
    }

    public void ClosePauseScreen()
    {
        PauseScreen.Deactivate();
    }
}
