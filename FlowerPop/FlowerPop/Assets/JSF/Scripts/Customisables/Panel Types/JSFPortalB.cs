using UnityEngine;
using System.Collections;

public class JSFPortalB : JSFPanelDefinition {

//	public List<Board> boardA; // the board A - entry path reference
//	public List<Board> boardB; // the board B - exit path reference
	
	
	void Awake(){
//		boardA.Clear(); // clears any old references before continuing
//		boardB.Clear(); // clears any old references before continuing
	}

	// called by Board during GameManager game-start phase
	// different from Start() as that is unity start, not neccessarily the game is set-up yet
	public override void onGameStart(JSFBoard board){
		// nothing...
	}

	// optional onCreate function to define extra behaviours
	public override void onPanelCreate(JSFBoardPanel bp){
		// add swirl tweening...
		LeanTween.rotateAround(bp.backPanel,Vector3.back,359f,3.0f).setLoopType(LeanTweenType.clamp);
	}
	// optional onDestroy function to define extra behaviours
	// not the same as being hit... this is when the panel is destroyed completely and changing types
	public override void onPanelDestroy(JSFBoardPanel bp){
		// default does nothing...
	}
	// optional onPlayerMove called by GameManager when player makes the next move
	public override void onPlayerMove(JSFBoardPanel bp){
		// default does nothing...
	}
	// optional onBoardStabilize called by GameManager when board stabilize and gets a suggestion
	public override void onBoardStabilize(JSFBoardPanel bp) {
		// default does nothing...
	}

	// for external scripts to call, will indicate that the panel got hit
	public override bool gotHit(JSFBoardPanel bp){
		return false; // do nothing...
	}

	// for external scripts to call, if splash damage hits correct panel type, perform the hit
	public override bool splashDamage(JSFBoardPanel bp){
		return false; // do nothing...
	}
	
	// function to check if pieces can fall into this board box
	// ( AKA piece want to come in? Welcome~! )
	public override bool allowsGravity(JSFBoardPanel bp){
		// your logic here ( if needed )
		return false;
	}
	
	// function to check if pieces can re-appear on this board box
	public override bool allowsAppearReplacement (JSFBoardPanel bp){
		// your logic here ( if needed )
		return false;
	}
	
	// if the piece here can be added to the swipe chain
	public override bool isSwippable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return false;
	}
	
	// if the piece here (if any) can be destroyed / Matched
	public override bool isDestructible(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// function to check if pieces can be stolen from this box by gravity 
	// ( AKA piece leaving the box when gravity calls )
	public override bool isStealable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// function to for resetBoard() to know which panel can be resetted
	public override bool isFillable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return false;
	}
	
	// function to check if this board is a solid panel
	// ( AKA piece, NO ENTRY!! ROADBLOCK~!- IMPORTANT, not the same of allowsGravity()~!
	// this function determines if pieces will landslide it's neighbouring piece to fill bottom blocks)
	public override bool isSolid(JSFBoardPanel bp){
		// your logic here ( if needed )
		return false;
	}

	// function to play the audio visuals of this panel
	public override void playAudioVisuals (JSFBoardPanel bp){
		// define your audio visual call here...
		// e.g. >
//		master.gm.audioScript.playSound(PlayFx.YOUR DEFINED AUDIO);
//		master.gm.animScript.doAnim(animType.YOUR DEFINED ANIM, master.arrayRef[0], master.arrayRef[1] );
	}


}
