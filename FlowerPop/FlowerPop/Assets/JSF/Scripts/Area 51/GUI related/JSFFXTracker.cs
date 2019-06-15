using UnityEngine;
using System.Collections;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is the identity and toggle for the music and sound icon.
/// When toggled, it will switch on/off the music/sound.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################


public class JSFFXTracker : MonoBehaviour {
	
	public JSFsoundButtonType IamA = JSFsoundButtonType.FX_ON; // identity enum defined in FXToggle.cs
	JSFFXToggle myMaster;
	bool isScriptBroken;
	
	void OnMouseUpAsButton(){
		if(!isScriptBroken){
			myMaster.slaveClick(IamA); // sends the trigger to the FXToggle.cs script
		}
	}
	
	void Awake(){
		myMaster = GameObject.Find("Sound Buttons").GetComponent<JSFFXToggle>();
		
		if(myMaster == null ){ // notify that the programmer broke something...
			Debug.Log("You changed the \"Sound Button\" game object! revise the FXTracker script!");
			isScriptBroken = true;
		}
	}
}
