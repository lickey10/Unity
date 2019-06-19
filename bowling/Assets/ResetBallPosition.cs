using UnityEngine;
using System.Collections;

public class ResetBallPosition : MonoBehaviour {

	public Vector3 _Position;
	
	
	// Use this for initialization
	void OnEnable () {
		_Position = gameObject.transform.position;
	}
	
	public void Reset(int _ball)
	{
		// _ball is ignored for ball reset
		StartCoroutine(DelayBallReset(2));
	}
	
	IEnumerator DelayBallReset(float secs)
	{
		gameObject.renderer.enabled = false;
		gameObject.transform.position = _Position;

		yield return new WaitForSeconds(secs);
		gameObject.renderer.enabled = true;

	}
}
