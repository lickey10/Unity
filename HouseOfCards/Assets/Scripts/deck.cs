using UnityEngine;
using System.Collections;

public class deck : MonoBehaviour {
	private Vector3 screenPoint;
	private GameObject card;
	private Vector3 StartPosition = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		//StartPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		StartPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -50f));
		//offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		card = (GameObject)Instantiate(Resources.Load(getNextCard()));
		//card.transform.position = new Vector3(75f, -45f, -50f);
		card.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, -50f);

		card.GetComponent<Rigidbody>().useGravity = false;
		card.GetComponent<Rigidbody>().detectCollisions = false;
		card.transform.rotation =  Quaternion.Euler(new Vector3(0, 90, 0));
	}

	private string getNextCard()
	{
		string nextCardName = "";

		nextCardName = "Card1";

		return nextCardName;
	}
}
