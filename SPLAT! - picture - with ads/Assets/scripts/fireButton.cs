using UnityEngine;
using System.Collections;

public class fireButton : MonoBehaviour {

	public Transform shootPosition;
	public float shootForce = 1000;
	public GameObject prefabBullet;

	int X = (Screen.width - 240 ) / 2;
	int Y = (Screen.height + 120) / 2;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() 
	{

		GameObject instanceBullet = (GameObject)Instantiate(prefabBullet,  shootPosition.transform.position, shootPosition.rotation);
		instanceBullet.GetComponent<Rigidbody>().AddForce(shootPosition.forward.normalized * 1 * shootForce);

	}


}
