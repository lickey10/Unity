using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class GamePlay_Classic : MonoBehaviour {

	/// <summary>
	/// The instance.
	/// </summary>
	public static GamePlay_Classic instance;
	/// <summary>
	/// The text counter.
	/// </summary>
	public Text txtCounter;
	/// <summary>
	/// The color data. All the colores used in game stores here. This is fully dynamic and color and its game can be changed from here.
	/// </summary>
	public List<ColorData> colorData = new List<ColorData>();

	[HideInInspector] public int score = 0;
	[HideInInspector] public int highScore = 0;

	public Text txtScore;
	public Text txtHighScore;
	
	public Image colorImage;
	public Text colorName;
	public Text txtObjective;
	bool isGameStarted = false;
	float Diffuculty = 1.0F;

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
	}

	/// <summary>
	/// Starts the game play.
	/// </summary>
	/// <param name="gameMode">Game mode.</param>
	public void StartGamePlay(GameMode _gameMode)
	{
		bool hasRescued = PlayerPrefs.GetInt("hasRescued",0) != 0;
		PlayerPrefs.SetInt ("hasRescued", 0);

		isGameStarted = false;
		highScore = PlayerPrefs.GetInt ("HighScore_" + GameUIController.ActiveGameMode.ToString(),0);
		txtHighScore.text = highScore.ToString ("00");

		if (!hasRescued) {
			score = 0;
			txtScore.text = score.ToString ("00");
			Diffuculty = 1.0F;
			colorImage.GetComponent<Animator> ().speed = Diffuculty;
		}

		txtCounter.gameObject.SetActive (true);
		txtCounter.GetComponent<Counter> ().StartCounter ();
		txtObjective.gameObject.SetActive (true);
		txtObjective.text = "True or false?";
	}

	/// <summary>
	/// Starts the fill circle.
	/// This method will be called firt time from Counter.cs once the 321Go Counter Gets completed.
	/// </summary>
	public void StartFillCircle()
	{
		Invoke ("StartTimer", 0.5F);
	}

	/// <summary>
	/// Gets the random color data.
	/// </summary>
	/// <returns>The random color data.</returns>
	public ColorData GetRandomColorData()
	{
		int RandomColorIndex = 0;
		if (score <= 20) {
			RandomColorIndex = UnityEngine.Random.Range (0, 4);
		} else {
			RandomColorIndex = UnityEngine.Random.Range (0, 8);
		}

		return colorData [RandomColorIndex];
	}

	/// <summary>
	/// Raises the true button pressed event.
	/// </summary>
	public void OnAnswerButtonPressed(bool userAnswer)
	{
		if (isGameStarted) {
			bool result = CheckResult (userAnswer);
			
			if (result == true) {
				StartTimer ();
				UpdateScore(1);
				UpdateDifficulty();
			} else {
				OnGameOver();
			}
			AudioManager.instance.PlayAnswerSound ();
		}
	}

	/// <summary>
	/// Updates the score.
	/// </summary>
	/// <param name="scoreUpdate">Score update.</param>
	public void UpdateScore(int scoreUpdate)
	{
		score += scoreUpdate;
		txtScore.text = score.ToString ("00");

		if (score > highScore) {
			highScore = score;
			txtHighScore.text = highScore.ToString("00");
			PlayerPrefs.SetInt ("HighScore_" + GameUIController.ActiveGameMode.ToString (), highScore);
		}
	}

	/// <summary>
	/// Raises the game over event.
	/// </summary>
	public void OnGameOver()
	{
		isGameStarted = false;
		colorName.text = "";
		colorImage.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("LastScore_" + GameUIController.ActiveGameMode.ToString (), score);
		GameUIController.instance.LoadGameOverScreen ();
	}

	/// <summary>
	/// Starts the timer.
	/// </summary>
	public void StartTimer()
	{
		colorImage.gameObject.SetActive (true);
		txtObjective.gameObject.SetActive (false);
		GenerateRandomColor ();
		colorImage.GetComponent<Animator> ().SetTrigger ("FillProgressBar");
		isGameStarted = true;
	}

	/// <summary>
	/// Resets the circle.
	/// </summary>
	public void ResetCircle()
	{
		txtObjective.gameObject.SetActive (false);
		GenerateRandomColor ();
		colorImage.GetComponent<Animator> ().SetTrigger ("FillProgressBar");
	}
	
	/// <summary>
	/// Generates the random color.
	/// </summary>
	void GenerateRandomColor()
	{
		int ResultData = UnityEngine.Random.Range (0, 4);
		
		ColorData circleColor = GetRandomColorData ();
		ColorData circleName = GetRandomColorData ();


		if (ResultData == 0) {
			colorImage.color = circleColor.color;
			colorName.text = circleColor.colorName;
			colorName.color = circleColor.color;
		} 
		else 
		{
			colorImage.color = circleColor.color;
			colorName.text = circleName.colorName;
			colorName.color = circleColor.color;
		}
	}
	
	/// <summary>
	/// Checks the result.
	/// </summary>
	/// <returns><c>true</c>, if result was checked, <c>false</c> otherwise.</returns>
	/// <param name="userAnswer">If set to <c>true</c> user answer.</param>
	public bool CheckResult(bool userAnswer)
	{
		ColorData data =  colorData.Find(item => item.colorName == colorName.text.ToString());
		
		if (data.color == colorImage.color) {
			return(userAnswer == true) ? true : false;
		} else {
			return (userAnswer == false) ? true : false;
		}
	}

	/// <summary>
	/// Updates the difficulty.
	/// </summary>
	public void UpdateDifficulty()
	{
		Diffuculty += 0.05F;
		colorImage.GetComponent<Animator> ().speed = Diffuculty;
	}
}
