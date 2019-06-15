using UnityEngine;
using System.Collections;

/*
 * JSFPanelDefinition template class
 * ==========================
 * 
 * use this as a guide template to create your own custom piece
 * 
 */

public class aPanelTemplate : JSFPanelDefinition {

	/*
	 * EXTRA NOTES
	 * ==================
	 * if you want to manipulate a panel type,
	 * use -> bp.setType( a new piece definition reference, initial strength ) ;
	 * This will destroy the current panel and replace it with the new defined panel.
	 * ** NOTE : onPanelDestroy() will be called for the old panel,
	 *           and onPanelCreate() will be call for the new panel.
	 * 
	 */
	

	// called by Board during GameManager game-start phase
	// different from Start() as that is unity start, not neccessarily the game is set-up yet
	public override void onGameStart(JSFBoard board){
		// do nothing....
	}

	// optional onCreate function to define extra behaviours
	public override void onPanelCreate(JSFBoardPanel bp){
		// default does nothing...
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

	// optional onSkinChange called by BoardPanel when panel changes skin
	public override void onSkinChange(JSFBoardPanel bp) {
		// default does nothing...
	}

	// optional onPanelClicked called by JSFRelay when panel is clicked
	public override void onPanelClicked(JSFBoardPanel bp) {
		// default does nothing...
	}

	// for external scripts to call, will indicate that the panel got hit
	public override bool gotHit(JSFBoardPanel bp){
		// your extra/custom code here...

		base.gotHit(bp); // default behaviour call function ( if needed )
		// ====== the default behaviour below - as reference
		playAudioVisuals(bp); // play audio visual for selected panels
		bp.durability--;
		return true; // tell controller that a hit registered.. ( will swap object skin according to durability )
		// ====== end of default behaviour reference
		// return statement,
		// if true - panel skin will refresh and panel will be active for other checks
		// if false - panel skin will NOT refresh and panel acts as though nothing happened
	}

	// for external scripts to call, if splash damage hits correct panel type, perform the hit
	public override bool splashDamage(JSFBoardPanel bp){
		// define your panel characteristic... typically durability--; else, do nothing...
		// don't forget to include "playAudioVisuals(bp);" if needed...

		return false; // default behaviour
		// return statement,
		// if true - panel skin will refresh and panel will be active for other checks
		// if false - panel skin will NOT refresh and panel acts as though nothing happened
	}
	
	// function to check if pieces can fall into this board box
	// ( AKA piece want to come in? Welcome~! )
	public override bool allowsGravity(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// function to check if pieces can re-appear on this board box
	public override bool allowsAppearReplacement (JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// if the piece here can be added to the swipe chain
	public override bool isSwippable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
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
	// ( AKA reset? change/put a piece here! : true = resets / false = ignore reset )
	public override bool isFillable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
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
