using UnityEngine;
using System.Collections;

public class PointDisplay : MonoBehaviour {
	private int points = 5;
	//var Point : float;
	private float GetHitEffect;
	private float targY;
	private Vector3 PointPosition;
	private string pointsToDisplayString = "";
	
	GUISkin PointSkin;
	GUISkin PointSkinShadow;
	GUIStyle pointStyle = new GUIStyle();
	GUIStyle pointStyleShadow = new GUIStyle();

	public void DisplayPoints(int pointsToDisplay)
	{
		PointPosition = this.gameObject.transform.position;// + new Vector3(Random.Range(-1,1),0,Random.Range(-1,1));
		points = pointsToDisplay;
	}

	public void DisplayPoints(int pointsToDisplay,Vector3 pointPosition)
	{
		PointPosition = pointPosition;
		points = pointsToDisplay;
	}

	// Use this for initialization
	void Start () {
		//Point = Mathf.Round(Random.Range(Point/2,Point*2));
		targY = Screen.height /2;

		pointStyle.fontSize = 40;
		pointStyle.normal.textColor = Color.yellow;
		pointStyleShadow.fontSize = 40;
		pointStyleShadow.normal.textColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		targY -= Time.deltaTime*200;
	}

	void OnGUI() {
		Vector3 screenPos2 = Camera.main.GetComponent<Camera>().WorldToScreenPoint (PointPosition);
		GetHitEffect += Time.deltaTime * 30;
		GUI.color = new Color (1.0f, 1.0f, 1.0f, 1.0f - (GetHitEffect - 50) / 7);
		GUI.skin = PointSkinShadow;
		GUI.skin = PointSkin;

		if (points < 0)
			pointsToDisplayString = points.ToString();
		else
			pointsToDisplayString = "+" + points;

		GUI.Label (new Rect (screenPos2.x + 8, targY - 2, 160, 140), pointsToDisplayString,pointStyleShadow);
		GUI.Label (new Rect (screenPos2.x + 10, targY, 240, 240), pointsToDisplayString,pointStyle);
	}
}
