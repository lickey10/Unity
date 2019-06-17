using UnityEngine;
using System.Collections;

public class CrateHandler : MonoBehaviour {
    public Transform explosionPrefab;
    public Transform Dynamite;
    public float Radius = 5.0F;
    public float Power = 10.0F;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == gameObject.tag)//these are the same type of crate
        {
            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            Instantiate(Dynamite, pos, rot);
            
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
