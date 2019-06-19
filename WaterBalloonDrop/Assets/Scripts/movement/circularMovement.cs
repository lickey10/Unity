using UnityEngine;
using System.Collections;

public class circularMovement : MonoBehaviour {

	public Transform center;
	public float degreesPerSecond = -65.0f;
	public int Radius = 1;

	private Vector3 theCenter;
	private Vector3 v;
	
	void Start() {
		theCenter = transform.position;
		theCenter.x = theCenter.x + 0.5f;
		theCenter.y = theCenter.y + 0.5f;

		v = transform.position - center.position;
		//v = transform.position - theCenter;
	}
	
	void Update () {
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;
		//transform.position = theCenter + v;
	}
}
