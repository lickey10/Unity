using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class ST_PuzzleDisplay : MonoBehaviour 
{
	// this puzzle texture.
	public Texture[] PuzzleImages;

	// the width and height of the puzzle in tiles.
	public int Height = 3;
	public int Width  = 3;

	// additional scaling value.
	public Vector3 PuzzleScale = new Vector3(1.0f, 1.0f, 1.0f);

	// additional positioning offset.
	public Vector3 PuzzlePosition = new Vector3(0.0f, 0.0f, 0.0f);

	// seperation value between puzzle tiles.
	public float SeperationBetweenTiles = 0.5f;

	// the tile display object.
	public GameObject Tile;

	// the shader used to render the puzzle.
	public Shader PuzzleShader;

	// array of the spawned tiles.
	private GameObject[,] TileDisplayArray;
	private List<Vector3>  DisplayPositions = new List<Vector3>();

	// position and scale values.
	private Vector3 Scale;
	private Vector3 Position;

	// has the puzzle been completed?
	public bool Complete = false;

    public GameObject WidthDisplay;
    public GameObject HeightDisplay;
    public AudioClip[] CompletePuzzleSounds;
    public AudioClip[] BackgroundSounds;
    public UILabel CompletionsDisplay;
    public UILabel MovesCounterDisplay;
    public UILabel MovesRecordDisplay;
    public UILabel GameTimeDisplay;

    private int currentPuzzleImageIndex = 0;
    AudioSource audioSource;
    private bool muted = false;
    private bool countMoves = false;
    private int moveCounter = 0;
    private Time timeCounter;
    private int movesRecord = 0;
    private float gameTime;
    private float startTime;
    private bool gameStarted = false;

    //public GameData gameData;

    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    // Use this for initialization
    void Start () 
	{
        Width = SaveSystem.GetInt("Width", Width);
        Height = SaveSystem.GetInt("Height", Height);

        WidthDisplay.GetComponent<UILabel>().text = Width.ToString();
        HeightDisplay.GetComponent<UILabel>().text = Height.ToString();

        CompletePuzzleSounds = new AudioClip[1];
        audioSource = GetComponent<AudioSource>();

        movesRecord = SaveSystem.GetInt(Width.ToString() + "X" + Height.ToString() + "MovesRecord", moveCounter);

        if(MovesRecordDisplay != null)
            MovesRecordDisplay.text = movesRecord.ToString();

        CompletionsDisplay.text = SaveSystem.GetInt(Width.ToString() + "X" + Height.ToString() + "completions", 0).ToString();

        muted = SaveSystem.GetBool("muted", false);

        ResetGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// move the puzzle to the position set in the inspector.
		this.transform.localPosition = PuzzlePosition;

		// set the scale of the entire puzzle object as set in the inspector.
		this.transform.localScale = PuzzleScale;

        if (!audioSource.isPlaying && !muted)
            audioSource.PlayOneShot(BackgroundSounds[Random.Range(0, BackgroundSounds.Length - 1)]);

        //20 seconds has past
        if (gameStarted && Time.time - startTime > 20)
        {
            
        }

        if (!gameStarted)
        {
            gameTime = 0;

            GameTimeDisplay.text = "00:00";
        }
        else
        {
            gameTime += Time.deltaTime;

            var seconds = Mathf.RoundToInt(gameTime);
            var minutes = 0;

            if(seconds > 59)
            {
                minutes = Mathf.RoundToInt(seconds / 60);
                seconds = Mathf.RoundToInt(seconds - minutes * 60);
            }

            var timeToDisplay = "";

            if (minutes > 0)
                timeToDisplay = minutes + ":" + seconds;
            else
                timeToDisplay = seconds.ToString();

            GameTimeDisplay.text = timeToDisplay;
        }
    }

    public void ResetGame()
    {
        ResetGame(-1);
    }

    public void ResetGame(int newImageIndex)
    {
        gameStarted = false;
        StopCoroutine(CheckForComplete());

        audioSource.clip = BackgroundSounds[Random.Range(0, BackgroundSounds.Length - 1)];
        audioSource.Play();

        clearTiles();

        ST_PuzzleDisplay puzzle = FindObjectOfType<ST_PuzzleDisplay>();

        Width = int.Parse(WidthDisplay.GetComponent<UILabel>().text);

        Height = int.Parse(HeightDisplay.GetComponent<UILabel>().text);

        puzzle.Width = Width;
        puzzle.Height = Height;

        SaveSystem.SetInt("Width", Width);
        SaveSystem.SetInt("Height", Height);

        // create the games puzzle tiles from the provided image.
        if (newImageIndex > -1)
            CreatePuzzleTiles(newImageIndex);
        else
            CreatePuzzleTiles();

        //randomize the tiles
        StartCoroutine(randomize());

        // mix up the puzzle.
        //StartCoroutine(JugglePuzzle());
    }

    public void MuteToggle()
    {
        if (!muted)
        {
            audioSource.Stop();

            muted = true;
        }
        else
        {
            audioSource.PlayOneShot(BackgroundSounds[Random.Range(0, BackgroundSounds.Length - 1)]);

            muted = false;
        }

        SaveSystem.SetBool("muted", muted);
    }

    public void SetImage(Texture newImage)
    {
        PuzzleImages = new Texture[1];
        PuzzleImages[0] = newImage;
    }

    public int AddPuzzleImage(Texture newImage)
    {
        Texture[] newTextures = new Texture[PuzzleImages.Length];
        PuzzleImages.CopyTo(newTextures, 0);

        PuzzleImages = new Texture[PuzzleImages.Length + 1];
        newTextures.CopyTo(PuzzleImages, 0);

        currentPuzzleImageIndex = PuzzleImages.Length - 1;

        return currentPuzzleImageIndex;
    }
    
    public Vector3 GetTargetLocation(ST_PuzzleTile thisTile)
	{
		// check if we can move this tile and get the position we can move to.
		ST_PuzzleTile MoveTo = CheckIfWeCanMove((int)thisTile.GridLocation.x, (int)thisTile.GridLocation.y, thisTile);

		if(MoveTo != thisTile)
		{
			// get the target position for this new tile.
			Vector3 TargetPos = MoveTo.TargetPosition;
			Vector2 GridLocation = thisTile.GridLocation;
			thisTile.GridLocation = MoveTo.GridLocation;

			// move the empty tile into this tiles current position.
			MoveTo.LaunchPositionCoroutine(thisTile.TargetPosition);
			MoveTo.GridLocation = GridLocation;

            if (countMoves)
            {
                moveCounter++;

                if(MovesCounterDisplay != null)
                    MovesCounterDisplay.text = moveCounter.ToString();
            }

			// return the new target position.
			return TargetPos;
		}

		// else return the tiles actual position (no movement).
		return thisTile.TargetPosition;
	}

	private ST_PuzzleTile CheckMoveLeft(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move left 
		if((Xpos - 1)  >= 0)
		{
			// we can move left, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos - 1, Ypos, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveRight(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move right 
		if((Xpos + 1)  < Width)
		{
			// we can move right, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos + 1, Ypos , thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveDown(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move down 
		if((Ypos - 1)  >= 0)
		{
			// we can move down, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  - 1, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveUp(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move up 
		if((Ypos + 1)  < Height)
		{
			// we can move up, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  + 1, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckIfWeCanMove(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// check each movement direction
		if(CheckMoveLeft(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveLeft(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveRight(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveRight(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveDown(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveDown(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveUp(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveUp(Xpos, Ypos, thisTile);
		}

		return thisTile;
	}

	private ST_PuzzleTile GetTileAtThisGridLocation(int x, int y, ST_PuzzleTile thisTile)
	{
		for(int j = Height - 1; j >= 0; j--)
		{
			for(int i = 0; i < Width; i++)
			{
				// check if this tile has the correct grid display location.
				if((TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().GridLocation.x == x)&&
				   (TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().GridLocation.y == y))
				{
					if(TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().Active == false)
					{
						// return this tile active property. 
						return TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>();
					}
				}
			}
		}

		return thisTile;
	}

	private IEnumerator JugglePuzzle()
	{
		yield return new WaitForSeconds(1.0f);

		// hide a puzzle tile (one is always missing to allow the puzzle movement).
		TileDisplayArray[0,0].GetComponent<ST_PuzzleTile>().Active = false;

		yield return new WaitForSeconds(.5f);

        for (int k = 0; k < 20; k++)
        {
            // use random to position each puzzle section in the array delete the number once the space is filled.
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    // attempt to execute a move for this tile.
                    TileDisplayArray[i, j].GetComponent<ST_PuzzleTile>().ExecuteAdditionalMove();

                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        
        // continually check for the correct answer.
        StartCoroutine(CheckForComplete());

		yield return null;
	}

	public IEnumerator CheckForComplete()
	{
		while(Complete == false)
		{
			// iterate over all the tiles and check if they are in the correct position.
			Complete = true;
			for(int j = Height - 1; j >= 0; j--)
			{
				for(int i = 0; i < Width; i++)
				{
					// check if this tile has the correct grid display location.
					if(TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().CorrectLocation == false)  
					{
						Complete = false;
					}
				}
			}

			yield return null;
		}

        // if we are still complete then all the tiles are correct.
        if (Complete)
        {
            Debug.Log("Puzzle Complete!");

            gameStarted = false;
            audioSource.PlayOneShot(CompletePuzzleSounds[Random.Range(0, CompletePuzzleSounds.Length - 1)]);

            int tempCompletions = SaveSystem.GetInt(Width.ToString() + "X" + Height.ToString() + "completions", 0);
            tempCompletions++;

            SaveSystem.SetInt(Width.ToString() + "X" + Height.ToString() + "completions", tempCompletions);

            CompletionsDisplay.text = tempCompletions.ToString();

            movesRecord = SaveSystem.GetInt(Width.ToString() + "X" + Height.ToString() + "MovesRecord", 0);

            if (moveCounter < movesRecord)//only save if we have beaten our record
            {
                SaveSystem.SetInt(Width.ToString() + "X" + Height.ToString() + "MovesRecord", moveCounter);

                if (MovesRecordDisplay != null)
                    MovesRecordDisplay.text = movesRecord.ToString();
            }
        }

		yield return null;
	}

	private Vector2 ConvertIndexToGrid(int index)
	{
		int WidthIndex = index;
		int HeightIndex = 0;

		// take the index value and return the grid array location X,Y.
		for(int i = 0; i < Height; i++)
		{
			if(WidthIndex < Width)
			{
				return new Vector2(WidthIndex, HeightIndex);
			}
			else
			{
				WidthIndex -= Width;
				HeightIndex++;
			}
		}

		return new Vector2(WidthIndex, HeightIndex);
	}

    private void CreatePuzzleTiles()
    {
        currentPuzzleImageIndex = Random.Range(0, PuzzleImages.Length - 1);

        CreatePuzzleTiles(currentPuzzleImageIndex);
    }

    private void CreatePuzzleTiles(int newPuzzleIndex)
	{
		// using the width and height variables create an array.
		TileDisplayArray = new GameObject[Width,Height];

		// set the scale and position values for this puzzle.
		Scale = new Vector3(1.0f/Width, 1.0f, 1.0f/Height);
		Tile.transform.localScale = Scale;

		// used to count the number of tiles and assign each tile a correct value.
		int TileValue = 0;

        //currentPuzzleImageIndex = Random.Range(0, PuzzleImages.Length - 1);
        currentPuzzleImageIndex = newPuzzleIndex;
        Texture puzzleImage = PuzzleImages[newPuzzleIndex];

        // spawn the tiles into an array.
        for (int j = Height - 1; j >= 0; j--)
		{
			for(int i = 0; i < Width; i++)
			{
				// calculate the position of this tile all centred around Vector3(0.0f, 0.0f, 0.0f).
				Position = new Vector3(((Scale.x * (i + 0.5f))-(Scale.x * (Width/2.0f))) * (10.0f + SeperationBetweenTiles), 
				                       0.0f, 
				                      ((Scale.z * (j + 0.5f))-(Scale.z * (Height/2.0f))) * (10.0f + SeperationBetweenTiles));

				// set this location on the display grid.
				DisplayPositions.Add(Position);

				// spawn the object into play.
				TileDisplayArray[i,j] = Instantiate(Tile, new Vector3(0.0f, 0.0f, 0.0f) , Quaternion.Euler(90.0f, -180.0f, 0.0f)) as GameObject;
				TileDisplayArray[i,j].gameObject.transform.parent = this.transform;

				// set and increment the display number counter.
				ST_PuzzleTile thisTile = TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>();
				thisTile.ArrayLocation = new Vector2(i,j);
				thisTile.GridLocation = new Vector2(i,j);
				thisTile.LaunchPositionCoroutine(Position);
				TileValue++;

				// create a new material using the defined shader.
				Material thisTileMaterial = new Material(PuzzleShader);

                // apply the puzzle image to it.
                thisTileMaterial.mainTexture = puzzleImage;
					
				// set the offset and tile values for this material.
				thisTileMaterial.mainTextureOffset = new Vector2(1.0f/Width * i, 1.0f/Height * j);
				thisTileMaterial.mainTextureScale  = new Vector2(1.0f/Width, 1.0f/Height);
					
				// assign the new material to this tile for display.
				TileDisplayArray[i,j].GetComponent<Renderer>().material = thisTileMaterial;
			}
		}

		/*
		// Enable an impossible puzzle for fun!
		// switch the second and third grid location textures.
		Material thisTileMaterial2 = TileDisplayArray[1,3].GetComponent<Renderer>().material;
		Material thisTileMaterial3 = TileDisplayArray[2,3].GetComponent<Renderer>().material;
		TileDisplayArray[1,3].GetComponent<Renderer>().material = thisTileMaterial3;
		TileDisplayArray[2,3].GetComponent<Renderer>().material = thisTileMaterial2;
		*/
	}

    // Randomize a 2D array.
    private IEnumerator randomize()
    {
        //don't count these moves for the user
        countMoves = false;

        //yield return new WaitForSeconds(2.0f);

        // hide a puzzle tile (one is always missing to allow the puzzle movement).
        TileDisplayArray[Random.Range(0,Width), Random.Range(0,Height)].GetComponent<ST_PuzzleTile>().Active = false;

        // Get the dimensions.
        int num_rows = TileDisplayArray.GetUpperBound(0) + 1;
        int num_cols = TileDisplayArray.GetUpperBound(1) + 1;
        int num_cells = num_rows * num_cols;

        for (int x = 0; x < num_cells * 2; x++)
        {
            // Randomize the array.
            System.Random rand = new System.Random();
            for (int i = 0; i < num_cells - 1; i++)
            {
                // Pick a random cell between i and the end of the array.
                int j = rand.Next(i, num_cells);

                // Convert to row/column indexes.
                int row_i = i / num_cols;
                int col_i = i % num_cols;
                int row_j = j / num_cols;
                int col_j = j % num_cols;

                // Swap cells i and j.


                ST_PuzzleTile thisTile1 = TileDisplayArray[row_i, col_i].GetComponent<ST_PuzzleTile>();
                ST_PuzzleTile thisTile2 = TileDisplayArray[row_j, col_j].GetComponent<ST_PuzzleTile>();
                //Vector2 tempArrayLocation1 = thisTile1.ArrayLocation;
                //Vector2 tempGridLocation1 = thisTile1.GridLocation;
                //Vector3 localPosition = thisTile1.transform.localPosition;

                thisTile1.ExecuteAdditionalMove(40f);
                thisTile2.ExecuteAdditionalMove(40f);

                yield return new WaitForSeconds(0.0001f);

                //thisTile1.transform.localPosition = thisTile2.transform.localPosition;
                //thisTile2.transform.localPosition = localPosition;

                //thisTile1.ArrayLocation = thisTile2.ArrayLocation;
                //thisTile1.GridLocation = thisTile2.GridLocation;

                //thisTile2.ArrayLocation = tempArrayLocation1;
                //thisTile2.GridLocation = tempGridLocation1;

                //GameObject tempTile1 = TileDisplayArray[row_i, col_i];
                //TileDisplayArray[row_i, col_i] = TileDisplayArray[row_j, col_j];
                //TileDisplayArray[row_j, col_j] = tempTile1;

                //thisTile1.LaunchPositionCoroutine(thisTile2.transform.localPosition);
                //thisTile2.LaunchPositionCoroutine(localPosition);
            }
        }

        Complete = false;

        moveCounter = 0;
        countMoves = true;

        MovesCounterDisplay.text = "0";

        // continually check for the correct answer.
        StartCoroutine(CheckForComplete());
        gameStarted = true;
        startTime = Time.time;

        yield return null;
    }


    private void clearTiles()
    {
        for (int j = Height - 1; j >= 0; j--)
        {
            for (int i = 0; i < Width; i++)
            {
                if(TileDisplayArray != null && TileDisplayArray[i, j] != null)
                    Destroy(TileDisplayArray[i, j].gameObject);
            }
        }
    }
}
