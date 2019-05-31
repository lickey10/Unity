using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerBallHand; an extension to the Player component (class) and is responsile for notifying the Player when an trigger event 
/// is intercepted. 
/// NB; Collision are propagated through parents that have a collider attached to them; but because we don't want to 
/// 'collide' with any object (but rather react) we are using Triggers 
/// </summary>
[RequireComponent (typeof (Collider))]
public class PlayerBallHand : MonoBehaviour {
	
	/// <summary>
	/// Cached reference to the Player component (contained within one of the parents) 
	/// </summary>
	private Player _player = null; 
	
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		Transform parent = transform.parent; 
		while( parent != null && _player == null ){
			Player parentPlayer = parent.GetComponent<Player>(); 	
			if( parentPlayer != null ){
				_player = parentPlayer;
			} else{
				parent = parent.parent; 	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Raises the trigger enter event - delegate to the parent player component 
	/// </summary>
	/// <param name='collider'>
	/// Collider.
	/// </param>
	void OnTriggerEnter( Collider collider ){
		_player.OnTriggerEnter( collider ); 
	}
}
