using UnityEngine;
using System.Collections;

public class pictureHitScript : MonoBehaviour {

	public GameObject TheSplatter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		GameObject instanceSplatter = (GameObject)Instantiate(TheSplatter, col.gameObject.transform.position, transform.rotation);


		Destroy(col.gameObject);

	}
}
