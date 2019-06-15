using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material[] Skyboxes;
    public int CurrentSkyboxIndex = 0;
    public GameObject SkyNumDisplay;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSkyboxIndex = SaveSystem.GetInt("SkyboxIndex", CurrentSkyboxIndex);

        if (CurrentSkyboxIndex > Skyboxes.Length - 1)
            CurrentSkyboxIndex = 0;

        RenderSettings.skybox = Skyboxes[CurrentSkyboxIndex];

        SkyNumDisplay.GetComponent<UILabel>().text = (CurrentSkyboxIndex + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkyboxNext()
    {
        int tempIndex = CurrentSkyboxIndex + 1;

        if(tempIndex < Skyboxes.Length)//change skybox
        {
            CurrentSkyboxIndex++;
        }
        else //go to beginning then change skybox
        {
            CurrentSkyboxIndex = 0;
        }

        RenderSettings.skybox = Skyboxes[CurrentSkyboxIndex];

        SkyNumDisplay.GetComponent<UILabel>().text = (CurrentSkyboxIndex + 1).ToString();

        SaveSystem.SetInt("SkyboxIndex", CurrentSkyboxIndex);
    }
    
    public void SkyboxPrev()
    {
        int tempIndex = CurrentSkyboxIndex - 1;

        if (tempIndex < 0)//go the the end then change skybox
        {
            CurrentSkyboxIndex = Skyboxes.Length - 1;
        }
        else //change skybox
        {
            CurrentSkyboxIndex--;
        }

        RenderSettings.skybox = Skyboxes[CurrentSkyboxIndex];

        SkyNumDisplay.GetComponent<UILabel>().text = (CurrentSkyboxIndex + 1).ToString();

        SaveSystem.SetInt("SkyboxIndex", CurrentSkyboxIndex);
    }
}
