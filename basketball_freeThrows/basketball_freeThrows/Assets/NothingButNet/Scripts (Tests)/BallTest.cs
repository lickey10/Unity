using UnityEngine;
using System.Collections;

public class BallTest : MonoBehaviour {
	
	public Ball ball; 
	
	void Start () {
		ball.OnNet += Handle_OnNet; 
	}
	
	protected void Handle_OnNet(){
		Debug.Log ( "NOTHING BUT NET!!!" );	
	}
}
