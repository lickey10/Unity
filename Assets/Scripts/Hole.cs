using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
    bool flowerOpen = true;
    public GameObject LeftPetal;
    public GameObject RightPetal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		// This function is called whenever the ball
		// collides with something
		
		Destroy(col.gameObject);

        if (LeftPetal != null && RightPetal != null)
        {
            if (LeftPetal.transform.rotation.z != 0)
            {
                iTweenEvent.GetEvent(LeftPetal, "CloseLeftPetal").Play();
                iTweenEvent.GetEvent(RightPetal, "CloseRightPetal").Play();
            }
            else
            {
                iTweenEvent.GetEvent(LeftPetal, "OpenLeftPetal").Play();
                iTweenEvent.GetEvent(RightPetal, "OpenRightPetal").Play();
            }
        }
    }

    void OnGUI() {
		
		
	}
}
