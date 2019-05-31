using UnityEngine;
using System.Collections;

public class InstructionMenuHandler : MonoBehaviour {

    public UIPanel uiPanel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void PlayButtonPressed()
    {
        uiPanel.alpha = 0;
    }
}
