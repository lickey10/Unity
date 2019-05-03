using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockBallKickBack : MonoBehaviour {

    public float KickBackDelay = 2;
    // force is how forcefully we will push the ball
    public float KickBackForce = 100;
    public bool LockBall = false;
    public bool ballIsLocked = false;
    public GameObject[] CorrespondingLights;

    Rigidbody2D ballRigidbody;
    float origGravityScale = 0;
    bool foundBall = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (LockBall && !ballIsLocked)
        {
            CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().Flash());

            //ballIsLocked = LockBall;

            //if (!LockBall)
            //    StartCoroutine(kickbackBall()); 
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (ballRigidbody == null)
        {
            ballRigidbody = c.gameObject.GetComponent<Rigidbody2D>();
            
            origGravityScale = ballRigidbody.gravityScale;
            ballRigidbody.gravityScale = 0;
            ballRigidbody.velocity = Vector2.zero;
            ballRigidbody.MovePosition(this.transform.position);
            
            if (!LockBall)
            {
                StartCoroutine(kickbackBall());
            }
            else //ball is locked
            {
                ballIsLocked = true;

                LockBallGroupController lockBallGroup = this.transform.parent.GetComponent<LockBallGroupController>();

                if(lockBallGroup != null)
                {
                    lockBallGroup.BallIsLocked(this.gameObject);
                }

                CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().TurnLightOn());
            }

            if (foundBall)//if we found the ball with a ball sensor we want to lock next time it enters this ball lock
            {
                LockBall = true;
                foundBall = false;
            }
        }
    }

    void FoundBall(GameObject trigger)
    {
        foundBall = true;
    }

    IEnumerator kickbackBall()
    {
         yield return new WaitForSeconds(KickBackDelay);

        if (ballRigidbody != null && !LockBall)
        {
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push the ball
            ballRigidbody.gravityScale = origGravityScale;
            ballRigidbody.AddForce((new Vector2(0,-1)) * KickBackForce);

            ballRigidbody = null;

            CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().TurnLightOff());

            ballIsLocked = false;
        }
    }
}
