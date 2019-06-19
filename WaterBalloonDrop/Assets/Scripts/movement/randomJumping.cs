using UnityEngine;
using System.Collections;

public class randomJumping : MonoBehaviour {
	public float JumpingPercentage = 1.03f;
	public float JumpingHeight = 5f;

	float index;
	bool inTheAir = false;
	float startingPositionY = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(startingPositionY == 0)
			startingPositionY = transform.localPosition.y;

		int chosenNumber = Random.Range(1,100);

		if(chosenNumber < JumpingPercentage || inTheAir)
		{
			if(transform.localPosition.y > startingPositionY + 1)
			{
   				string message = "";
			}

			if(inTheAir && transform.localPosition.y <= startingPositionY)
				inTheAir = false;
			else if(transform.localPosition.y >= startingPositionY)
			{
				index += Time.deltaTime;
				float y = Mathf.Abs (JumpingHeight*Mathf.Sin (1*index));
				
				transform.localPosition= new Vector3(transform.localPosition.x,y,0);

				inTheAir = true;
			}
		}
//		
//			if(skippedJumps > jumpingBuffer || inTheAir)
//			{
//				index += Time.deltaTime;
//				float y = Mathf.Abs (1*Mathf.Sin (7f*index));
//
//				if(inTheAir)
//					y = y * -1;
//
//				transform.localPosition= new Vector3(transform.localPosition.x,y,0);
//				inTheAir = !inTheAir;
//
//				skippedJumps = 0;
//			}
//
//			skippedJumps++;
//		}
	}

}
