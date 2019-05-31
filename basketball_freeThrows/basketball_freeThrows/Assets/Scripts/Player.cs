using UnityEngine;
using System.Collections;

/// <summary>
/// Player - responsible for playing the animation and moving the player to the 'shot spot' - controlled by the game controller 
/// </summary>
//[RequireComponent (typeof (Animation))]
public class Player : MonoBehaviour {
	
	/// <summary>
	/// Delegate method used by observing parties of the OnPlayerAnimationFinished event 
	/// </summary>
	public delegate void PlayerAnimationFinished(string animation);
	/// <summary>
	/// Event raised when a non-looping animation is complete 
	/// </summary>
	public PlayerAnimationFinished OnPlayerAnimationFinished = null;	
	
	/// <summary>
	/// Player state enum - determines what animation is played 
	/// </summary>
	public enum PlayerStateEnum{
		Idle, 					// waiting for user interaction (not used) 
		BouncingBall, 			// bouncing the ball (waiting for the user to take the shot) 
		PreparingToThrow, 		// when the user is setting the power of the throw 
		Throwing, 				// when the user releases the throw 
		Score, 					// user scores!!! 
		Miss, 					// user misses
		Walking					// walking to the next spot point 
	}
	
	// reference to all our animations (public so they're accessiable via the Editor) 
	// (nb: we are using external references to the animations rather than in code so that the animations can be easily updated 
	// in the future i.e. we may have 2 different versions of walk and rather than editing the code we can modify it via the editor) 
	public AnimationClip animIdle; 
	public AnimationClip animBounceDown; 
	public AnimationClip animBounceUp; 
	public AnimationClip animWalkForward; 
	public AnimationClip animWalkBackward; 
	public AnimationClip animPrepareThrow; 
	public AnimationClip animThrow; 
	public AnimationClip animScore; 
	public AnimationClip animMiss;

	bool animationIsPlaying = false;
	string exceptionMessage;
	float throwPower = 0f;
	
	/// <summary>
	/// The walk speed of our player moving into position 
	/// </summary>
	public float walkSpeed = 5.0f; 
	
	/// <summary>
	/// Reference to the basket ball
	/// </summary>
	public Ball basketBall; 
	
	/// <summary>
	/// Force applied to the ball when dribbling it 
	/// </summary>
	public float bounceForce = 1900f; 
	
	/// <summary>
	/// max throw force applied to the ball when throwing 
	/// </summary>
	public float maxThrowForce = 500f; 
	
	/// <summary>
	/// The throw direction 
	/// </summary>
	public Vector3 throwDirection = new Vector3( -1.0f, 1.5f, 0.0f ); 
	
	/// <summary>
	/// current player state 
	/// </summary>
	private PlayerStateEnum _state = PlayerStateEnum.Idle; 
	
	/// <summary>
	/// record how long we have been in each state 
	/// </summary>
	private float _elapsedStateTime = 0.0f; 
	
	/// <summary>
	/// The shot position i.e. position the player will stand when taking a shot 
	/// </summary>
	private Vector3 _shotPosition = Vector3.zero; 
	
	/// <summary>
	/// Cached reference to the transform component 
	/// </summary>
	private Transform _transform; 
	
	/// <summary>
	/// Cached reference to the animation component
	/// </summary>
	//private Animation _animation; 
	
	/// <summary>
	/// Cached reference to the components collider 
	/// </summary>
	private CapsuleCollider _collider; 
	
	/// <summary>
	/// Cached reference to our 'ball handling hand'
	/// </summary>
	public Transform _handTransform; 
	
	/// <summary>
	/// Currently playing animation 
	/// </summary>
	private AnimationClip _currentAnimation = null; 	

	private Animator anim;
	
	/// <summary>
	/// flag indicating whether we are holding the ball or not; if we're holding it then we call the method 
	/// AttachAndHoldBall to fix the ball to the players hand 
	/// </summary>
	private bool _holdingBall = false; 
	
	void Awake(){
		// local references for component 
		_transform = GetComponent<Transform>(); 
		//_animation = GetComponent<Animation>(); 
		_collider = GetComponent<CapsuleCollider>();
		
		// find hand transform by traversing through the child components 
		//_handTransform = _transform.Find("BPlayerSkeleton/Pelvis/Hip/Spine/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/BallHand");
				
		// pre-set the shot position 
		_shotPosition = _transform.position; 
	}
	
	// Use this for initialization
	void Start () {			
		anim = GetComponent<Animator>();
	}		
	
