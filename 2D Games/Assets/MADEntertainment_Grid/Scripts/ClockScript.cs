
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClockScript : MonoBehaviour 
{

	[SerializeField]
	Sprite WhiteSprite;

	[SerializeField]
	GameObject ClockText;
	[SerializeField]
	GameObject WaitText;

	public int TimeLeft = 20;
	int Seconds = 3;

	public bool AllowTouch;
	bool TickTock;

	GameObject DataManager;
	SaveDataScript SDS;

	[SerializeField]
	GameObject GameManager;
	ScoreMgmt SMG;
	RandomiserScript RMS;
	ThemeChangeScript CGS;

	void Start () 
	{
		DataManager = GameObject.FindGameObjectWithTag ("DM");

		SDS = DataManager.GetComponent<SaveDataScript> ();
		SMG = GameManager.GetComponent<ScoreMgmt> ();
		RMS = GameManager.GetComponent<RandomiserScript> ();
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		ClockText.GetComponent<TextMesh> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		Camera.main.backgroundColor = new Color32 (CGS.R3, CGS.G3, CGS.B3,255);
		WaitText.gameObject.SetActive (true);
		WaitText.GetComponent<TextMesh>().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		WaitOnStart ();
	}

	void WaitOnStart()
	{
		if (Seconds > 0) 
		{
			WaitText.GetComponent<MeshRenderer> ().sortingOrder = 10;
			WaitText.GetComponent<TextMesh> ().text = Seconds.ToString ();
			Seconds--;
			Invoke ("WaitOnStart", 0.75f);
		}
		else 
		{
			WaitText.SetActive (false);
			RMS.ManageDifficulty ();
			RMS.GenerateRandomGrid ();
			AllowTouch = true;
			Invoke("CountDown",0.01f);
		}
	}

	void PlayTickTock()
	{
		if (!TickTock) 
		{
			gameObject.GetComponent<AudioSource> ().Play ();
			TickTock = true;
		}
	}

	public void CountDown()
	{
		if (TimeLeft <= 3) 
		{
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0,255);
			ClockText.GetComponent<TextMesh> ().color = Color.red;
			transform.localScale = new Vector2 (1.2f,1.2f);
			PlayTickTock ();
			Invoke ("ResizeCircle",0.3f);
		} 
		else if (TimeLeft % 2 == 0) 
		{
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
			ClockText.GetComponent<TextMesh> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

			if(TickTock)
			{
				gameObject.GetComponent<AudioSource> ().Stop ();
				TickTock = false;
			}
		} 
		else 
		{
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
			ClockText.GetComponent<TextMesh> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);

			if(TickTock)
			{
				gameObject.GetComponent<AudioSource> ().Stop ();
				TickTock = false;
			}
		}

		ClockText.GetComponent<TextMesh> ().text = TimeLeft.ToString();
		if (TimeLeft > 0) 
		{
			TimeLeft--;
			Invoke ("CountDown",1);
		}
		else
		{
			AllowTouch = false;
			Invoke("GameOver",1.0f);
		}
	}

	public void AddSeconds ()
	{
		TimeLeft += 2;
		ClockText.GetComponent<TextMesh> ().text = TimeLeft.ToString();
	}

	void ResizeCircle()
	{
		transform.localScale = new Vector2 (1f,1f);
	}

	public void GameOver()
	{		
		SDS.CurrentScore = SMG.TotalScore;
		SceneManager.LoadScene (3);
	}
}
