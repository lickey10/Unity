using UnityEngine;
using System.Collections;

public class wonderingMovement : MonoBehaviour {

	public float amplitudeX = 5.0f;
	public float amplitudeY = 2.5f;
	public float omegaX = 1.0f;
	public float omegaY = 2.0f;
	float index;
	bool movingLeft = false;
	bool movingRight = false;
	bool moving = false;
	private Random randomGenerator = new Random();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
		transform.localPosition= new Vector3(x,y,0);

		//do we want to change directions?
		if(changeDirection())
		{
			//which direction to we want to go?
			if(moveLeft())
				transform.localPosition= new Vector3(amplitudeX - 15,0,0);
			else if(moveRight())
				transform.localPosition= new Vector3(amplitudeX + 15,0,0);

		}

		moving = !movingLeft && !movingRight;
	}

	/// <summary>
	/// Determines if we should move to the left
	/// </summary>
	/// <returns><c>true</c>, if should move left, <c>false</c> otherwise.</returns>
	private bool moveLeft()
	{
		return Random.value < 0.5f;
	}

	private bool moveRight()
	{
		return Random.value < 0.5f;
	}

	private bool changeDirection()
	{
		bool doWeChangeDirection = Random.value < 0.5f;
		return doWeChangeDirection;
	}
}
