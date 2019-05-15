using UnityEngine;
using System.Collections;

public class CheckForMatchScript : MonoBehaviour 
{
	[SerializeField]
	Sprite GreySprite;

	[SerializeField]
	GameObject Clock;

	GameObject DataManager;
	GameObject[,] Grid = new GameObject[3,3];
	bool[,] MainGrid = new bool[3,3];
	public bool[,] GridToFollow = new bool[3,3];
	int TotalMatch = 0;

	ThemeChangeScript CGS;
	RandomiserScript RMS;
	ScoreMgmt SMG;
	ClockScript CSK;
	SaveDataScript SDS;

	AudioSource AS;
	float Pitch;

	void Start () 
	{
		DataManager = GameObject.FindGameObjectWithTag ("DM");

		RMS = gameObject.GetComponent<RandomiserScript> ();	
		SMG = gameObject.GetComponent<ScoreMgmt> ();
		CSK = Clock.GetComponent<ClockScript> ();
		SDS = DataManager.GetComponent<SaveDataScript> ();
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		AS = gameObject.GetComponent<AudioSource> ();

		Grid [0, 0] = GameObject.Find ("MG00");
		Grid [0, 1] = GameObject.Find ("MG01");
		Grid [0, 2] = GameObject.Find ("MG02");

		Grid [1, 0] = GameObject.Find ("MG10");
		Grid [1, 1] = GameObject.Find ("MG11");
		Grid [1, 2] = GameObject.Find ("MG12");

		Grid [2, 0] = GameObject.Find ("MG20");
		Grid [2, 1] = GameObject.Find ("MG21");
		Grid [2, 2] = GameObject.Find ("MG22");

		for (int Counter1 = 0; Counter1 < 3; Counter1++) 
		{
			for (int Counter2 = 0; Counter2 < 3; Counter2++) 
			{
				Grid [Counter1, Counter2].GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
			}
		}
	}

	public void PlayAudio () 
	{
		Pitch += 1.0f;
		AS.pitch = Pitch;
		AS.Play ();
	}

	public void SetSmallGridValue(int Row,int Column, bool State)
	{
		GridToFollow [Row, Column] = State;
	}

	public bool GetSmallGridValue(int Row,int Column)
	{
		return GridToFollow [Row, Column];
	}

	public void SetGridValue(int Row,int Column, bool State)
	{
		MainGrid [Row, Column] = State;

		CheckForMatch (Row,Column);
	}

	void CheckForMatch(int Row, int Column)
	{
		if (SDS.GameMode == 2) 
		{
			if (MainGrid [Row, Column] != GridToFollow [Row, Column]) 
			{
				CSK.AllowTouch = false;
				CSK.GameOver ();
			}
		}

		if (SDS.GameMode == 1 || SDS.GameMode == 2) 
		{
			TotalMatch = 0;
			for (int Counter1 = 0; Counter1 < 3; Counter1++) 
			{
				for (int Counter2 = 0; Counter2 < 3; Counter2++) 
				{
					if (MainGrid [Counter1, Counter2] == GridToFollow [Counter1, Counter2]) 
					{
						TotalMatch++;
					}
				}
			}
		} 
		else if (SDS.GameMode == 3) 
		{
			TotalMatch = 0;
			for (int Counter1 = 0; Counter1 < 3; Counter1++) 
			{
				if (MainGrid [Counter1, 0] == GridToFollow [Counter1, 2])
					TotalMatch++;
				if (MainGrid [Counter1, 1] == GridToFollow [Counter1, 1])
					TotalMatch++;
				if (MainGrid [Counter1, 2] == GridToFollow [Counter1, 0])
					TotalMatch++;
			}
		} 

		if(TotalMatch == 9)
		{
			SMG.AddScore (RMS.TotalTiles);
			CSK.AddSeconds ();
			Invoke("ResetMainGrid",0.1f);
			RMS.ResetGrid();
			RMS.Turns++;
			RMS.ManageDifficulty ();
			RMS.GenerateRandomGrid();
		}
	}

	void ResetMainGrid()
	{
		Pitch = 0.0f;
		for (int Counter1 = 0; Counter1 < 3; Counter1++) 
		{
			for (int Counter2 = 0; Counter2 < 3; Counter2++) 
			{
				MainGrid [Counter1, Counter2] = false;
				Grid [Counter1, Counter2].GetComponent<TouchHandlerScript> ().IsClicked = false;
				Grid [Counter1, Counter2].GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
			}
		}
	}
}
