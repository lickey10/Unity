using UnityEngine;
using System.Collections;

public class loadLevelButton : MonoBehaviour {

	public string LevelToLoad = "camera";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		
		if(LevelToLoad.Length > 0)
			Application.LoadLevel(LevelToLoad);
	}

//	void OnGUI()
//	{
//		if (
//			GUI.Button(
//			// Center in X, 1/3 of the height in Y
//			new Rect(
//			transform.position.x,
//			transform.position.y,
//			240,
//			120
//			)
//			,"", new GUIStyle()
//			)
//			)
//		{
//			if(LevelToLoad.Length > 0)
//				Application.LoadLevel(LevelToLoad);
//		}
//	}
}
