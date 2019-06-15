using UnityEngine;
using System.Collections;


/// <summary> ##################################
/// 
/// NOTICE :
/// This script is just a simple delegate to announce to GameManager
/// on which piece is being dragged and dragged towards which piece.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################

public class JSFPieceTracker : MonoBehaviour {
	
	[HideInInspector] public JSFGameManager gm {get{return JSFUtils.gm;}}
	[HideInInspector] public int[] arrayRef = new int[2]; // a tracker to keep note on which board this piece belongs too..
	int x {get{return arrayRef[0];}} // easy reference for arrayRef[0]
	int y {get{return arrayRef[1];}} // easy reference for arrayRef[1]
	
	void OnMouseUpAsButton(){
		JSFRelay.onPieceClick(x,y);
	}

	void OnMouseDown(){
		JSFSwipeManager.swipeStart(gm.iBoard(arrayRef)); // start a swipe call
	}

	void OnMouseEnter(){
		JSFSwipeManager.swipeCall(gm.iBoard(arrayRef)); // make a swipe chain call
	}
}
