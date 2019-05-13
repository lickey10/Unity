using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public float speed = 3;
    public GameObject BloodySaw;

	void Update () 
	{
		transform.Rotate (new Vector3 (0f, 0f, speed));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(BloodySaw)
        {
            //replace current gameobject with BloodySaw
            Instantiate(BloodySaw, transform.position, transform.rotation);

            Destroy(this);
        }
    }
}
