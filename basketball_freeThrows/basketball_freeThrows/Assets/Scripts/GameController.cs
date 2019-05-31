using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	#region singleton methods 
	
	/// <summary>
	/// A class variable and property are created to allow other objects to have easy access to the game controller 
	/// </summary>
	
	private static GameController _instance = null; 
	
	public static GameController SharedInstance{
		get{
			// if the instance hasn't been assigned then search for it
			if( _instance == null ){
				_instance = GameObject.FindObjectOfType(typeof( GameController ) ) as GameController; 	
			}
			
			return _instance; 
		}
	}
	
	#endregion
	
	public enum GameStateEnum{
		Undefined, 
		Menu, 
		Paused, 
		Play, 
		GameOver
	}
	
	/// <summary>
	/// Reference to our player on the scene 
	/// </summary>
	public Player player; 
	
	/// <summary>
	/// Reference to our games scoreboard 
	/// </summary>
	public ScoreBoard scoreBoard; 
	
	/// <summary>
	/// reference to the courts one and only basketball 
	/// </summary>
	public Ball basketBall; 			
	
	/// <summary>
	/// time for a single game session (in seconds) 
	/// </summary>
	public float gameSessionTime = 180.0f;  
	
	/// <summary>
	/// radius the player will be positioned for each throw 
	/// </summary>
	public float throwRadius = 5.0f;

	public GameObject NetObject;
	
	/// <summary>
	/// state of the current game - controls how user interactions are interrupted and what is activivated and disabled 
	/// </summary>
	private GameStateEnum _state = GameStateEnum.Undefined;	
	
	/// <summary>
	/// Points accumulated by the user for this game session 
	/// </summary>
	private int _gamePoints = 0; 
	
	/// <summary>
	/// The time remaining for current game session 
	/// </summary>
	private float _timeRemaining = 0.0f;
	
	/// <summary>
	/// we only want to update the count down every second; so we'll accumulate the time in this variable 
	/// and update the remaining time after each second 
	/// </summary>
	private float _timeUpdateElapsedTime = 0.0f; 
	
	/// <summary>
	/// The original player position - each throw position will be offset based on this and a random value between 
	/// -throwRadius and throwRadius 
	/// </summary>
	private Vector3 _orgPlayerPosition; 

	private BoxCollider netCollider;
	
	void Awake(){		
		_instance = this; 
	}
	
	// Use this for initialization
	void Start () {
		// register the event delegates 
		basketBall.OnNet += HandleBasketBallOnNet; 
		
		// listen out for player animation change events 
		//player.OnPlayerAnimationFinished += HandlePlayerOnPlayerAnimationFinished;
		
		// set the players original position 
		_orgPlayerPosition = player.transform.position; 

		netCollider = NetObject.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		//check if the game is over
		if( player.State != Player.PlayerStateEnum.Idle && _timeRemaining <= 0.0f )
			State = GameStateEnum.GameOver;

		if( _state == GameStateEnum.Undefined ){
			// if no state is set then we will switch to the menu state 
			State = GameStateEnum.Menu;	
		}
		else if( _state == GameStateEnum.Play ){			
			UpdateStatePlay(); 
		}
		else if(_state == GameStateEnum.GameOver ){
			UpdateStateGameOver();	
		}
		
	}
	
	private void UpdateStatePlay(){
		_timeRemaining -= Time.deltaTime; 
			
		// accumulate elapsed time 
		_timeUpdateElapsedTime += Time.deltaTime; 
			
		// has a second past? 
		if( _timeUpdateElapsedTime >= 1.0f ){
			TimeRemaining = _timeRemaining; 
		}
			
		// after n seconds of the player being in the miss or score state reset the position and session 
		if( (player.State == Player.PlayerStateEnum.Miss || player.State == Player.PlayerStateEnum.Score)
			&& player.ElapsedStateTime >= 3.0f ){
				
			// set a new throw position 
			Vector3 playersNextThrowPosition = _orgPlayerPosition;
			// offset x 
			playersNextThrowPosition.x +=  Random.Range(-throwRadius, throwRadius); 
			player.ShotPosition = playersNextThrowPosition; 			

		}
	}
	
	private void UpdateStateGameOver(){		
		// TODO; to implement (next tutorial) 
		
	}
	
	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	/// <value>
	/// The state.
	/// </value>
	public GameStateEnum State{
		get{
			return _state; 	
		}
		set{
			_state = value; 
			
			// MENU 
			if( _state == GameStateEnum.Menu ){
				Debug.Log( "State change - Menu" ); 				
				
				player.State = Player.PlayerStateEnum.BouncingBall;	
				
				// TODO: replace play state with menu (next tutorial)
				StartNewGame();
			}
			
			// PAUSED 
			else if( _state == GameStateEnum.Paused ){
				Debug.Log( "State change - Paused" );
				 				
				// TODO; add pause state (next tutorial)				
			}
			
			// PLAY 
			else if( _state == GameStateEnum.Play ){
				Debug.Log( "State change - Play" ); 				
				 
			}
			
			// GAME OVER 
			else if( _state == GameStateEnum.GameOver ){
				Debug.Log( "State change - GameOver" ); 				

				Application.LoadLevel("gameover");

				//StartNewGame();
			}						
			
		}
	}	
	
	public void StartNewGame(){		
		GamePoints = 0; 
		TimeRemaining = gameSessionTime; 
		player.State = Player.PlayerStateEnum.BouncingBall; 
		State = GameStateEnum.Play;
	}
	
	public void ResumeGame(){
		if( _timeRemaining < 0 ){
			StartNewGame(); 	
		} else{
			State = GameStateEnum.Play;
		}
	}
	
	/// <summary>
	/// Gets or sets the game points and updates the score baord.
	/// </summary>
	/// <value>
	/// The game points.
	/// </value>
	public int GamePoints{
		get{
			return _gamePoints; 	
		}
		set{
			_gamePoints = value; 
			scoreBoard.SetPoints( _gamePoints.ToString() ); 
		}
	}
	
	/// <summary>
	/// Gets or sets the time remaining and updates the score board 
	/// </summary>
	/// <value>
	/// The time remaining.
	/// </value>
	public float TimeRemaining{
		get{
			return _timeRemaining; 
		}
		set{			
			_timeRemaining = value; 
			scoreBoard.SetTime( _timeRemaining.ToString("00:00") ); 			
			
			// reset the elapsed time 
			_timeUpdateElapsedTime = 0.0f; 
		}
	}
	
	/// <summary>
	/// Callback method called by the Ball component to notify the controller of any collisions 
	/// </summary>
	/// <param name='collision'>
	/// Collision.
	/// </param>
	public void OnBallCollisionEnter( Collision collision ){
		// ignore the callback if the player is holding the ball 
		if( !player.IsHoldingBall ){
			// have we hit the ground and no points have been awarded? 
			if( (collision.transform.name == "Ground" || collision.transform.name == "Court")
				&& player.State == Player.PlayerStateEnum.Throwing ){
				player.State = Player.PlayerStateEnum.Miss; 
			}
			
		}
	}
	
	/// <summary>
	/// Callback method registered to the OnNet event of the attached Ball, 
	/// add points and set the player animatino to winning 
	/// </summary>
	public void HandleBasketBallOnNet(){
		netCollider.enabled = false;
		Invoke("EnableNetCollider",2);

		GamePoints += 2; 
		player.State = Player.PlayerStateEnum.Score;

		if(GamePoints > gamestate.Instance.getHighScore())
			gamestate.Instance.SetHighScore(GamePoints);
	}

	public void EnableNetCollider()
	{
		netCollider.enabled = true;
	}
	
	/// <summary>
	/// Callback method from the OnPlayerAnimationFinished event from the player 
	/// </summary>
	/// <param name='animationName'>
	/// Animation name.
	/// </param>
	public void HandlePlayerOnPlayerAnimationFinished( string animationName ){
		if( player.State == Player.PlayerStateEnum.Walking ){
			player.State = Player.PlayerStateEnum.BouncingBall;	
		}
	}
	
	#region input handlers 
	
	/// <summary>
	/// I normally create a InputController which is responsible for interrupted the user input; having it decoupled means
    /// you have the abstraction of being able to extend or change platforms and input without breaking the code.
    /// Because of the simplicity of this example we'll bundle it up in the GameController class - these methods will be accessed by
    /// all game elements querying the input states 
	/// </summary>
	
	/// <summary>
	/// Gets a value indicating whether this instance is mobile.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is mobile; otherwise, <c>false</c>.
	/// </value>
	public bool IsMobile{
		get{
			return (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android); 	
		}
	}
	
	public int TouchCount{
		get{
			if( IsMobile ){
				return Input.touchCount; 
			} else{
				// if its not consdered to be mobile then query the left mouse button, returning 1 if down or 0 if not  
				if( Input.GetMouseButton(0) ){
					return 1; 	
				} else{
					return 0; 
				}
			}
		}
	}
	
	public int TouchDownCount{
		get{
			if( IsMobile ){
				int currentTouchDownCount = 0; 
				foreach( Touch touch in Input.touches ){
					if( touch.phase == TouchPhase.Began ){
						currentTouchDownCount++; 	
					}
				}
				
				return currentTouchDownCount;
			} else{
				// if its not consdered to be mobile then query the left mouse button, returning 1 if down or 0 if not  
				if( Input.GetMouseButtonDown(0) ){
					return 1; 	
				} else{
					return 0; 
				}
			}
		}
	}
	
	#endregion 
	
}
