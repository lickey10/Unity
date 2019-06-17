using UnityEngine;
using System.Collections;

public class LoadLevel2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel2()
    {
        gamestate.Instance.SetGameRunning(true);
        SimpleSceneFader.ChangeSceneWithFade("level2");
    }
}
