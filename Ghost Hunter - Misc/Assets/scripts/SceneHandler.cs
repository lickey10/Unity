using UnityEngine;
using System.Collections;

public class SceneHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayInfoScene()
    {
        Application.LoadLevel("InfoScene");
    }

    public void DisplayMiscData()
    {
        Application.LoadLevel("main");
    }
}
