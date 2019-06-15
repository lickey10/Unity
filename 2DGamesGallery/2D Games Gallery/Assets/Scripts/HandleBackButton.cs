using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBackButton : MonoBehaviour
{
    public string SceneNameToLoad = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//this is the back button on Android
        {
            SwitchScene switchScene = new SwitchScene();
            switchScene.LoadScene(SceneNameToLoad, Color.blue);
        }
    }
}
