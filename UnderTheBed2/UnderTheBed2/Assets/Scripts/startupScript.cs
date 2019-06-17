using UnityEngine;
using System.Collections;

public class startupScript : MonoBehaviour {
	public Transform StartPosition;
    public GameObject DefaultCharacter;

    // Use this for initialization
    void Start()
    {
        if (gamestate.Instance.CurrentCharacter == null)
        {
            gamestate.Instance.CurrentCharacter = DefaultCharacter;
            //DefaultCharacter = (GameObject)Instantiate(DefaultCharacter, StartPosition.transform.position, DefaultCharacter.transform.rotation);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformCamera>().Hero = DefaultCharacter.transform;
            //GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<CameraController_m>().Player = DefaultCharacter.transform;
        }
        else
        {
            //gamestate.Instance.CurrentCharacter = gamestate.Instance.CurrentCharacter;
            
        }

        gamestate.Instance.CurrentCharacter = (GameObject)Instantiate(gamestate.Instance.CurrentCharacter, StartPosition.transform.position, gamestate.Instance.CurrentCharacter.transform.rotation);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformCamera>().Hero = gamestate.Instance.CurrentCharacter.transform;
        //GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<CameraController_m>().Player = gamestate.Instance.CurrentCharacter.transform;
    }

    // Update is called once per frame
    void Update () {
	
	}

	void OnGUI()
	{
//		if(VariableManager.Instance == null)
//			VariableManager.Instantiate(this);

//		if(!VariableManager.Instance.GameStarted)
//		{
//			Time.timeScale = 0; 
//			const int buttonWidth = 120;
//			const int buttonHeight = 90;
//
////			GUI.color = Color.green;
////
////			//logo
////			GUI.DrawTexture(new Rect(Screen.width / 2 - 150,
////			                         30,
////			                         400,
////			                         200), logo, ScaleMode.ScaleToFit, true, 0.0F);
//
//
//			GUI.color = Color.white;
//			if (
//				GUI.Button(
//				// Center in X, 1/3 of the height in Y
//				new Rect(
//				Screen.width / 2 - (buttonWidth / 2),
//				(1 * Screen.height / 3) - (buttonHeight / 2),
//				buttonWidth,
//				buttonHeight
//				),
//				"Start!!"
//				)
//				)
//			{
//					Time.timeScale = 1;
//					VariableManager.Instance.GameStarted = true;
//			}
//		}


	}
}
