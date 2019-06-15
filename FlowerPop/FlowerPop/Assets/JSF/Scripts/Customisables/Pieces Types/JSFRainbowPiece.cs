using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSFRainbowPiece : JSFPieceDefinition {
	
	// only used these functions...
	// view more ovveridable functions in PieceDefinition.cs script itself OR
	// check out aPieceTemplate.cs script

	public override bool addToSwipeChain (JSFGamePiece gp, int swipeColor, bool isBoardCheck)
	{
		return true; // can be added to any chain
	}

	public override void onSwipeAdded (JSFGamePiece gp, bool isBoardCheck)
	{
		JSFSwipeManager.allowAnyColorOnce(); // this is the rainbow piece after all!
	}

	public override JSFPieceDefinition chanceToSpawnThis (int x, int y)
	{
		if(Random.Range(0,40) == 0){ // 1/40 chance to spawn...
			return this;
		}
		return null;
	}

	public override bool performPower (int[] arrayRef)
	{
		// no special power
		// return calls...
		return false; // default call - will destroy the piece immediately after this ( AKA after calling the power )
		//		return true; // Only if you do not wish the custom piece to be destroyed instantly
		/*
		 * IMPORTANT : 
		 * you must call gm.destroyInTimeMarked(arrayRef, delay, mScore);
		 * either here or in your power function to manually destroy the piece
		 * ( that is if you returned false; )
		 * 
		*/ 
	}

	// function called when user starts the first swipe touch (the first piece)
	public override bool useAsFirstSwipe(JSFGamePiece gp, bool isBoardCheck) {
		// isBoardCheck == true if the board is checking for legal swipes (use it in if-else when needed!)
		// return true to allow SwipeManager to use this piece as a starting swipe
		// return false to NOT allow SwipeManager to use this piece as a starting swipe

		JSFSwipeManager.allowAnyColorOnce();
		return true;
	}

}
