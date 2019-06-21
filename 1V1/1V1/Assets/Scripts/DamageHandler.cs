using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {
    public GameObject[] strikingObjects;//fist,foot, head, etc

	// Use this for initialization
	void Start () {
	    //add collliders to each strikingObject and set isTrigger to true
        //foreach(GameObject strikingObject in strikingObjects)
        //{
            //strikingObject.AddComponent<BoxCollider>().isTrigger = true;
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hit)
    {
        float damage = 0;

        if (hit.gameObject.tag == "Fighter" && hit.relativeVelocity.magnitude > 1f)
        {
            damage = hit.relativeVelocity.magnitude;
            Debug.Log("Ouch!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Fighter")
        //    SendMessage("ApplyDamage", 10, SendMessageOptions.DontRequireReceiver);
    }
}
