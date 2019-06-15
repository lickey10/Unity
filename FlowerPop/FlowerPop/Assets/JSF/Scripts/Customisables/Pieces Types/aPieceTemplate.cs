using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * JSFPieceDefinition template class
 * ==========================
 * 
 * use this as a guide template to create your own custom piece
 * 
 */

/// <summary>
/// 
/// BELOW ARE THE FUNCTIONS THAT IS USEFUL
/// 
/// ==============
/// WARNING, wrap these in StartCoroutine( "x" );
/// gm.destroyInTime()    <--- this is a function that accepts 4 properties. 
///                       1. the x position.
///                       2. the y position.
///                       3. the delay before the piece is destroyed ( in float value )
///                       4. the score added once the piece is destroyed.
/// gm.destroyInTimeMarked()  <--- same as destroyInTime(), but pieces here will be marked and other
///                                destroy calls will not affect it. (i.e., another bomb will not pre-maturely
///                                destroy the piece.)
/// ===============
/// 
/// yield return new WaitForSeconds(gm.gemSwitchSpeed);  <--- use this to wait for the visual effect above...
/// 
/// 
/// gm.boardWidth / gm.boardHeight    <--- the width and height of the current board
/// gm.board[x,y]    <--- use this to reference the board if you needed more board properties
/// 
/// gm.lockJustCreated(posX1,posY1,0f)   <--- use this to lock the pieces so that it will not be destroyed by
///                                           a chain explosion so quickly.
///                                           var 1 & var 2 = the board array position [x,y]
///                                           var 3 = the delay before it is unlocked
/// 
/// 
/// gm.audioScript.playSound(PlayFx.fxtype) <--- to play sound effects defined in AudioPlayer script
/// gm.animScript.doAnim(animType,x,y) <--- to play your desired anim defined in CustomAnimations script
/// 
/// </summary>

public class aPieceTemplate : JSFPieceDefinition {
	
	// ============================================================
	//  Virtual functions that users can override
	//  or leave it as default behaviours
	//
	// P.S.> you can delete this entire section / any specific function if you are not changing anything...
	// ============================================================

	// called by Board during GameManager game-start phase
	// different from Start() as that is unity start, not neccessarily the game is set-up yet
	public override void onGameStart(JSFBoard board){
		// do nothing....
	}

	// called by JSFRelay during onPieceClicked
	public override void onPieceClicked(JSFGamePiece gp){
		// default does nothing...
		// your own code here if you need
	}
	
	// called by GamePiece during creation of a type
	public override void onPieceCreated(JSFGamePiece gp){
		// default does nothing...
		// your own code here if you need
	}
	
	// called by GamePiece during destruction of a type
	public override void onPieceDestroyed(JSFGamePiece gp){
		// default do nothing...
		// your own code here if you need
	}

	// called by GameManager when player makes the next move
	public override void onPlayerMove(JSFBoard board) {
		// default do nothing...
		// your own code here if you need
	}
	
	// called by GameManager when board stabilize and gets a suggestion
	public override void onBoardStabilize(JSFBoard board) {
		// default do nothing...
		// your own code here if you need
		// think of this as something like "on the next turn"...
	}
	// Optional piece splash function when a piece is destroyed
	public override void splashDamage(JSFBoard board){
		// default do nothing...
		// your own code here if you need
		// splash when a match is formed...
	}
	
	// for external script to call (mainly GamePiece.cs) to call which skin to use
	public override GameObject getSkin(int num){
		return skin[num]; // default behaviour will use back the same color skin
	}

	// different from getSkin() ... this is for piece to specify 
	// the skin to use during spawning new piece ** when you use chanceToSpawnThis()
	public override int skinToUseDuringSpawn(int x, int y){
		// ** x / y is the board position being called for spawning...
		return 0; // default behaviour when called, return skin 0
	}
	
	// when spawning a new piece, chance to spawn this type...
	public override JSFPieceDefinition chanceToSpawnThis(int x, int y){
		// ** x / y is the board position being called for spawning...

		return null; // default does nothing... will create a normal piece instead
		/*
		 * else, 
		 * if( your criteria here...)
		 * return this; // spawns this piece if conditions are met...
		 * WARNING - beware of returning true all the time, then it will only spawn this piece!
		 */
	}
	

	
	// user can further specify the position of the object on top of the default if needed
	public override void extraPiecePositioning(GameObject thisPiece){
		// default is no extra positioning

		// else, in front of the normal board position
		// thisPiece.transform.localPosition += new Vector3(0,0,-1*thisPiece.transform.localScale.z);

		// else, behind the normal board position
		// thisPiece.transform.localPosition += new Vector3(0,0,1*thisPiece.transform.localScale.z);
	}
	
	// the minimum swipe distance between the last piece and this new swiped piece
	public override int minSwipeDistance(JSFGamePiece gp){
		// extra stuff you want want to do...
		return 1; // default is 1.
	}
	
	// the maximum swipe distance between the last piece and this new swiped piece
	public override int maxSwipeDistance(JSFGamePiece gp){
		// extra stuff you want want to do...
		return 1; // default is 1.
	}
	
