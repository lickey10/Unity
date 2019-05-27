using UnityEngine;
using System.Collections;

public class shootBall : MonoBehaviour {

	public float speed = 27.0f;//range is 12 to 27
	public GameObject Ball;
	SwipeManager swipeManager = new SwipeManager();
    private float mouseDownTime = 0f;

	bool swipeHasStarted = false;

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
        
    }

	void FixedUpdate(){
        //0 is for when the left button is clicked, 1 is for the right
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDownTime = Time.time - mouseDownTime;
            //speed = mouseDownTime + 13;//this is the minimum for the power required to shoot the ball

            //if (speed < 13)
            //    speed = 13;

            //if (speed > 27)
            //    speed = 27;

            GameObject newBall = Instantiate(Ball, this.transform.position, Quaternion.identity) as GameObject;

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(-100, 100).normalized;

            //speed = (Time.time - mouseDownTime) * 3f;

            // Set Velocity with dir * speed
            newBall.GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        swipeManager.DetectSwipe();

		//switch(SwipeManager.swipeDirection)
		//{
		//	case SwipeManager.Swipe.SwipingDown:
		//		swipeHasStarted = true;
		//		break;
		//	case SwipeManager.Swipe.SwipingUp:
		//		swipeHasStarted = true;
		//		break;
		//	case SwipeManager.Swipe.None:
		//		if(swipeHasStarted)
		//		{
		//			GameObject newBall=Instantiate(Ball,new Vector3(-1.747414f,-2f,0), Quaternion.identity) as GameObject;
					
		//			// Calculate direction, set length to 1
		//			Vector2 dir = new Vector2(-1, 1).normalized;
					
		//			// Set Velocity with dir * speed
		//			newBall.GetComponent<Rigidbody2D>().velocity = dir * speed;

		//			swipeHasStarted = false;
		//		}
		//		break;
		//}
	}

}
