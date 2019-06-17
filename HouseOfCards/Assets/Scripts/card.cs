using UnityEngine;
using System.Collections;

public class card : MonoBehaviour {
	public float moveSpeed = 15;
	private bool dragging = false;
	private Vector3 screenPoint;
	private Vector3 offset;
	float doubleClickStart = 0;
	float newXRotation = 0f;
	GameObject extraCard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

//	void moveCard()
//	{
//		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
//		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
//		var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
//		
//		
//		#if UNITY_EDITOR
//		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
//		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
//		curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
//		//for touch device
//		#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
//		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
//		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z));
//		curScreenSpace = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z);
//		
//		#endif
//		
//		var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
//		
//		transform.position = new Vector3(curPosition.x,transform.position.y,transform.position.z);
//	}

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		
	}

	void OnMouseUp()
	{
		if ((Time.time - doubleClickStart) < 0.3f)
		{
			this.OnDoubleClick();
			doubleClickStart = -1;
		}
		else
		{
			doubleClickStart = Time.time;
		}
	}
	

	void OnDoubleClick()
	{
		//rotate by 45 degrees on x axis
		rotateCard();
	}

	//rotate by 45 degrees on x axis
	private void rotateCard()
	{
		float newX = 0;
		float newY = 0;
		float xyDifference = 0;
		
		//rotate card 45 degrees on x
		transform.Rotate(45,0,0);
		
		newX = Mathf.RoundToInt(transform.eulerAngles.x);
		newY = Mathf.RoundToInt(transform.eulerAngles.y);
		xyDifference = Mathf.Abs(newX - newY);
		
		if(xyDifference == 45) //this is the left card
		{
			transform.Rotate(-15,0,0);
			
			//need to instantiate the other card o lean against
			extraCard = (GameObject)Instantiate(Resources.Load("Card1"));
			extraCard.GetComponent<card>().enabled = false;
			extraCard.transform.rotation = transform.rotation;
			extraCard.transform.Rotate(120,0,0);
			extraCard.transform.position = new Vector3(transform.position.x + 7.5f, transform.position.y - 1,transform.position.z);
			extraCard.GetComponent<Rigidbody>().useGravity = false;
		}
		else if(xyDifference == 225) //this is the right card
		{
			transform.Rotate(15,0,0);
			
			//need to instantiate the other card o lean against
			extraCard = (GameObject)Instantiate(Resources.Load("Card1"));
			extraCard.GetComponent<card>().enabled = false;
			extraCard.transform.rotation = transform.rotation;
			extraCard.transform.Rotate(90,0,0);
			extraCard.transform.position = new Vector3(transform.position.x - 8f, transform.position.y + 1,transform.position.z);
			extraCard.GetComponent<Rigidbody>().useGravity = false;
		}
		else if(xyDifference == 15)
		{
			transform.Rotate(15,0,0);
			
			Transform.Destroy(extraCard);
		}
		else if(xyDifference == 75) 
		{
			transform.Rotate(-15,0,0);
			
			Transform.Destroy(extraCard);
		}
	}
}
