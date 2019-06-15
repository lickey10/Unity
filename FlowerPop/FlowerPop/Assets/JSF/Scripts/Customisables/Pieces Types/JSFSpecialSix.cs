using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSFSpecialSix : JSFPieceDefinition {
	
	// only used these functions...
	// view more ovveridable functions in PieceDefinition.cs script itself OR
	// check out aPieceTemplate.cs script

	public override bool addToSwipeChain (JSFGamePiece gp, int swipeColor, bool isBoardCheck)
	{
		return true; // can be added to any chain
	}

	public override bool createPowerAtSwipeEnd (JSFGamePiece gp, int swipeLength)
	{
		if(swipeLength >= gm.minSwipeMatch + 6){ // meet min swipe + 6 length
			gp.master.convertToSpecial(this,0); // convert to this power using this skin! :)
			gp.master.panelHit(); // hits the panel as well...
			return true;
		}
		return false;
	}

	public override bool performPower (int[] arrayRef)
	{
		// perform the colored power with a random color and no visuals
		doSpecial6Power(arrayRef,null,gm.iBoard(arrayRef).piece);

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
	
	// power merge ability code
	void doSpecial6Power(int[] arrayRef, JSFGamePiece target, JSFGamePiece refPiece){
		gm.audioScript.bombSoundFx.play(); // play this sound fx
		
		float delayPerPiece = 0.05f;
		gm.animScript.doAnim(JSFanimType.Special6,arrayRef); // visual fx animation

		// destroy the special 6 piece to avoid re-occurence loop
		gm.destroyInTimeMarked(refPiece.master, 0f, scorePerPiece);
		
		for(int x = 0; x < gm.boardWidth;x++){
			for(int y = 0; y < gm.boardHeight;y++)
			{
				// code below fans out the destruction with the bomb being the epicentre
				if( (arrayRef[0]-x) >= 0 && (arrayRef[1]-y) >=0 ){
					gm.destroyInTime(arrayRef[0]-x,arrayRef[1]-y, delayPerPiece*(x+y), scorePerPiece);
				}
				if( (arrayRef[0]+x) < gm.boardWidth && (arrayRef[1]+y) < gm.boardHeight ){
					gm.destroyInTime(arrayRef[0]+x,arrayRef[1]+y, delayPerPiece*(x+y), scorePerPiece);
				}
				if( (arrayRef[0]-x) >= 0 && (arrayRef[1]+y) < gm.boardHeight ){
					gm.destroyInTime(arrayRef[0]-x,arrayRef[1]+y, delayPerPiece*(x+y), scorePerPiece);
				}
				if( (arrayRef[0]+x) < gm.boardWidth && (arrayRef[1]-y) >=0 ){
					gm.destroyInTime(arrayRef[0]+x,arrayRef[1]-y, delayPerPiece*(x+y), scorePerPiece);
				}
			}
		}
	}
}
