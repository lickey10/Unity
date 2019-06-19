using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Restart()
	{
		if(!gamestate.Instance.IsGameRunning())
		{
			//the game isn't running so reset variables
			gamestate.Instance.Reset();
			Application.LoadLevel("logo");
		}
		else if(!gamestate.Instance.IsGameWon)
		{
			// start the game
			Application.LoadLevel("level"+ gamestate.Instance.getActiveLevel());
		}
		else //they won the game
		{
			gamestate.Instance.SetGameRunning(false);
			Application.LoadLevel("youWon");
		}
	}
}
