using UnityEngine;
using System.Collections;

public class shootBall : MonoBehaviour {

	public float speed = 30.0f;
	public GameObject Ball;
	SwipeManager swipeManager = new SwipeManager();

	bool swipeHasStarted = false;

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		//0 is for when the left button is clicked, 1 is for the right
//		if(Input.GetMouseButtonDown(0))
//		{
//			GameObject newBall=Instantiate(Ball,new Vector3(-1.747414f,-2f,0), Quaternion.identity) as GameObject;
//
//			// Calculate direction, set length to 1
//			Vector2 dir = new Vector2(-1, 1).normalized;
//			
//			// Set Velocity with dir * speed
//			newBall.rigidbody2D.velocity = dir * speed;
//
//		}

		swipeManager.DetectSwipe();

		switch(SwipeManager.swipeDirection)
		{
			case SwipeManager.Swipe.SwipingDown:
				swipeHasStarted = true;
				break;
			case SwipeManager.Swipe.SwipingUp:
				swipeHasStarted = true;
				break;
			case SwipeManager.Swipe.None:
				if(swipeHasStarted)
				{
					GameObject newBall=Instantiate(Ball,new Vector3(-1.747414f,-2f,0), Quaternion.identity) as GameObject;
					
					// Calculate direction, set length to 1
					Vector2 dir = new Vector2(-1, 1).normalized;
					
					// Set Velocity with dir * speed
					newBall.rigidbody2D.velocity = dir * speed;

					swipeHasStarted = false;
				}
				break;
		}
	}

}
