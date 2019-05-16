using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour 
{

	GameObject DataManager;

	SaveDataScript SDS;
	ThemeChangeScript CGS;

	int CurrentScore, HighScore;

	[SerializeField]
	Text ScoreText;
	[SerializeField]
	Text BestScoreText;

	public Text PlayButtonText;
	public Text MenuButtonText;
	public Image PlayButtonImage;
	public Image MenuButtonImage;

	void Start () 
	{
		DataManager = GameObject.FindGameObjectWithTag ("DM");	
		SDS = DataManager.GetComponent<SaveDataScript> ();
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		ScoreText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		BestScoreText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);

		PlayButtonText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		MenuButtonText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);

		PlayButtonImage.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		MenuButtonImage.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		Camera.main.backgroundColor = new Color32 (CGS.R3, CGS.G3, CGS.B3,255);

		CurrentScore = SDS.CurrentScore;
		HighScore = SDS.GetHighScore ();

		ScoreText.text = "Score: " + CurrentScore.ToString ();

		if (CurrentScore > HighScore) 
		{
			HighScore = CurrentScore;

			SDS.SetHighScore (CurrentScore);
			SDS.Save ();
		}

		BestScoreText.text = "Best: " + HighScore.ToString ();
	}

	public void Play()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (2);
	}

	public void MainMenu () 
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (1);
	}
}
