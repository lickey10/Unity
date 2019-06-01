using UnityEngine;
using System.Collections;

public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;

	public GameObject Paddle;
    public Transform ballPrefab;
	public Transform[] Powerups;
	 
	private int NumberOfBalls = 0;
    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;
	private bool isLostBallRepeating = false;

    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.playing;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;
        Time.timeScale = 1.0f;
		Invoke("SpawnBall",1);
	}
	
	void Start()
	{
		NumberOfBalls = gamestate.Instance.getLives();
	}

	// Instantiates a prefab in a grid
	
//	public GameObject prefab;
//	public float gridX = 5f;
//	public float gridY = 5f;
//	public float spacing = 2f;
//	
//	void Start() {
//		for (int y = 0; y < gridY; y++) {
//			for (int x = 0; x < gridX; x++) {
//				Vector3 pos = new Vector3(x, 0, y) * spacing;
//				Instantiate(prefab, pos, Quaternion.identity);
//			}
//		}
//	} 


    public void SpawnBall()
    {
        //Instantiate(ballPrefab, new Vector3(1.81f, 1.0f , 9.75f), Quaternion.identity);

		//shoot up from the middle of the paddle
		Instantiate(ballPrefab, new Vector3(Paddle.transform.localPosition.x+2, Paddle.transform.localPosition.y , Paddle.transform.position.z + 1), Quaternion.identity);
    }

	void spawnPowerup(Vector3 blockPosition)
	{
		//Instantiate(ballPrefab, new Vector3(1.81f, 1.0f , 9.75f), Quaternion.identity);
		
		//drop down from the block
		Transform thePowerUp = (Transform)Instantiate(Powerups[Random.Range(0,Powerups.Length)], new Vector3(blockPosition.x, blockPosition.y , blockPosition.z), Powerups[Random.Range(0,Powerups.Length)].transform.rotation);
		//thePowerUp.gameObject.AddComponent<Powerup>();
	}

    void OnGUI(){
    
        //GUILayout.Space(10);
        //GUILayout.Label("  Hit: " + blocksHit + "/" + totalBlocks);

//        if (gameState == BreakoutGameState.lost)
//        {
//            GUILayout.Label("You Lost!");
//            if (GUILayout.Button("Try again"))
//            {
//                Application.LoadLevel(Application.loadedLevel);
//            }
//        }
//        else if (gameState == BreakoutGameState.won)
//        {
//            GUILayout.Label("You won!");
//            if (GUILayout.Button("Play again"))
//            {
//                Application.LoadLevel(Application.loadedLevel);
//            }
//        }
    }

    public void HitBlock(Vector3 blockPosition)
    {
        blocksHit++;
        
        if (blocksHit%10 == 0) //Every 10th block will spawn a new ball
        {
            //SpawnBall();

			//spawn power up/down
			spawnPowerup(blockPosition);
        }

		//totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length; //some blocks take multiple hits so this needs to update to see if they are still there

        
        if (blocksHit >= totalBlocks)
        {

            WonGame();
        }
    }

	//won the level
    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.won;
		gamestate.Instance.currentLevelComplete = true;
		gamestate.Instance.setActiveLevel(gamestate.Instance.getActiveLevel() + 1);
		Application.LoadLevel("Level"+ gamestate.Instance.getActiveLevel() +"CompleteMenu");
    }

    public void LostBall()
    {
		int currentBallsInPlay = GameObject.FindGameObjectsWithTag("Player").Length;

		if(currentBallsInPlay <= 0)
		{
			CancelInvoke("LostBall");
			isLostBallRepeating = false;

			NumberOfBalls--;
			gamestate.Instance.SetLives(NumberOfBalls);

	        if(NumberOfBalls<=0){
					SetGameOver();
	        }
			else 
				Invoke("SpawnBall",2);
		}
		else if(!isLostBallRepeating)
		{
			isLostBallRepeating = true;
			InvokeRepeating("LostBall",1,1);
		}
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.lost;

		gamestate.Instance.currentLevelComplete = false;
		gamestate.Instance.IsGameWon = false;
		Application.LoadLevel("Level"+ gamestate.Instance.getActiveLevel() +"CompleteMenu");
    }
}
