using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour 
{
	bool IsPaused;
	public GameObject PausePanel;

	public Text PauseText;
	public Text ScoreText;
	public Text PauseButtonText;
	public Image PauseButtonImage;

	public Image PlayButtonImage;
	public Text PlayButtonText;
	public Image MenuButtonImage;
	public Text MenuButtonText;

	[SerializeField]
	GameObject Clock;
	ClockScript CSK;

	GameObject DataManager;
	ThemeChangeScript CGS;

	void Start () 
	{
		CSK = Clock.GetComponent<ClockScript> ();
		DataManager = GameObject.FindGameObjectWithTag ("DM");
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		PauseText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		ScoreText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		PauseButtonText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		PauseButtonImage.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);

		PlayButtonImage.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		PlayButtonText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		MenuButtonImage.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		MenuButtonText.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	public void Pause()
	{
		if (IsPaused) 
		{
			PausePanel.SetActive (false);
			Time.timeScale = 1;
			CSK.AllowTouch = true;
		} 
		else 
		{
			PausePanel.SetActive (true);
			Time.timeScale = 0;
			CSK.AllowTouch = false;
		}
		IsPaused = !IsPaused;
	}

	public void MainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (1);
	}
}
