using UnityEngine;
using System.Collections;

public class FollowTouch : TouchLogicV2//NOTE: This script has been updated to V2 after video recording
{
	public float speed = 1f, maxDist = 2;
	public Transform Player;
	public Transform PlayerHand;
	private Vector3 finger;
	private Transform camTrans;
	private Animator animator;
	
	void Start ()
	{
		//myTrans = this.transform;
		camTrans = Camera.main.transform;
		animator = Player.GetComponent<Animator>();
	}
	
	//separated to own function so it can be called from multiple functions
	void LookAtFinger()
	{
		//z of ScreenToWorldPoint is distance from camera into the world, so we need to find this object's distance from the camera to make it stay on the same plane
		Vector3 tempTouch = new Vector3(Input.GetTouch(touch2Watch).position.x, Input.GetTouch(touch2Watch).position.y,camTrans.position.y - Player.position.y);
		//Convert screen to world space
		finger = Camera.main.ScreenToWorldPoint(tempTouch);
		
		//look at finger position
		Player.LookAt(finger);
		
		//move towards finger if not too close
		if(Vector3.Distance(finger, Player.position) > maxDist)
			Player.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	public override void OnTouchMovedAnywhere()
	{
		LookAtFinger();
	}

	public override void OnTouchStayedAnywhere()
	{
		LookAtFinger();
	}

	public override void OnTouchBeganAnywhere()
	{
		touch2Watch = TouchLogicV2.currTouch;
	}

	void LookAtMouse()
	{
		//z of ScreenToWorldPoint is distance from camera into the world, so we need to find this object's distance from the camera to make it stay on the same plane
		Vector3 tempTouch = new Vector3(Input.mousePosition.x, Input.mousePosition.y,camTrans.position.y - Player.position.y);
		//Convert screen to world space
		finger = Camera.main.ScreenToWorldPoint(tempTouch);
		
		//look at mouse position
		Player.LookAt(finger);
		
		//move towards finger if not too close
		if(Vector3.Distance(finger, Player.position) > maxDist)
		{

			Player.Translate (Vector3.forward * speed * Time.deltaTime);

		}
		else //grab mouse
		{
			if(!animator.GetBool("Hanging"))
				animator.SetBool("Hanging",true);

			Vector3 handLocation = Vector3.forward * speed * Time.deltaTime;
			handLocation.y = Input.mousePosition.y;
			
			Player.Translate (handLocation);
		
		}
	}

	public void OnMouseOver()
	{
		LookAtMouse();
	}

	public void OnMouseDown()
	{
		LookAtMouse();
	}
}