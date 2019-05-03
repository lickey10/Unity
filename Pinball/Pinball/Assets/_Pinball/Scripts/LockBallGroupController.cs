using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockBallGroupController : MonoBehaviour {

    public GameObject[] LockBalls;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BallIsLocked(GameObject ballLock)
    {
        //check to see if all locks are filled and start multi-ball
        //if all balls aren't locked activate the next ball lock in the group
        if (LockBalls.Where(x => !x.GetComponent<LockBallKickBack>().ballIsLocked).Any())
            LockBalls.Where(x => !x.GetComponent<LockBallKickBack>().ballIsLocked).FirstOrDefault().GetComponent<LockBallKickBack>().LockBall = true;
        else //all balls are locked - start multi-ball
        {
            LockBalls.ToList().ForEach(x => x.GetComponent<LockBallKickBack>().LockBall = false);
        }
    }
}
