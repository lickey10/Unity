using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	GameObject[] coverPieces;
	bool displayNextLevelScreen = false;
	Vector3 centerPoint;
	int levelCounter = 0;
	int pieceToDisplayTracker = 0;
	GameObject currentPieceToCover;
	public GameObject[] CoverPieceArray;
	public GameObject[] PieceToCoverArray;
	public GameObject PieceToCoverPlaceholder;
	public bool UseRandomeCoverPieces = false;
	public bool UseRandomeRotation = false;
	
	// Use this for initialization
	void Start () {
		centerPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
		centerPoint = new Vector3 (centerPoint.x, centerPoint.y, PieceToCoverPlaceholder.transform.position.z - 1.5f);

		startLevel();
	}
	
	// Update is called once per frame
	void Update () {
		//check the number of existing cover pieces and notify when done.
		coverPieces = GameObject.FindGameObjectsWithTag ("coverPiece");

		//iterate array of cover pieces and check that they are all visible
//		foreach (GameObject coverPiece in coverPieces) {
//			if (!canISeeObject (coverPiece))//remove 
//			{
//				//coverPiece.GetComponent<GUIEffects>().DestroyMe();
//				DestroyObject (coverPiece);
//			}
//		}

		if (coverPieces.Length == 0 && !displayNextLevelScreen) {
			SoundEffectsHelper.Instance.MakeLevelCompleteSound ();
			displayNextLevelScreen = true;
		}

		if (Input.GetMouseButtonDown(0) && displayNextLevelScreen) {//start next level
			startLevel ();
		}
	}

	private bool canISeeObject(GameObject Object) {
		try {
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

			Collider tempCollider = Object.GetComponentInChildren<Collider> ();

			Bounds tempBounds = Object.GetComponentInChildren<Collider> ().bounds;
			tempBounds.Expand (-1);
			
			if (GeometryUtility.TestPlanesAABB(planes , tempBounds))
				return true;
			else
				return false;
		} catch (System.Exception ex) {
			return false;
		}

	}

	void OnGUI()
	{
		if(displayNextLevelScreen)
			GUI.Box(new Rect((Screen.width - 100)/2,Screen.height-50,100,50),"Next Level");
	}

	void OnMouseDown()
	{
//		if (displayNextLevelScreen) {//start next level
//			startLevel();
//		}
	}

	private void startLevel()
	{
		displayNextLevelScreen = false;

		if (currentPieceToCover != null)
			DestroyObject (currentPieceToCover.gameObject);

		currentPieceToCover = Instantiate (PieceToCoverArray[pieceToDisplayTracker] as GameObject, PieceToCoverPlaceholder.transform.position, PieceToCoverArray[pieceToDisplayTracker].transform.rotation) as GameObject;

		placeCoverPieces ();
		
		//get an array of cover pieces
		coverPieces = GameObject.FindGameObjectsWithTag ("coverPiece");

		Invoke ("checkIfVisible", .01f);

		levelCounter++;
		pieceToDisplayTracker++;

		if (pieceToDisplayTracker >= PieceToCoverArray.Length)
			pieceToDisplayTracker = 0;
	}

	private void checkIfVisible()
	{
		//iterate array of cover pieces and check that they are all visible
		foreach (GameObject coverPiece in coverPieces) {
			if (!canISeeObject (coverPiece))//remove 
			{
				//coverPiece.GetComponent<GUIEffects>().DismissObjects();//.DismissNow();//.DestroyMe();
				DestroyObject (coverPiece);
			}
		}
	}

	private void placeCoverPieces()
	{
		//start in center and work outward till we match screen height and width
		//Quaternion randomRotation;// = Quaternion.Euler( Random.Range(0, 360) , Random.Range(0, 360) , Random.Range(0, 360));

		//GameObject newCoverPiece = (GameObject)Object.Instantiate (CoverPiece,centerPoint , randomRotation);

		int numPoints = 10;

		float radius = .28f;
		int numberOfCircles = 7;
		int currentCoverPieceIndex = Random.Range (0, CoverPieceArray.Length - 1);
		Quaternion rotation = Quaternion.identity;

		for(int x = 0; x < numberOfCircles; x++)
		{
			float tempRadius = (radius * (x+1));
			numPoints = numPoints + x+5;

			for (int pointNum = 0; pointNum < numPoints; pointNum++)
			{
				if(UseRandomeCoverPieces)
					currentCoverPieceIndex = Random.Range (0, CoverPieceArray.Length);

				//randomRotation = Quaternion.Euler( Random.Range(0, 360) , Random.Range(0, 360) , Random.Range(0, 360));

				if(UseRandomeRotation)
					rotation = Quaternion.Euler( Random.Range(0, 35) , Random.Range(0, 35) , Random.Range(0, 35));
				
				// "i" now represents the progress around the circle from 0-1
				// we multiply by 1.0 to ensure we get a fraction as a result.
				float i = (float)(pointNum * 1.0) / numPoints;
				// get the angle for this step (in radians, not degrees)
				float angle = i * Mathf.PI * 2;
				// the X &amp; Y position for this angle are calculated using Sin &amp; Cos
				var newX = Mathf.Sin(angle) * tempRadius;
				var newY = Mathf.Cos(angle) * tempRadius;
				Vector3 pos = new Vector3(newX, newY, 0) + centerPoint;
				GameObject tempCoverPiece = CoverPieceArray[currentCoverPieceIndex] as GameObject;
				// no need to assign the instance to a variable unless you're using it afterwards:
				GameObject newCoverPiece = (GameObject)Instantiate (tempCoverPiece, pos, rotation);
				newCoverPiece.tag = "coverPiece";
			}   
		}
	}
}
