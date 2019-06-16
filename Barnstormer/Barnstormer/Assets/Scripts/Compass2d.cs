using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

	public Texture2D compass; // compass image 
	public Texture2D needle; // needle image (same size of compass) 
	public Rect r = new Rect(10, 10, 200, 200); // rect where to draw compass 
	float angle; // angle to rotate the needle
	public Transform Target;
	public Transform Player;
	public float North = 0;
	public Vector3 northVector;
	Vector3 dirVector;

	Quaternion rot;// = Quaternion.FromToRotation(North, dirVector);
	//float angle; // angle is what we want
	public Vector3 axis; // but an axis variable must be provided as well


	void Start () 
	{
		//rot = Quaternion.FromToRotation(North, dirVector);

		if(Player == null)
			Player = this.transform;
	}

	void Update()
	{
		// Note -90 compensation cos north is along 2D Y axis
		//rot = (-90 + this.transform.eulerAngles.y - North)* Mathf.Deg2Rad;
	}
	
	void OnGUI()
	{
		dirVector = Target.position - Player.position;
		dirVector.y = 0; // remove the vertical component, if any
		rot = Quaternion.FromToRotation(northVector, dirVector);

		//angle = Quaternion.FromToRotation(Vector3.forward, Target.transform.forward).eulerAngles.y;
		rot.ToAngleAxis(out angle,out axis); // get the angle
		//angle = rot.eulerAngles.y;

		GUI.DrawTexture(r, compass); // draw the compass...
		Vector2 p = new Vector2(r.x+r.width/2,r.y+r.height/2); // find the center
		Matrix4x4 svMat = GUI.matrix; // save gui matrix
		GUIUtility.RotateAroundPivot(angle,p); // prepare matrix to rotate
		GUI.DrawTexture(r,needle); // draw the needle rotated by angle
		GUI.matrix = svMat; // restore gui matrix
	}
}