	// function called when user starts the first swipe touch (the first piece)
	public override bool useAsFirstSwipe(JSFGamePiece gp, bool isBoardCheck) {
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// return true to allow SwipeManager to use this piece as a starting swipe
		// return false to NOT allow SwipeManager to use this piece as a starting swipe
		
		// below is the default behaviour
		// if it's not a special... set the swipeColor to the slotNum; use as first swipe
		if(!isSpecial){ // just making sure it's not a special piece
			JSFSwipeManager.swipeColor = gp.slotNum; // default sets the swipe color
			return true;
		}
		return false;
	}

	// function called when user continues swiping (the next pieces being swiped other than the first) 
	public override bool addToSwipeChain(JSFGamePiece gp, int swipeColor, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// return true to allow SwipeManager to add this piece to the swipe chain
		// return false to NOT allow SwipeManager to add this piece to the swipe chain
		// P.S.> this status (true/false) is IGNORED if JSFSwipeManager.allowAnyColorOnce() is active 
		//          and this piece is not a special (isSpecial == false)!
		
		// below is the default behaviour
		// if it's not a special... if swiped is same color, add to swipe
		if(!isSpecial){
			if(gp.slotNum == swipeColor){ // same color...
				return true; // allow add to chain
			}
		}
		return false; // DO NOT allow add to chain
	}
	
	// when the piece is added to the swipe chain (after addToSwipeChain() )
	public override void onSwipeAdded(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// suggest calling "JSFSwipeManager.allowAnyColorOnce()" here to allow the next piece
		//     to be added without color limitation :)
	}

	// when this piece is removed to the swipe chain
	// P.S.> NOT THE SAME AS onSwipeBackTracked !!
	public override void onSwipeRemoved(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// Use this method to remove any special power triggered during "onSwipeAdd"...
	}

	// when the swipe is illegal, void and being removed...
	// this is per piece call function
	public override void onSwipeFailed(JSFGamePiece gp){
		// default does nothing...
		// p.s.> you can remove active power triggered by onSwipeAdded() here :)
	}
	
	// when the piece is being removed 1 by 1 (swipe is valid and being processed) 
	// this is per piece call function
	public override void onSwipeValidating(JSFGamePiece gp){
		gm.increaseScore(scorePerPiece,gp.master.arrayRef[0],gp.master.arrayRef[1]); // increase score
		foreach(JSFBoard neighbour in gp.master.allNeighbourBoards){ // to all its neighbour boards
			neighbour.SplashDamage(); // splash damage call (causes splash damage)
		}
		if(JSFSwipeManager.lastSwipeChainBoard == gp.master) { // if this is the last piece of the swipe
			for(int w = gm.pieceTypes.Length-1; w >= 0; w--){
				if(gm.pieceTypes[w].createPowerAtSwipeEnd(gp, JSFSwipeManager.currentSwipeCount)){
					return; // finished here.. 
				}
			}
			gp.master.destroyBox(); // no power created... destroys the piece here
		} else {
			gp.master.destroyBox(); // destroys the piece here ( default )
		}
	}


	/// <summary>
	/// Creates the power at swipe end.
	/// </summary>
	/// <returns><c>true</c>, if power at swipe end was created, <c>false</c> otherwise.</returns>
	/// <param name="gp">target Game Piece at the end of the chain</param>
	/// <param name="swipeLength">Swipe length.</param>
	public override bool createPowerAtSwipeEnd(JSFGamePiece gp, int swipeLength){
		// return calls...
		return false; // default call, destroys the piece here at the end...
		// return true; // Only if you do NOT wish the piece here to be destroyed
		
	}
	
	// when the swipe is backtracked to this piece during a swipe
	public override void onSwipeBackTracked(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// this is called when a swipe back-tracked to this piece :)
		// suggest re-calling "JSFSwipeManager.allowAnyColorOnce()" here (if needed)
		//     to allow the next piece to be added without color limitation :)
	}
	
	// performs power during destruction of the piece (if any)
	public override bool performPower(int[] arrayRef){
		// NOTE :- this function is called during piece destruction ( of destroyBox() )
		/* your function call here - define your own power...
		 * e.g. doMyPower(arrayRef);
		 * 
		 * arrayRef is the board position of the piece..
		 * arrayRef[0] = x position; arrayRef[1] = y position.
		 * use it to call gm.board[x,y]
		 * 
		 */
		
		// return calls...
		return false; // default call - will destroy the piece immediately after this ( AKA after calling the power )
		//		return true; // Only if you do not wish the custom piece to be destroyed instantly
		/*
		 * IMPORTANT : 
		 * you must call StartCoroutine( gm.destroyInTimeMarked(x,y, delay, mScore) );
		 * either here or in your power function to manually destroy the piece
		 * ( that is if you returned false; )
		 * 
		*/ 
	}

	// allows you to specify a power merge...
	public override bool powerMerge(List<JSFBoard> chain, JSFGamePiece target,
	                               JSFGamePiece refPiece, int[] arrayRef, bool isACheck){
		return false;
		/* NOTES :-
		 * List<JSFBoard> chain <--- the current swipe chain
		 * arrayRef <--- the current board position the call is being executed
		 * target <--- the current GamePiece being checked for powerMerge
		 * refPiece <--- the GamePiece used as a reference (the piece that owns this PieceDefinition
		 * isACheck <--- signifies whether this call is a check or an execute call
		 * 
		 * return statements :-
		 * return true; <--- tell JSF that there is a powerMerge
		 * return false; <--- tell JSF NO powerMerge happened...
		 * 
		 */
	}
}
