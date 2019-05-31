using UnityEngine;
using System.Collections;

public class fieldOfPins : MonoBehaviour {

	public int Columns = 5;
	public int Rows = 1;
	public GameObject Pin;
	public float BallWidth = .2f;
	public bool SecondRowToTheLeft = true;

	private Vector2 currentPosition;

	// Use this for initialization
	void Start () {
		currentPosition = this.gameObject.transform.position;

		for (int y = 0; y < Rows; y++) {

			for (int x = 0; x < Columns; x++) {

				GameObject newPin=Instantiate(Pin,new Vector3(currentPosition.x ,currentPosition.y,0), Quaternion.identity) as GameObject;
                newPin.GetComponent<SpriteRenderer>().enabled = false;

                currentPosition.x = currentPosition.x + ((BallWidth + .2f));
			}

			if(y % 2 == 0)//the even numbers
				if(SecondRowToTheLeft)
					currentPosition.x = this.gameObject.transform.position.x - (BallWidth/2) - .05f;
				else
					currentPosition.x = this.gameObject.transform.position.x + ((BallWidth + .1f)) ;
			else
			{
				if(SecondRowToTheLeft)
					currentPosition.x = this.gameObject.transform.position.x;
				else
					currentPosition.x = this.gameObject.transform.position.x - (BallWidth/2) - .05f;
				
			}

			currentPosition.y = currentPosition.y - ((BallWidth + .15f));

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
