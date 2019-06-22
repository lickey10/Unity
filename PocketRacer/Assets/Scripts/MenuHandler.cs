using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public float PauseDelay = 0;
    public bool RestartLevelOnDisable = false;

    RaceManager raceManager;

    // Start is called before the first frame update
    void Start()
    {
        raceManager = GameObject.FindObjectOfType<RaceManager>();
        raceManager.PauseGame(PauseDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (RestartLevelOnDisable)
            raceManager.RestartLevel();
        else//just unpause
            raceManager.UnPauseGame();
    }
}
