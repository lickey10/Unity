using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour 
{
	/// <summary>
	/// The Singleton Instance of the GameUIController.
	/// </summary>
	public static GameUIController instance;
	public static GameMode ActiveGameMode;
	public GameObject MainScreen;
	public GameObject SelectModeScreen;
	public GameObject GamePlayScreen_Classic;
	public GameObject GamePlayScreen_Chrono;
	public GameObject GamePlayScreen_FindColor;
	public GameObject GamePlayScreen_TapColor;
	public GameObject GameOverScreen;
	public GameObject QuitConfirmScreen;

	[HideInInspector]	public GameObject CurrentActiveScreen;
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
		Destroy (gameObject);
		Application.targetFrameRate = 60;
		CurrentActiveScreen = MainScreen;
	}

	/// <summary>
	/// Loads the game play.
	/// </summary>
	/// <param name="gameMode">Game mode.</param>
	public void LoadGamePlay(GameMode gameMode)
	{
		MainScreen.GetComponent<Animator> ().SetTrigger ("fadeout");
		SelectModeScreen.SetActive (false);
		StartCoroutine (ActivateGamePlay (gameMode));
	}

	/// <summary>
	/// Loads the select mode screen and activate it.
	/// </summary>
	public void LoadSelectModeScreen()
	{
		if (SelectModeScreen != null) {
			SelectModeScreen.SetActive(true);
			CurrentActiveScreen = SelectModeScreen;
		}
	}
	/// </summary>
	/// <returns>The game play.</returns>
	/// <param name="gameMode">Game mode.</param>
	IEnumerator ActivateGamePlay(GameMode gameMode, bool wait = true)
	{
		ActiveGameMode = gameMode;

		yield return new WaitForSeconds((wait) ? 2F : 0F);

		switch (ActiveGameMode) {
		case GameMode.Classic:
			GamePlayScreen_Classic.SetActive (true);
			GamePlayScreen_Classic.GetComponent<GamePlay_Classic> ().StartGamePlay (gameMode);
			MainScreen.SetActive (false);
			CurrentActiveScreen = GamePlayScreen_Classic;
			break;
		case GameMode.Chrono:
			GamePlayScreen_Chrono.SetActive (true);
			GamePlayScreen_Chrono.GetComponent<GamePlay_Chrono> ().StartGamePlay (gameMode);
			MainScreen.SetActive (false);
			CurrentActiveScreen = GamePlayScreen_Chrono;
			break;
		case GameMode.Findcolor:
			GamePlayScreen_FindColor.SetActive (true);
			GamePlayScreen_FindColor.GetComponent<GamePlay_FindColor> ().StartGamePlay (gameMode);
			MainScreen.SetActive (false);
			CurrentActiveScreen = GamePlayScreen_FindColor;
			break;
		case GameMode.Tapcolor:
			GamePlayScreen_TapColor.SetActive (true);
			GamePlayScreen_TapColor.GetComponent<GamePlay_TapColor> ().StartGamePlay (gameMode);
			MainScreen.SetActive (false);
			CurrentActiveScreen = GamePlayScreen_TapColor;
			break;
		}
	}

	/// <summary>
	/// Loads the game over screen.
	/// </summary>
	public void LoadGameOverScreen()
	{
		if (GameOverScreen != null) {
			GameOverScreen.SetActive(true);
			CurrentActiveScreen = GameOverScreen;
		}
	}

	/// <summary>
	/// Loads the main screen.
	/// </summary>
	public void LoadMainScreen()
	{
		MainScreen.SetActive (true);
		GamePlayScreen_Classic.SetActive (false);
		GamePlayScreen_Chrono.SetActive (false);
		GamePlayScreen_FindColor.SetActive (false);
		GamePlayScreen_TapColor.SetActive (false);
		GameOverScreen.SetActive (false);
		CurrentActiveScreen = MainScreen;
	}

	public void OnReplayGame()
	{
		GameOverScreen.SetActive (false);
		StartCoroutine (ActivateGamePlay (ActiveGameMode,false));
	}

	/// <summary>
	/// Raises the rescue game event.
	/// </summary>
	public void OnRescueGame()
	{
		GameOverScreen.SetActive (false);
		StartCoroutine (ActivateGamePlay (ActiveGameMode,false));
	}


	public void LoadQuitConfirm()
	{
		QuitConfirmScreen.SetActive(true);
		CurrentActiveScreen = QuitConfirmScreen;
	}
	/// <summary>
	/// Raises the quit confirm cancel event.
	/// </summary>
	public void OnQuitConfirmCancel()
	{
		QuitConfirmScreen.SetActive (false);
		CurrentActiveScreen = MainScreen;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(InputManager.instance.canInput())
			{
				HandleBackButton();
			}
		}
	}

	/// <summary>
	/// Handles the back button.
	/// </summary>
	void HandleBackButton()
	{
		switch (CurrentActiveScreen.name) {
		case "MainScreen":
			LoadQuitConfirm ();
			break;
		case "SelectMode":
			SelectModeScreen.SetActive(false);
			CurrentActiveScreen = MainScreen;
			break;
		case "Quit-Confirm":
			OnQuitConfirmCancel();
			break;
		case "GameOver":
			LoadMainScreen();
			break;
		}

	}
}