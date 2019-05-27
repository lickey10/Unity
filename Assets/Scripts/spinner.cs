using UnityEngine;
using System.Collections;

public class spinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void  OnCollisionEnter2D(Collision2D currentCollider) 
	{

		float rotSpeed = 60;
		int x = 0;
		int rotations = 40;

		while(rotations > x)
		{
			transform.transform.Rotate(0, 0, rotSpeed * Time.deltaTime, Space.World);
			x++;
		}
		//currentCollider.gameObject.transform.Rotate();
		//currentCollider.gameObject.transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
	}
}
