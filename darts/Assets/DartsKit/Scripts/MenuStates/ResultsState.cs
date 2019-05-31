using UnityEngine;
using System.Collections;


namespace DartKit
{
	public class ResultsState : MonoBehaviour {

		//the game over panel
		public GameObject gameoverPanel;

		public GUIText winnerLabel;

		public GameObject pauseButton;


		void Start()
		{
//			gameoverPanel.SetActive(false);
		}

		public void OnEnable()	
		{

			BaseGameManager.onGameOver += onGameOver;
			BaseGameManager.onButtonPress += onButtonClickCBF;


		}
		public void OnDisable()	
		{
			BaseGameManager.onGameOver -= onGameOver;
			BaseGameManager.onButtonPress -= onButtonClickCBF;

		}



		public void onGameOver(bool vic)
		{
			if(pauseButton)
			{
				pauseButton.SetActive(false);
			}

			DartGameScript gameScript = (DartGameScript)GameObject.FindObjectOfType(typeof(DartGameScript));
			if(gameScript)
			{
				if(gameScript.getNomPlayers()==1)
				{

					winnerLabel.text = "GAMEOVER";

				}else{
					string winner= "No one wins!";
					if(vic)
					{
						winner = "Player 1 Wins!";
					}else {
						winner = "Player 2 Wins!";

					}
					winnerLabel.text = winner;
				}
				
			}
			if(gameoverPanel)
			{
				gameoverPanel.SetActive(true);
			}
		}
		public void onButtonClickCBF(string buttonID)
		{
			switch (buttonID) {
			case "Restart":
				Application.LoadLevel(Application.loadedLevel);
				break;
			case "Main Menu":
				Application.LoadLevel(0);
				break;
			}
		}
	
	
	}
}