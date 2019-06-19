using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class player : MonoBehaviour {

	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForce = new Vector2(0, 300);
	public bool DieOnCollision = false;
	public List<GameObject> ObjectsToSpawn = new List<GameObject>();
	public GameObject LeftHandSpawner;
	public GameObject RightHandSpawner;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 50.0f;

	private bool moveBalloon = false;
	private GameObject balloon;

	void Awake() {

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gamestate.Instance.DisplayingMenu && gamestate.Instance.getLives () > 0) //they still have balloons to drop
		{
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray;
			Vector3 raycastHit;
			bool clickIsOnRightHalfOfScreen = true;

			//for unity editor
			#if UNITY_EDITOR
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Input.GetTouch (0).position

			if (Input.GetMouseButtonDown (0)) {
				
				//Check if the ray hits any collider
				if (Physics.Raycast (ray, out hit)) {
					//save the click / tap position
					raycastHit = hit.point;
					
					//create balloon object and pass in target
					var mousePos = Input.mousePosition;
					
					if (mousePos.x > Screen.width / 2)
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], RightHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
					else
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], LeftHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
					
					balloon.GetComponent<Balloon> ().BalloonTarget = raycastHit;
				}
			}
			//for touch device
			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)

			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				ray = Camera.main.ScreenPointToRay(touch.position);

				//Check if the ray hits any collider
				if (Physics.Raycast (ray, out hit)) {
					//save the click / tap position
					raycastHit = hit.point;

					//get which side was touched
					Vector2 touchPosition = touch.position;

					if (touchPosition.x > Screen.width / 2)//the right side was touched
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], RightHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
					else
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], LeftHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
					
					balloon.GetComponent<Balloon> ().BalloonTarget = raycastHit;
				}
			}
			#else
			ray = Camera.main.ScreenPointToRay (Input.mousePosition); 

			if (Input.GetMouseButtonDown (0)) {
				
				//Check if the ray hits any collider
				if (Physics.Raycast (ray, out hit)) {
					//save the click / tap position
					raycastHit = hit.point;

					//create balloon object and pass in target
					var mousePos = Input.mousePosition;

					if (mousePos.x > Screen.width / 2)
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], RightHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
					else
						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], LeftHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;

					balloon.GetComponent<Balloon> ().BalloonTarget = raycastHit;
				}
			}
			#endif

			//Check if the ray hits any collider
//			if (Physics.Raycast (ray, out hit)) {
//				//save the click / tap position
//				raycastHit = hit.point;
//				
//				//create balloon object and pass in target
//				if (clickIsOnRightHalfOfScreen)
//					balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], RightHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
//				else
//					balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], LeftHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
//				
//				balloon.GetComponent<Balloon> ().BalloonTarget = raycastHit;
//			}

			// drop balloon
//			if (Input.touchCount > 0) {
//				Touch touch = Input.GetTouch (0);
//			
//				if (touch.phase == TouchPhase.Began) {
//					//get which side was touched
//					Vector2 touchPosition = touch.deltaPosition;
//
//					if (touchPosition.x > Screen.width / 2)//the right side was touched
//						Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], touchPosition, Quaternion.identity);
//					else
//						Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], touchPosition, Quaternion.identity);
//				}
//			} else if (Input.GetMouseButtonDown (0)) {
//
//				//Check if the ray hits any collider
//				if (Physics.Raycast (ray, out hit)) {
//					//save the click / tap position
//					raycastHit = hit.point;
//
//					//create balloon object and pass in target
//				
//				
//					var mousePos = Input.mousePosition;
//
//					if (mousePos.x > Screen.width / 2)
//						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], RightHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
//					else
//						balloon = Object.Instantiate (ObjectsToSpawn [Random.Range (0, ObjectsToSpawn.Count)], LeftHandSpawner.transform.position, Quaternion.Euler (0, 0, 180)) as GameObject;
//
//					balloon.GetComponent<Balloon> ().BalloonTarget = raycastHit;
//				}
//			}
		}
	}
	
	// Die by collision
//	void OnCollisionEnter2D(Collision2D other)
//	{
//		if(other.collider is PolygonCollider2D)
//		{
//			SpecialEffectsHelper.Instance.Explosion(transform.position);
//
//			SoundEffectsHelper.Instance.MakeExplosionSound();
//
//			if(DieOnCollision)
//				gamestate.Instance.SetHealth(gamestate.Instance.GetHealth() - 1);
//
//			if(gamestate.Instance.GetHealth() <= 0)
//			{
//				this.gameObject.SetActive(false);
//				Invoke("Die",2f);	
//			}
//		}
//	}

//	void OnTriggerExit2D(Collider2D coll) {
//		if(coll is BoxCollider2D)
//			gamestate.Instance.SetScore(gamestate.Instance.getScore() + 1);
//	}

	void Die()
	{
		Destroy(this.gameObject);

		gamestate.Instance.SetLives(gamestate.Instance.getLives() - 1);

		if(gamestate.Instance.getLives() <= 0)
		{
			Application.LoadLevel("gameover");
		}
		else
			Application.LoadLevel(Application.loadedLevel);
	}
}
