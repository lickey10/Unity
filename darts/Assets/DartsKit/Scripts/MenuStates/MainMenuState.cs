using UnityEngine;
using System.Collections;
namespace DartKit
	{
	public class MainMenuState : MonoBehaviour {

		public TouchButton2 graphicsQuality;
		public GameObject mainPanel;
		public GameObject optionsPanel;
		public GameObject configPanel;

		public TouchButton2 aiDifficultyButton;
		public TouchButton2 enemyButton;

		public Texture[] aiLevels;
		public Texture[] enemyTextures;
		public Texture[] graphicsTextures;


		public Material cabinentMat;
		public Material dartboardMat;
		public int lvltoLoad = 1;


		void Start()
		{
			updateGraphicsQuality();
			updateAI();
			updateEnemy();

		}
		public void OnEnable()
		{
			BaseGameManager.onButtonPress += onButtonClickCBF;
		}
		public void OnDisable()
		{
			BaseGameManager.onButtonPress -= onButtonClickCBF;
		}

		public void setConifg(int gameType)
		{
			if(configPanel)
				configPanel.SetActive(true);
			if(mainPanel)
				mainPanel.SetActive(false);
			Constants.setGameType(gameType);
			gameType++;
			cabinentMat.mainTexture = Resources.Load ( "cabinet0" + gameType) as Texture;
			dartboardMat.mainTexture = Resources.Load ( "dartboard0" + gameType) as Texture;


		}
		public void onButtonClickCBF(string buttonID)
		{
			switch (buttonID) 
			{
			case "301":
				setConifg(0);
				break;
			case "501":
				setConifg(1);
				break;
			case "Around The World":
				setConifg(2);
				break;

				
			case "Player2Toggle":
				toggleEnemies();
				//BaseGameManager.connect(true,1,false);
				break;
			case "StartGame":
				handleStartGame();
				//BaseGameManager.connect(true,1,false);
				break;
			case "AIToggle":
			{
				toggleAIDifficulty();
			}
				break;

			case "Options":
				if(optionsPanel)
					optionsPanel.SetActive(true);
				if(mainPanel)
					mainPanel.SetActive(false);
				break;
			case "ConfigBack":
				if(configPanel)
					configPanel.SetActive(false);
				if(mainPanel)	
					mainPanel.SetActive(true);
				break;
			case "OptionsBack":
				if(optionsPanel)
					optionsPanel.SetActive(false);
				if(mainPanel)	
					mainPanel.SetActive(true);
				break;
				
			case "GraphicsToggle":
				toggleQuality();
				break;
			}
		}
		public int getLevelToLoad()
		{
			return PlayerPrefs.GetInt("Alley",0) + 1;
		}

		void handleStartGame()
		{
			int enemy = PlayerPrefs.GetInt("Enemy",0);
			int nomHumans = 1;
			int nomAI = 0;
			if(enemy==1)
			{
				nomAI = 1;
			}

			if(enemy == 2)
			{
				nomHumans=2;
			}
			BaseGameManager.connect(true,getLevelToLoad(),nomHumans,nomAI);
		}

	

		void toggleEnemies()
		{
			int val = PlayerPrefs.GetInt("Enemy",0);
			val++;
			if(val>=enemyTextures.Length)
			{
				val=0;
			}
			PlayerPrefs.SetInt("Enemy",val);
			updateEnemy();
		}
		void updateEnemy()
		{
			int enemy = PlayerPrefs.GetInt("Enemy",0);
			if(enemyButton)
			{	
				if(enemy>-1 && enemy<enemyTextures.Length)
				{
					enemyButton.setTexture(enemyTextures[enemy]);
				}
			}
		}




		void toggleAIDifficulty()
		{
			int ai = PlayerPrefs.GetInt("AIDifficultyX",1);
			ai++;
			if(ai>=aiLevels.Length)
			{
				ai=0;
			}

			PlayerPrefs.SetInt("AIDifficultyX",ai);

			updateAI();
		}
		void updateAI()
		{
			int ai = PlayerPrefs.GetInt("AIDifficultyX",1);
			Debug.Log ("AI" + ai);
			if(aiDifficultyButton)
			{	
				if(ai>-1 && ai<aiLevels.Length)
				{
					aiDifficultyButton.setTexture(aiLevels[ai]);
				}
			}
		}
	
		public void toggleQuality()
		{
			int currentQuality = QualitySettings.GetQualityLevel();

			if(currentQuality==0)
			{
				QualitySettings.SetQualityLevel(1);		
			}
			else if(currentQuality==1)
			{
				QualitySettings.SetQualityLevel(2);		
			}
			else if(currentQuality==2)
			{
				QualitySettings.SetQualityLevel(0);		
			}
			Debug.Log ("toggleQuality" + QualitySettings.GetQualityLevel() + " oldquality " + currentQuality);
			updateGraphicsQuality();
		}

		void updateGraphicsQuality()
		{
			if(graphicsQuality)
			{
				graphicsQuality.setTexture(graphicsTextures[QualitySettings.GetQualityLevel()]);// = "Graphics: "+ QualitySettings.names[QualitySettings.GetQualityLevel()];
			}
		}
	}

}