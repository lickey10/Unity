using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * JSFPieceDefinition main class
 * ==========================
 * 
 * it is the holder as well as the definition of all its children.
 * There are functions all children must implement as well as certain functions
 * that users can change to suit their piece properly.
 * 
 * 
 */

public enum SkinList {Auto, SquareList, HexList};
public abstract class JSFPieceDefinition : MonoBehaviour {

	public bool isSpecial = false;
	public bool isDestructible = true;
	public bool allowGravity = true;
	public bool landslideEffect = false;
	public bool ignoreReset = false;
	public int scorePerPiece = 0;
	public SkinList skinListToUse = SkinList.SquareList;


	public GameObject[] skinSquare; // how the piece will look like for square mode
	public GameObject[] skinHex; // how the piece will look like for hex mode
	[HideInInspector] public JSFGameManager gm {get{return JSFUtils.gm;}} // easy reference call

	public GameObject[] skin{get { // skin variable for game engine to call
			switch(skinListToUse){
			case SkinList.Auto : default:
				switch(gm.boardType){
				case JSFBoardType.Square : default :
					return skinSquare;
				case JSFBoardType.Hexagon :
					return skinHex;
				}
			case SkinList.SquareList :
				return skinSquare;
			case SkinList.HexList :
				return skinHex;
			} } }

	//
	//  Virtual functions that users can override
	//  or leave it as default behaviours
	//

	// called by Board during GameManager game-start phase
	// different from Start() as that is unity start, not neccessarily the game is set-up yet
	public virtual void onGameStart(JSFBoard board){
		// do nothing....
	}

	// called by GamePiece during creation of a type
	public virtual void onPieceCreated(JSFGamePiece gp){
		// do nothing....
	}

	// called by GamePiece during destruction of a type
	public virtual void onPieceDestroyed(JSFGamePiece gp){
		// do nothing...
	}

	// called by JSFRelay during onPieceClicked
	public virtual void onPieceClicked(JSFGamePiece gp){
		// do nothing...
	}

	// called by GameManager when player makes the next move
	public virtual void onPlayerMove(JSFBoard board) {
		// do nothing...
	}

	// called by GameManager when board stabilize and gets a suggestion
	public virtual void onBoardStabilize(JSFBoard board) {
		// do nothing...
	}
	// Optional piece splash function when a piece is destroyed
	public virtual void splashDamage(JSFBoard board){
		// do nothing...
	}

	// for external script to call (mainly GamePiece.cs) to call which skin to use
	public virtual GameObject getSkin(int num){
		return skin[num];
	}

	// when spawning a new piece by gravity, chance to spawn a type defined...
	public virtual JSFPieceDefinition chanceToSpawnThis(int x, int y){
		// ** x / y is the board position being called for spawning...
		return null; // default does nothing... will create a normal piece instead
	}

	// different from getSkin() ... this is for piece to specify 
	// the skin to use during spawning new piece ** when you use chanceToSpawnThis()
	public virtual int skinToUseDuringSpawn(int x, int y){
		// ** x / y is the board position being called for spawning...
		return 0;
	}

	// user can further specify the position of the object on top of the default if needed
	public virtual void extraPiecePositioning(GameObject thisPiece){
		// default is no extra positioning
	}

	// the minimum swipe distance between the last piece and this new swiped piece
	public virtual int minSwipeDistance(JSFGamePiece gp){
		// extra stuff you want want to do...
		return 1; // default is 1.
	}
	
	// the maximum swipe distance between the last piece and this new swiped piece
	public virtual int maxSwipeDistance(JSFGamePiece gp){
		// extra stuff you want want to do...
		return 1; // default is 1.
	}

	// function called when user starts the first swipe touch (the first piece)
	public virtual bool useAsFirstSwipe(JSFGamePiece gp, bool isBoardCheck) {
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// return true to allow SwipeManager to use this piece as a starting swipe
		// return false to NOT allow SwipeManager to use this piece as a starting swipe

		// below is the default behaviour
		// if it's not a special... set the swipeColor to the slotNum; use as first swipe
		if(!isSpecial){ // just making sure it's not a special piece
			JSFSwipeManager.swipeColor = gp.slotNum;
			return true;
		}
		return false;
	}

	// function called when user continues swiping (the next pieces being swiped other than the first) 
	public virtual bool addToSwipeChain(JSFGamePiece gp, int swipeColor, bool isBoardCheck){
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
	public virtual void onSwipeAdded(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// suggest calling "JSFSwipeManager.allowAnyColorOnce()" here to allow the next piece
		//     to be added without color limitation :)
	}

	// when this piece is removed to the swipe chain
	// P.S.> NOT THE SAME AS onSwipeBackTracked !!
	public virtual void onSwipeRemoved(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// Use this method to remove any special power triggered during "onSwipeAdd"...
	}

	// when the swipe is illegal, void and being removed...
	// this is per piece call function
	public virtual void onSwipeFailed(JSFGamePiece gp){
		// default does nothing...
		// p.s.> you can remove active power triggered by onSwipeAdded() here :)
	}

	// when the piece is being removed 1 by 1 (swipe is valid and being processed) 
	// this is per piece call function
	public virtual void onSwipeValidating(JSFGamePiece gp){
		if(gp.markedForDestroy){
			return; // already marked for future destroy, do not proceed...
		}
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
	public virtual bool createPowerAtSwipeEnd(JSFGamePiece gp, int swipeLength){
		// return calls...
		return false; // default call, destroys the piece here at the end...
		// return true; // Only if you do NOT wish the piece here to be destroyed

	}

	// when the swipe is backtracked to this piece during a swipe
	// P.S.> NOT THE SAME AS onSwipeRemoved() !!
	public virtual void onSwipeBackTracked(JSFGamePiece gp, bool isBoardCheck){
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// default does nothing...
		// this is called when a swipe back-tracked to this piece :)
	}

	// performs power during destruction of the piece (if any)
	public virtual bool performPower(int[] arrayRef){
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
		 * you must call gm.destroyInTimeMarked(x,y, delay, mScore);
		 * either here or in your power function to manually destroy the piece
		 * ( that is if you returned false; )
		 * 
		*/ 
	}

	// allows you to specify a power merge...
	public virtual bool powerMerge(List<JSFBoard> chain, JSFGamePiece target,
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


