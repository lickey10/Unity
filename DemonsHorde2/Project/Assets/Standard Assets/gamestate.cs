using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class gamestate : MonoBehaviour {
	
	// Declare properties
	private static gamestate instance;
	private int activeLevel = 1;                     // Active level
	private int levelProgress = 1;					//the max level the have achieved
	private int numberOfLevels = 1;						//the number of levels that exist in the game
	private int score = 0;							//the current game score
	private int highScore = 0;						//the highScore for the game since install
	private int lives = 3;							//the number of lives the player has
	private int health = 1;
	private bool gameRunning = false;				//whether the game is actually being played or not
	private int bannerHeight = 50; 				//the height of the banner to be displayed
	private bool gamePaused = false;
	private bool DisplayText = false;
	public GUIStyle customGuiStyle;
	Vector2 scrollPosition = new Vector2();
	public int MaxFontSize = 60;
	public int StartFontSize = 1;
	public bool HideAfterHitMaxSize = true;

	private int activeLevel_default = 1;                     // Active level
	private int levelProgress_default = 1;					//the max level the have achieved
	private int numberOfLevels_default = 1;						//the number of levels that exist in the game
	private int score_default = 0;							//the current game score
	private int highScore_default = 0;						//the highScore for the game since install
	private int lives_default = 3;							//the number of lives the player has
	private int health_default = 1;
	private bool gameRunning_default = false;

	public bool IsGameWon = false;
	public bool NewHighScore = false;
	public bool NewHigherLevel = false;
	public bool GotKey = false;
	private int currentLevelScore = 0;
	public int currentLevelCoins = 0;
	public int currentLevelKills = 0;
	public bool currentLevelComplete = false;
	public int currentLevelMultiplier = 1;
	public int currentLevelTreasureMultiplier = 1;
	public int Coins = 0;
	public string GameName = "";
	public ArrayList Pickups = new ArrayList();
	public string[] TextToDisplay;


	//	private string name;                            // Characters name
	//	private int maxHP;                                      // Max HP
	//	private int maxMP;                                      // Map MP
	//	private int hp;                                         // Current HP
	//	private int mp;                                         // Current MP
	//	private int str;                                        // Characters Strength
	//	private int vit;                                        // Characters Vitality
	//	private int dex;                                        // Characters Dexterity
	//	private int exp;                                        // Characters Experience Points

	
	// ---------------------------------------------------------------------------------------------------
	// gamestate()
	// ---------------------------------------------------------------------------------------------------
	// Creates an instance of gamestate as a gameobject if an instance does not exist
	// ---------------------------------------------------------------------------------------------------
	public static gamestate Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameObject("gamestate").AddComponent<gamestate> ();

				DontDestroyOnLoad(Instance);
			}
			
			return instance;
		}
	} 


	
	// Sets the instance to null when the application quits
	public void OnApplicationQuit()
	{
		instance = null;
	}
	// ---------------------------------------------------------------------------------------------------
	
	
	// ---------------------------------------------------------------------------------------------------
	// startState()
	// ---------------------------------------------------------------------------------------------------
	// Creates a new game state
	// ---------------------------------------------------------------------------------------------------
	public void StartState()
	{
		print ("Creating a new game state");
		
		// Set default properties:
		StartState(numberOfLevels,lives,levelProgress,activeLevel,health,highScore);
	
		//		name = "My Character";
		//		maxHP = 250;
		//		maxMP = 60;
		//		hp = maxHP;
		//		mp = maxMP;
		//		str = 6;
		//		vit = 5;
		//		dex = 7;
		//		exp = 0;

	}

	public void StartState(int numOfLevels, int numLives, int newLevelProgress, int newActiveLevel, int newHealth, int newHighScore)
	{
		lives = numLives;
		activeLevel = newActiveLevel;
		numberOfLevels = numOfLevels;
		levelProgress = newLevelProgress;
		health = newHealth;
		highScore = newHighScore;

		//set defaults
		activeLevel_default = newActiveLevel;                     // Active level
		levelProgress_default = newLevelProgress;					//the max level the have achieved
		numberOfLevels_default = numOfLevels;						//the number of levels that exist in the game
		score_default = 0;							//the current game score
		highScore_default = newHighScore;						//the highScore for the game since install
		lives_default = numLives;							//the number of lives the player has
		health_default = newHealth;

		if(numOfLevels == 1)//start the game if there is only one level
			Application.LoadLevel("Level1");
		else
			Application.LoadLevel("LevelMenu");
	}

	public void Reset()
	{
		lives = lives_default;
		//activeLevel = activeLevel_default;
		numberOfLevels = numberOfLevels_default;
		health = health_default;
		score = score_default;
		gameRunning = gameRunning_default;
		IsGameWon =false;
		NewHighScore = false;
		NewHigherLevel = false;
		GotKey = false;
		Coins = 0;
		currentLevelCoins = 0;
		currentLevelScore = 0;
		currentLevelKills = 0;
		DisplayText = false;
	}

	public void ResetLevel()
	{
		currentLevelCoins = 0;
		currentLevelScore = 0;
		currentLevelKills = 0;
		currentLevelMultiplier = 1;
		currentLevelTreasureMultiplier = 1;
		DisplayText = false;
	}

	void OnGUI()
	{
		if(DisplayText)
		{
			//GUILayout.BeginArea (new Rect(0, 0, Screen.width-100, Screen.height-50));
			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width-10), GUILayout.Height (Screen.height-10));
			/*changes made in the below 2 lines */
			
			//GUI.skin.box.wordWrap = true;     // set the wordwrap on for box only.
			
			
			for(int x = 0; x < TextToDisplay.Length; x++)
			{
				//GUILayout.BeginHorizontal("box");
				//GUILayout.Button("I'm the first button");//this is the image if there is any
				GUILayout.TextArea(TextToDisplay[x],customGuiStyle);        // just your message as parameter.
				//GUILayout.EndHorizontal();
			}
			
			GUILayout.EndScrollView ();
			
			//GUILayout.EndArea();
		}
	}

	public bool SetGamePaused(bool pauseGame)
	{
		if (pauseGame)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
		
		gamePaused = pauseGame;
		
		return gamePaused;
	}

	public bool GetGamePaused()
	{
		return gamePaused;
	}
	
	// ---------------------------------------------------------------------------------------------------
	// getLevel()
	// ---------------------------------------------------------------------------------------------------
	// Returns the currently active level
	// ---------------------------------------------------------------------------------------------------
	public int getActiveLevel()
	{
		return activeLevel;
	}
	
	/// <summary>
	/// Gets the max level that is unlocked
	/// </summary>
	/// <returns>The max level.</returns>
	public int getLevelProgress()
	{
		return levelProgress;
	}

	public int SetLevelProgress(int newLevelProgress)
	{
		currentLevelCoins = 0;
		currentLevelScore = 0;

		return levelProgress = newLevelProgress;
	}

	public void SetBannerHeight(int newBannerHeight)
	{
		bannerHeight = newBannerHeight;
	}

	public int GetBannerHeight()
	{
		return bannerHeight;
	}
	
	/// <summary>
	/// Gets the level count for the game
	/// </summary>
	/// <returns>The level count.</returns>
	public int GetNumberOfLevels()
	{
		return numberOfLevels;
	}

	public int SetNumberOfLevels(int numLevels)
	{
		return numberOfLevels = numLevels;
	}
	
	// ---------------------------------------------------------------------------------------------------
	// setLevel()
	// ---------------------------------------------------------------------------------------------------
	// Sets the currently active level to a new value
	// ---------------------------------------------------------------------------------------------------
	public void setActiveLevel(int newLevel)
	{
		// Set activeLevel to newLevel
		activeLevel = newLevel;

		if(activeLevel > numberOfLevels) //they won the game
		{
			IsGameWon = true;
			activeLevel = numberOfLevels;
		}
		
		if(activeLevel > levelProgress)
		{
			NewHigherLevel = true;
			levelProgress = activeLevel;
		}
	}
	
	/// <summary>
	/// Gets the score.
	/// </summary>
	/// <returns>The score.</returns>
	public int GetScore()
	{
		return score;
	}

	public int SetScore(int newScore)
	{
		currentLevelScore += newScore - score;

		if(newScore > highScore)
			SetHighScore(newScore);

		return score = newScore;
	}

	public int AddToScore(int valueToAdd)
	{
		return SetScore(score + valueToAdd);
	}

	public int GetCurrentLevelScore()
	{
		return currentLevelScore;
	}

	/// <summary>
	/// Gets the high score.
	/// </summary>
	/// <returns>The high score.</returns>
	public int getHighScore()
	{
		return highScore;
	}

	public int SetHighScore(int newHighScore)
	{
		NewHighScore = true;
		return highScore = newHighScore;
	}
	
	/// <summary>
	/// Gets the lives.
	/// </summary>
	/// <returns>The lives.</returns>
	public int getLives()
	{
		return lives;
	}

	public int SetLives(int newLives)
	{
		return lives = newLives;
	}

	public int SetHealth(int newHealth)
	{
		return health = newHealth;
	}

	public int GetHealth()
	{
		return health;
	}

	public bool IsGameRunning()
	{
		return gameRunning;
	}

	public void SetGameRunning(bool GameRunning)
	{
		gameRunning = GameRunning;
	}

	public void GUIMessage(string message)
	{
		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		customGuiStyle.normal.textColor = Color.white;
		customGuiStyle.fontSize = 1;
		customGuiStyle.wordWrap = true;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
		
		customGuiStyle.alignment = TextAnchor.MiddleCenter;
		
		GUIMessage(message,customGuiStyle,0,0.1f);
	}

	public void GUIMessage(string message, GUIStyle newGuiStyle, int StartDelay, float GrowSpeed)
	{
		
		customGuiStyle = newGuiStyle;
		
		TextToDisplay = new string[1] {message};

		DisplayText = true;
		InvokeRepeating("increaseTextSize",StartDelay,GrowSpeed);
	}

	void increaseTextSize()
	{
		customGuiStyle.fontSize +=2;
		
		if(customGuiStyle.fontSize >= MaxFontSize)
		{
			CancelInvoke("increaseTextSize");
			
			if(HideAfterHitMaxSize)
				DisplayText = false;
		}
	}


	/// <summary>
	/// Save this instance to player prefs
	/// </summary>
	public void Save(string playerPrefKey)
	{
		string message = "";

		try 
		{
//			HappyPlane.GameStateObject serializableObject = new HappyPlane.GameStateObject();
//			serializableObject.activeLevel = getActiveLevel();
//			serializableObject.gameRunning = IsGameRunning();
//			serializableObject.health = GetHealth();
//			serializableObject.highScore = getHighScore();
//			serializableObject.lives = getLives();
//			serializableObject.levelProgress = getLevelProgress();
//			serializableObject.numberOfLevels = GetNumberOfLevels();
//			serializableObject.score = getScore();
//
//			System.IO.MemoryStream memoryStream = new MemoryStream();
//			
//			bf.Serialize(memoryStream, serializableObject);
//			string tmp = System.Convert.ToBase64String(memoryStream.ToArray());
//			PlayerPrefs.SetString(playerPrefKey,tmp);




			//from video
//			FileStream file = File.Create(Application.persistentDataPath +"/playerInfo.dat");
//			bf.Serialize(file,serializeableObject);
//			file.Close();




		} 
		catch (Exception ex) 
		{
			message = ex.Message;
		}




	}


	//save all gamestate value straight to player preferences
	public void Save()
	{
		string message = "";
		
		try 
		{
			//PlayerPrefs.SetInt("activeLevel", gamestate.Instance.getActiveLevel());                     // Active level
			PlayerPrefs.SetInt("levelProgress", gamestate.Instance.getLevelProgress());					//the max level the have achieved
			//PlayerPrefs.SetInt("numberOfLevels", gamestate.Instance.GetNumberOfLevels());						//the number of levels that exist in the game
   			//PlayerPrefs.SetInt("score", gamestate.instance.getScore());							//the current game score
		   	PlayerPrefs.SetInt("highScore", gamestate.Instance.getHighScore());						//the highScore for the game since install
		    //PlayerPrefs.SetInt("lives", gamestate.Instance.getLives());							//the number of lives the player has
		    //PlayerPrefs.SetInt("health", gamestate.Instance.GetHealth());
			//PlayerPrefs.SetInt("coins", gamestate.Instance.Coins);	
		} 
		catch (Exception ex) 
		{
			message = ex.Message;
		}
		
		
		
		
	}


	public void Load(string playerPrefKey)
	{
		string message = "";

		try 
		{
			string tmp = PlayerPrefs.GetString(playerPrefKey, string.Empty);

			if(tmp != string.Empty)
			{
//				MemoryStream memoryStream = new MemoryStream(System.Convert.FromBase64String(tmp));
//				
//				HappyPlane.GameStateObject gameObject = (HappyPlane.GameStateObject)bf.Deserialize(memoryStream);
//
//				gamestate.Instance.setActiveLevel(activeLevel_default);                     // Active level
//				gamestate.Instance.SetLevelProgress(gameObject.levelProgress);					//the max level the have achieved
//				//gamestate.Instance.SetNumberOfLevels(gameObject.numberOfLevels);						//the number of levels that exist in the game
//				gamestate.Instance.SetScore(score_default);							//the current game score
//				gamestate.Instance.SetHighScore(gameObject.highScore);						//the highScore for the game since install
//				gamestate.Instance.SetLives(lives_default);							//the number of lives the player has
//				gamestate.Instance.SetHealth(health_default);
//				gamestate.Instance.SetGameRunning(gameRunning_default);
			}
		} 
		catch (Exception ex) 
		{
			message = ex.Message;
		}
	}

	//load all stats straight from player preferences
	public void Load()
	{
		string message = "";
		
		try 
		{
			int tmpHighScore = PlayerPrefs.GetInt("highScore", 0);
			
			if(tmpHighScore > 0)
			{
				//gamestate.Instance.setActiveLevel(PlayerPrefs.GetInt("activeLevel", 1));                     // Active level
				gamestate.Instance.SetLevelProgress(PlayerPrefs.GetInt("levelProgress", 1));					//the max level the have achieved
				//gamestate.Instance.SetNumberOfLevels(PlayerPrefs.GetInt("numberOfLevels", 1));				//the number of levels that exist in the game
				//gamestate.Instance.SetScore(PlayerPrefs.GetInt("score", 0));							//the current game score
				gamestate.Instance.SetHighScore(PlayerPrefs.GetInt("highScore",tmpHighScore));						//the highScore for the game since install
				//gamestate.Instance.SetLives(PlayerPrefs.GetInt("lives", 3));							//the number of lives the player has
				//gamestate.Instance.SetHealth(PlayerPrefs.GetInt("health", 1));
				//gamestate.Instance.SetHighScore(PlayerPrefs.GetInt("coins",0));
			}
		} 
		catch (Exception ex) 
		{
			message = ex.Message;
		}
	}

}