using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
[RequireComponent (typeof (Rigidbody))]
public class Ball : MonoBehaviour {
	
	/// <summary>
	/// Cached reference to the transform component 
	/// </summary>
	private Transform _transform; 
	
	/// <summary>
	/// Cached reference to the rigidbody 
	/// </summary>
	private Rigidbody _rigidbody; 
	
	/// <summary>
	/// cache reference to the SphereCollider 
	/// </summary>
	private SphereCollider _sphereCollider; 
	
	/// <summary>
	/// Delegate method used by observing parties of the OnNet event 
	/// </summary>
	public delegate void Net();
	/// <summary>
	/// Event raised when a collision occurs on the net collider  
	/// </summary>
	public Net OnNet = null;
	
	/// <summary>
	/// cached reference to the game controller
	/// </summary>
	private GameController _gameController; 
	
	void Awake(){
		_transform = GetComponent<Transform>(); 
		_rigidbody = GetComponent<Rigidbody>(); 
		_sphereCollider = GetComponent<SphereCollider>(); 
	}
	
	// Use this for initialization
	void Start () {			
		_gameController = GameController.SharedInstance; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Transform BallTransform{
		get{
			return _transform; 
		}	
	}
	
	public Rigidbody BallRigidbody{
		get{
			return _rigidbody;	
		}
	}
	
	public SphereCollider BallCollider{
		get{
			return _sphereCollider; 	
		}
	}
	
	// notify game controller for all collisions
	public void OnCollisionEnter( Collision collision ){
		_gameController.OnBallCollisionEnter( collision );
	}
	
	public void OnTriggerEnter( Collider collider ){
		if( collider.transform.name.Equals ( "LeftHoop_001" ) ){
			if( OnNet != null ){
				OnNet(); 	
			}	
		}		
	}
}
