using UnityEngine;
using System.Collections;

public class RandomiserScript : MonoBehaviour 
{
	CheckForMatchScript CMS;
	GridRotationScript GRS;
	ThemeChangeScript CGS;

	GameObject[,] Grid = new GameObject[3,3];

	[SerializeField]
	GameObject GridObject;

	[SerializeField]
	Sprite PurpleSprite; 
	[SerializeField]
	Sprite GreySprite;

	[SerializeField]
	GameObject GridToFollow;

	GameObject DataManager;

	int TilesLeft;
	public int TotalTiles;
	int RandomNum;
	int Min,Max;
	public int Turns;

	void Start () 
	{
		CMS = gameObject.GetComponent<CheckForMatchScript> ();
		GRS = GridToFollow.GetComponent<GridRotationScript> ();
		DataManager = GameObject.FindGameObjectWithTag ("DM");
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		Grid [0, 0] = GameObject.Find ("GT00");
		Grid [0, 1] = GameObject.Find ("GT01");
		Grid [0, 2] = GameObject.Find ("GT02");

		Grid [1, 0] = GameObject.Find ("GT10");
		Grid [1, 1] = GameObject.Find ("GT11");
		Grid [1, 2] = GameObject.Find ("GT12");

		Grid [2, 0] = GameObject.Find ("GT20");
		Grid [2, 1] = GameObject.Find ("GT21");
		Grid [2, 2] = GameObject.Find ("GT22");

		for (int Counter1 = 0; Counter1 < 3; Counter1++) 
		{
			for (int Counter2 = 0; Counter2 < 3; Counter2++) 
			{
				Grid [Counter1, Counter2].GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
			}
		}
	}

	public void ManageDifficulty () 
	{
		if (Turns < 5) 
		{
			Min = Random.Range (1, 3);
			Max = Random.Range (3, 5);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;
		} 
		else if (Turns < 10) 
		{
			Min = Random.Range (1, 4);
			Max = Random.Range (4, 7);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;
		}
		else if (Turns < 15) 
		{
			Min = Random.Range (2, 4);
			Max = Random.Range (4, 8);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;

			if (Random.Range (0, 2) == 1)
			{
				GRS.DegreesToRotate = 90;
				GRS.RotateGrid ();
			}
		}
		else if (Turns < 20) 
		{
			Min = Random.Range (3, 6);
			Max = Random.Range (6, 8);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;

			if (Random.Range (0, 2) == 1)
			{
				GRS.DegreesToRotate = 270;
				GRS.RotateGrid ();
			}
		}
		else if (Turns < 25) 
		{
			Min = Random.Range (3, 6);
			Max = Random.Range (6, 9);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;

			int Temp = Random.Range (0, 3);

			if (Temp == 1) 
			{
				GRS.DegreesToRotate = 90;
				GRS.RotateGrid ();
			}
			else if (Temp == 2) 
			{
				GRS.DegreesToRotate = 270;
				GRS.RotateGrid ();
			}
		}
		else 
		{
			Min = Random.Range (5, 7);
			Max = Random.Range (7, 9);
			TilesLeft = Random.Range (Min, Max);
			TotalTiles = TilesLeft;

			int Temp = Random.Range (0, 4);

			if (Temp == 1) 
			{
				GRS.DegreesToRotate = 90;
				GRS.RotateGrid ();
			}
			else if (Temp == 2) 
			{
				GRS.DegreesToRotate = 180;
				GRS.RotateGrid ();
			}
			else if (Temp == 3) 
			{
				GRS.DegreesToRotate = 270;
				GRS.RotateGrid ();
			}
		}
	}

	public void GenerateRandomGrid()
	{
		for (int Counter1 = 0; Counter1 < 3 && TilesLeft > 0; Counter1++) 
		{
			for (int Counter2 = 0; Counter2 < 3 && TilesLeft > 0; Counter2++) 
			{
				RandomNum = Random.Range (0,2);

				if(RandomNum == 1 && !CMS.GetSmallGridValue(Counter1, Counter2))
				{
					CMS.SetSmallGridValue (Counter1, Counter2, true);
					Grid [Counter1, Counter2].GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
					TilesLeft--;
				}
			}
		}

		if (TilesLeft > 0) 
		{
			GenerateRandomGrid ();
		} 
		else 
		{
			TilesLeft = 4;
		}
	}

	public void ResetGrid()
	{
		GridObject.transform.rotation = Quaternion.identity;
		for (int Counter1 = 0; Counter1 < 3; Counter1++) 
		{
			for (int Counter2 = 0; Counter2 < 3; Counter2++) 
			{
				CMS.SetSmallGridValue (Counter1, Counter2, false);
				Grid [Counter1, Counter2].GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
			}
		}
	}
}
