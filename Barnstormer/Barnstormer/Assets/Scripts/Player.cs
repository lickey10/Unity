using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Animator animator;
	bool triggered = false;
	bool shooting = false;
	int kickPower = 10;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		move ();

		if (Input.GetButtonDown("Fire1"))
		{
			shooting = true;
			animator.SetBool("Shooting",true);
		}
	}

	void move()
	{
		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		
		
		#if UNITY_EDITOR
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		//for touch device
		#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z);
		
		#endif
		
		var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

		//handle run animation
//		if(Vector3.Distance(transform.position, curPosition) > 1f && !shooting)
//			animator.SetBool("Moving",true);
//		else
//			animator.SetBool("Moving",false);

		//move
		transform.position = new Vector3(curPosition.x,transform.position.y,curPosition.y);
	}

	/// <summary>
	/// Controls the ball after it hits the paddle
	/// </summary>
	/// <param name="collisionInfo">Collision info.</param>
	void OnCollisionExit(Collision collisionInfo ) {
		if(shooting && collisionInfo.gameObject.tag == "Ball")
		{
			//SoundEffectsHelper.Instance.MakePaddleHitSound();
			
			//Add X velocity..otherwise the ball would only go up&down
			Rigidbody rigid = collisionInfo.rigidbody;
			float xDistance = rigid.position.x - transform.position.x;
			
			if(xDistance > -1.5f)
				xDistance = -1.5f;
			
			rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y*2, rigid.velocity.z*4);
		}
	}

//	void OnControllerColliderHit(ControllerColliderHit hit) {
//		Rigidbody body = hit.collider.attachedRigidbody;
//		if (body == null || body.isKinematic)
//			return;
//		
//		if (hit.moveDirection.y < -0.3F)
//			return;
//
//		hit.rigidbody.AddForce(hit.moveDirection * kickPower);
//		
////		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
////		body.velocity = pushDir * pushPower;
//	}

//	void OnMouseDown()
//	{
//		animator.SetBool("Shooting",true);
//	}
//
//	void OnMouseUp()
//	{
//		Invoke("StopShooting", 10);
//	}
//
//	void OnMouseOver()
//	{
//		string test = "";
//	}
//	
//	void OnMouseEnter()
//	{
//		//		if(createBubblesScript.CrazyMode)
//		//			popBubble();
//		string test = "";
//	}
//	
//	void OnMouseExit()
//	{
//		string test = "";
//	}

	public void StopShooting()
	{
		animator.SetBool("Shooting",false);
		shooting = false;

		transform.Rotate(0,105,0,Space.Self);
	}

	public void StopMoving()
	{
		animator.SetBool("Moving",false);
	}

	//function to check if its a goal
	void OnTriggerEnter(Collider other) {
		//check if the football has triggered an object named GoalLine and triggered is not true
		if(other.gameObject.name == "GoalLine" && !triggered){
			//if the scorer is the player
//			if(turn==0)    
//				scorePlayer++; //increment the goals tally of player
//			//if the scorer is the opponent
//			else
//				scoreOpponent++; //increment the goals tally of opponent
			
			triggered = true;       //check triggered to true
			
		}       
	}
}
