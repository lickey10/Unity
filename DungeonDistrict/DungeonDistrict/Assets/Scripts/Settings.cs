using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject WelcomePanel;
    public GameObject DungeonInfoPopup;
    public GameObject RPGSchedulePopup;
    public GameObject DungeonRPGScheduleListPopup;

    bool showWelcomePanel = true;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            int tempInt = PlayerPrefs.GetInt("showWelcomePanel");

            if (tempInt == 1 || !PlayerPrefs.HasKey("showWelcomePanel"))
                showWelcomePanel = true;
            else
                showWelcomePanel = false;
        }
        catch (System.Exception)
        {

        }

        DungeonHandler.SaveDungeons(DungeonHandler.GetDungeonsList());

        WelcomePanel.SetActive(showWelcomePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideWelcomePanel()
    {
        showWelcomePanel = false;
        PlayerPrefs.SetInt("showWelcomePanel", 0);
        WelcomePanel.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
