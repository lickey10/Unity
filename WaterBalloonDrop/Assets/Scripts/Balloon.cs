using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {
	public Vector3 BalloonTarget;
	PointDisplay PointScript;
	private int carScore = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(gameObject.transform.position, BalloonTarget, Time.deltaTime * 2);

		//if balloon has reached it's target turn on gravity
		if ((transform.position - BalloonTarget).sqrMagnitude < .001)
			GetComponent<Rigidbody>().useGravity = true;
	}

	void OnCollisionEnter(Collision col)
	{
		//get points if any
		Car carScript = col.gameObject.GetComponent ("Car") as Car;
		if (carScript != null)//this is a car - get the points
			carScore = carScript.CarScore;
		else
			carScore = 0;

		//display points popup if any
		//PointScript = gameObject.GetComponent("PointDisplay") as PointDisplay;
		PointScript = col.gameObject.AddComponent<PointDisplay>() as PointDisplay;
		if(PointScript != null)
			PointScript.DisplayPoints(carScore * gamestate.Instance.getActiveLevel(),this.gameObject.transform.position);

		//destroy balloon object
		Destroy (this.gameObject);

		//make balloon pop sound
		SoundEffectsHelper.Instance.MakeBallonPopSound ();

		//instead of lives we are actually tracking balloons
		gamestate.Instance.SetLives (gamestate.Instance.getLives () - 1);
	}
}
