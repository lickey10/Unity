using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class GamePlay_TapColor : MonoBehaviour {

	/// <summary>
	/// The instance.
	/// </summary>
	public static GamePlay_TapColor instance;

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
	public Text txtObjective;

	public GameObject ColorPanel;
	public GameObject ProgressBarPanel;
	
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
			ProgressBarPanel.transform.GetChild(0).GetComponent<Animator> ().speed = Diffuculty;
		}

		txtCounter.gameObject.SetActive (true);
		txtCounter.GetComponent<Counter> ().StartCounter ();
		txtObjective.gameObject.SetActive (true);
		isGameStarted = false;
		txtObjective.text = "Tap the true color!";
	}

	/// <summary>
	/// Starts the fill circle.
	/// This method will be called firt time from Counter.cs once the 321Go Counter Gets completed.
	/// </summary>
	public void StartFillCircle()
	{
		Invoke ("StartTimer", 0.5F);
	}

	public void OnAnswerButtonPressed(Button btn)
	{
		if (isGameStarted) {
			Color btnColor = btn.GetComponent<Image> ().color;
			bool result = CheckResult (btnColor, btn.transform.GetChild(0).GetComponent<Text>().text);
			
			if (result == true) {
				StartTimer ();
				UpdateScore (1);
				UpdateDifficulty ();
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
		ColorPanel.SetActive (false);
		ProgressBarPanel.transform.GetChild (0).GetComponent<Animator>().enabled = false;
		ProgressBarPanel.SetActive (false);
		PlayerPrefs.SetInt ("LastScore_" + GameUIController.ActiveGameMode.ToString (), score);

		GameUIController.instance.LoadGameOverScreen ();
	}

	/// <summary>
	/// Starts the timer.
	/// </summary>
	public void StartTimer()
	{
		ProgressBarPanel.SetActive (true);
		ProgressBarPanel.transform.GetChild (0).GetComponent<Animator>().enabled = true;
		txtObjective.gameObject.SetActive (false);
		GenerateRandomColor ();
		ProgressBarPanel.transform.GetChild(0).GetComponent<Animator> ().SetTrigger ("FillProgressBar_TapColor");
		isGameStarted = true;
	}

	/// <summary>
	/// Resets the circle.
	/// </summary>
	public void ResetCircle()
	{
		txtObjective.gameObject.SetActive (false);
		GenerateRandomColor ();
		ProgressBarPanel.transform.GetChild(0).GetComponent<Animator> ().SetTrigger ("FillProgressBar_TapColor");
	}
	
	/// <summary>
	/// Generates the random color.
	/// </summary>
	void GenerateRandomColor()
	{
		List<int> numberList;
		if (score < 10) {
			numberList = new List<int> (){0,1,2,3};
		} else 
		{
			numberList = new List<int>(){0,1,2,3,4,5,6,7};	
		}

		ShuffleGenericList (numberList);
		int RandomAnswerIndex = UnityEngine.Random.Range(0,4);

		for (int colorIndex = 0; colorIndex < 4; colorIndex++) {
			ColorData data = colorData[numberList[colorIndex]];
			Transform colorOption = ColorPanel.transform.GetChild(colorIndex);
			colorOption.GetComponent<Image>().color = data.color;

			colorOption.GetChild(0).GetComponent<Text>().text = colorData[numberList[RandomAnswerIndex]].colorName;
			colorOption.GetChild(0).GetComponent<Text>().color = colorData[numberList[colorIndex]].color;
		}

		ColorPanel.SetActive (true);
	}

	/// <summary>
	/// Checks the result.
	/// </summary>
	/// <returns><c>true</c>, if result was checked, <c>false</c> otherwise.</returns>
	/// <param name="btnColor">Button color.</param>
	/// <param name="btnColorName">Button color name.</param>
	public bool CheckResult(Color btnColor, string btnColorName)
	{
		ColorData data =  colorData.Find(item => item.colorName == btnColorName);
		return (btnColor == data.color) ? true : false;
	}
	
	/// <summary>
	/// Updates the difficulty.
	/// </summary>
	public void UpdateDifficulty()
	{
		Diffuculty += 0.05F;
		ProgressBarPanel.transform.GetChild(0).GetComponent<Animator> ().speed = Diffuculty;
	}

	/// <summary>
	/// Shuffles the generic list.
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void ShuffleGenericList<T>(List<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}