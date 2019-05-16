using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Select mode.
/// </summary>
public class GameOver : MonoBehaviour 
{
	public Text txtTitle;
	public Text txtScore;
	public Text txtHighScore;
	public Button btnRescue;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		AudioManager.instance.PlayGameOverSound ();
		btnRescue.gameObject.SetActive (false);
		switch (GameUIController.ActiveGameMode) {
		case GameMode.Classic:
			txtTitle.text = "CLASSIC MODE";
			break;
		case GameMode.Chrono:
			txtTitle.text = "CHRONO MODE";
			break;
		case GameMode.Findcolor:
			txtScore.text = "FIND THE COLOR";
			break;
		case GameMode.Tapcolor:
			txtScore.text = "TAP THE COLOR";
			break;
		}

		int Score = PlayerPrefs.GetInt ("LastScore_" + GameUIController.ActiveGameMode.ToString (), 0);
		int HighScore = PlayerPrefs.GetInt ("HighScore_" + GameUIController.ActiveGameMode.ToString (), 0);

		txtScore.text = Score.ToString ("00");
		txtHighScore.text = "HIGH SCORE : " + HighScore.ToString ("00");
	}

	/// <summary>
	/// Raises the replay button pressed event.
	/// </summary>
	public void OnReplayButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.OnReplayGame();
		}
	}

	/// <summary>
	/// Raises the rescue button pressed event.
	/// </summary>
	public void OnRescueButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.OnReplayGame();
		}
	}

	/// <summary>
	/// Raises the home button pressed event.
	/// </summary>
	public void OnHomeButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.LoadMainScreen();
			PlayerPrefs.SetInt("hasRescued",0);
		}
	}
}
