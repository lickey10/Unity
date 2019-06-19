using UnityEngine;
using System.Collections;

public class sineMovement : MonoBehaviour {

	public float amplitudeX = 5.0f;
	public float amplitudeY = 2.5f;
	public float omegaX = 1.0f;
	public float omegaY = 2.0f;

	float index;
	
	public void Update(){
		index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));

		//if you just use the x value the object will gain speed
		transform.localPosition= new Vector3(transform.localPosition.x,y,0);
	}
}