	// Update is called once per frame
	void Update () {
						
		if( _holdingBall ){
			AttachAndHoldBall(); 	
		}

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,270,0),100 * Time.deltaTime);
		
		_elapsedStateTime += Time.deltaTime; 
						
		if( _state == PlayerStateEnum.Walking ){
			if(!animationIsPlaying)
			{
				Vector3 pos = _transform.position; 
				pos = Vector3.Lerp( pos, _shotPosition, Time.deltaTime * walkSpeed ); 
				_transform.position = pos; // update position 
				
				// if below a specified threshold then move the player into position and update the state 
				if( (pos - _shotPosition).sqrMagnitude < 1.0f ){
					pos = _shotPosition; 
					
					// raise event 
					if( OnPlayerAnimationFinished != null ){
						OnPlayerAnimationFinished( _currentAnimation.name ); 	
					}
				}
				else
					animationIsPlaying = true;
			}
			else
			{
				State = PlayerStateEnum.BouncingBall; 
				SetCurrentAnimation( animBounceDown );
			}
		} 
		else if( _state == PlayerStateEnum.BouncingBall ){	
			if( _holdingBall ){
				if( GameController.SharedInstance.State == GameController.GameStateEnum.Play && GameController.SharedInstance.TouchDownCount >= 1 ){
					State = PlayerStateEnum.PreparingToThrow;
					return; 
				}
			}

			if( _currentAnimation.name.Equals( animBounceDown.name ) ){
				if( !animationIsPlaying && _holdingBall ){
					// let go of ball
					_holdingBall = false; 
					resetAnimations();
					anim.SetBool("BounceDown",true);
					animationIsPlaying = true;

					// throw ball down 
					basketBall.BallRigidbody.AddRelativeForce( Vector3.down * bounceForce ); 				
				}
			} 
			else if( _currentAnimation.name.Equals( animBounceUp.name ) ){						
				if( !animationIsPlaying ){
					SetCurrentAnimation( animBounceDown ); 
					resetAnimations();
					anim.SetBool("BounceDown",true);
					animationIsPlaying = true;
				}					
			}
		}
		else if( _state == PlayerStateEnum.PreparingToThrow ){
			if( GameController.SharedInstance.State == GameController.GameStateEnum.Play && GameController.SharedInstance.TouchCount == 0 ){
				// throw ball 
				State = PlayerStateEnum.Throwing; // switch states (which will play the throw animation) 
				
				// power based on how long the user held their finger down for 				
				_holdingBall = false; 

				throwPower = Time.time - throwPower;

				basketBall.BallRigidbody.AddRelativeForce( throwDirection * (maxThrowForce * (throwPower * 900)) );
			}
		}
		else if( _state == PlayerStateEnum.Throwing ){
			// turn on the collider as we want the ball to react to the player is it bounces back 
			//if( !_animation.isPlaying && !_collider.enabled ){				
			if(!animationIsPlaying && !_collider.enabled ){				
				_collider.enabled = true; 
			}
		}
	}
	
	/// <summary>
	/// Call this method to get the player to move into position 
	/// </summary>
	/// <value>
	/// The shot position.
	/// </value>
	public Vector3 ShotPosition{
		get{
			return _shotPosition; 
		}
		set{
			AttachAndHoldBall();

			_shotPosition = value; 
			
//			if( Mathf.Abs( _shotPosition.x - _transform.position.x ) < 0.1f ){
//				State = PlayerStateEnum.BouncingBall; 	
//			} else{
				State = PlayerStateEnum.Walking;
//			}
		}
	}	
	
	/// <summary>
	/// Gets a value indicating whether this instance is holding ball.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is holding ball; otherwise, <c>false</c>.
	/// </value>
	public bool IsHoldingBall{
		get{
			return _holdingBall; 	
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether this instance is animating.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is animating; otherwise, <c>false</c>.
	/// </value>
	public bool IsAnimating{
		get{
			return animationIsPlaying; 	
		}
	}
	
	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	/// <value>
	/// The state.
	/// </value>
	public PlayerStateEnum State{
		get{
			return _state; 
		}
		set{
			
			_state = value; 
			// reset the elapsed state time 
			_elapsedStateTime = 0.0f; 
			
			// take action based on state 
			switch( _state ){
			case PlayerStateEnum.Idle:
				SetCurrentAnimation( animIdle ); 				
				break;			
			case PlayerStateEnum.BouncingBall:
				// turn off the collider when we are handling the ball as we don't want any collisions to occur when the user is 
				// bouncing or preparing/and throwing the ball 
				_collider.enabled = false; 
				AttachAndHoldBall(); 			
				SetCurrentAnimation( animBounceUp );				
				//SetCurrentAnimation( animBounceDown );
				resetAnimations();
				anim.SetBool("BounceUp",true);
				animationIsPlaying = true;
				break;
			case PlayerStateEnum.PreparingToThrow:
				SetCurrentAnimation( animPrepareThrow ); 
				resetAnimations();
				anim.SetBool("holdingBall",true);
				throwPower = Time.time;
				break;
			case PlayerStateEnum.Throwing:				
				SetCurrentAnimation( animThrow ); 
				resetAnimations();
				anim.SetBool("ThrowBall",true);
				break;
			case PlayerStateEnum.Score:
				SetCurrentAnimation( animScore ); 
				resetAnimations();
				anim.SetBool("Score",true);
				break;
			case PlayerStateEnum.Miss:
				SetCurrentAnimation( animMiss );
				resetAnimations();
				anim.SetBool("MissedBasket",true);
				break;
			case PlayerStateEnum.Walking:
				// determine what direction we're moving 
				if( _shotPosition.x < _transform.position.x ){
					SetCurrentAnimation( animWalkForward ); 
					resetAnimations();
					anim.SetBool("WalkingForwards",true);
				} else{
					SetCurrentAnimation( animWalkBackward );
					resetAnimations();
					anim.SetBool("WalkingBackwards",true);
				}
				break;
			}															 									
		}
		
	}
	
	/// <summary>
	/// Gets the elapsed state time.
	/// </summary>
	/// <value>
	/// The elapsed state time.
	/// </value>
	public float ElapsedStateTime{
		get{
			return _elapsedStateTime;	
		}
	}
	
	/// <summary>
	/// Gets or sets the current animation.
	/// </summary>
	/// <value>
	/// The current animation.
	/// </value>
	public AnimationClip CurrentAnimation{
		get{
			return _currentAnimation; 
		}
		set{
			// route to the SetCurrentAnimation method
			SetCurrentAnimation( value ); 	
		}
	}
	
	/// <summary>
	/// Sets and plays the specified animation clip - if the animation is not looping then schedule a method invoke when the 
	/// animation is estimated to finish 
	/// </summary>
	/// <param name='animationClip'>
	/// Animation clip.
	/// </param>
	public void SetCurrentAnimation(AnimationClip animationClip){

		_currentAnimation = animationClip; 

//		_animation[_currentAnimation.name].time = 0.0f; 
		//_animation.CrossFade( _currentAnimation.name, 0.1f ); 
		
		// if the animation is not looping then we want to schedule a invoke to fire when the animation is finished 
//		if( _currentAnimation.wrapMode != WrapMode.Loop ){
//			Invoke ("OnAnimationFinished", _animation[_currentAnimation.name].length /  _animation[_currentAnimation.name].speed );
//		}
	}		
	
	/// <summary>
	/// callback when an animation is finished playing 
	/// </summary>
//	private void OnAnimationFinished(){	
//		
//		if( OnPlayerAnimationFinished != null ){
//			OnPlayerAnimationFinished( _currentAnimation.name ); 	
//		}		
//	}
	
	/// <summary>
	/// Method that attaches the ball to the players ball hand; 
	/// Attach meaning: Position under the ball hand, remove velocity, and remove rotation
	/// </summary>
	public void AttachAndHoldBall(){
		_holdingBall = true;
		//resetAnimations();
		//anim.SetBool("holdingBall",true);
		
		Transform bTransform = basketBall.BallTransform; 
		SphereCollider bCollider = basketBall.BallCollider;  
		Rigidbody bRB = basketBall.BallRigidbody; 
		
		// remove velocity 
		bRB.velocity = Vector3.zero; 
		
		// reset the rotation 
		bTransform.rotation = Quaternion.identity; 
		
		// set the position of the ball just below the players hand 
		Vector3 bPos = bTransform.position; 		
		bPos = _handTransform.position; 
		bPos.y -= bCollider.radius;
		bPos.x -= 1;
		bTransform.position = bPos;						
		
	}
	
	
	public void OnTriggerEnter(Collider collider) {
		if( _state == PlayerStateEnum.BouncingBall ){
			if( !_holdingBall && collider.transform == basketBall.BallTransform ){
				AttachAndHoldBall(); 			
				SetCurrentAnimation( animBounceUp ); 
				resetAnimations();
				anim.SetBool("BounceUp",true);
				animationIsPlaying = true;
			}
		}
	}

	public void AnimationStopped()
	{
		animationIsPlaying = false;
	}

	void resetAnimations()
	{
		anim.SetBool("holdingBall",false);
		anim.SetBool("WalkingForwards",false);
		anim.SetBool("WalkingBackwards",false);
		anim.SetBool("ThrowBall",false);
		anim.SetBool("MissedBasket",false);
		anim.SetBool("Score",false);
		anim.SetBool("BounceDown",false);
		anim.SetBool("BounceUp",false);
	}
}
