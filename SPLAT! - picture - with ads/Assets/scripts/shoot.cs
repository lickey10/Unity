using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

	public Transform shootPosition;
	public float shootForce = 1000;
	public GameObject prefabBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			//ShootBullet();			
		}
	}

	public void ShootBullet()
	{
		GameObject instanceBullet = (GameObject)Instantiate(prefabBullet, transform.position, shootPosition.rotation);
		instanceBullet.GetComponent<Rigidbody>().AddForce(shootPosition.forward * -1 * shootForce);
	}
}
