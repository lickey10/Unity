using UnityEngine;
using System.Collections;

public class dragScript : MonoBehaviour {
	bool moving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
			move ();
	}

	void OnMouseDown()
	{
		moving = true;
	}

	void OnMouseUp()
	{
		moving = false;
	}

	void move()
	{
		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		
		
		#if UNITY_EDITOR
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		//for touch device
		#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z);
		
		#endif
		
		var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
		
		//handle run animation
		//		if(Vector3.Distance(transform.position, curPosition) > 1f && !shooting)
		//			animator.SetBool("Moving",true);
		//		else
		//			animator.SetBool("Moving",false);
		
		//move
		transform.position = new Vector3(curPosition.x,transform.position.y,curPosition.y);// - offset;
	}
}
