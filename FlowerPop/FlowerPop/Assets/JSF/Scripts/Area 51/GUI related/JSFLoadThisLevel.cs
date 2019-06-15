using UnityEngine;
using System.Collections;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is a simple level loader.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################

public class JSFLoadThisLevel : MonoBehaviour {
	public string sceneName = "SceneNameHere"; // changable in the inspector
	
	void OnMouseUpAsButton(){
		Application.LoadLevel(sceneName); // loads the specified level when clicked
	}
}
