using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSFHorizontalPiece : JSFPieceDefinition {
	
	// only used these functions...
	// view more ovveridable functions in PieceDefinition.cs script itself OR
	// check out aPieceTemplate.cs script

	public override bool powerMerge (List<JSFBoard> chain, JSFGamePiece target,
	                                 JSFGamePiece refPiece, int[] arrayRef, bool isACheck)
	{
		if(target.pd is JSFVerticalPiece || target.pd is JSFHorizontalPiece){
			if(!isACheck) doPowerMergeHV(arrayRef); // do a power merge power
			return true; // <--- has power merge
		}
		return false; // <--- no power merge
	}

	public override bool createPowerAtSwipeEnd (JSFGamePiece gp, int swipeLength)
	{
		if(swipeLength >= gm.minSwipeMatch + 2){ // meet min swipe + 2 length
			gp.master.convertToSpecial(this); // convert to this power :)
			gp.master.panelHit(); // hits the panel as well...
			return true;
		}
		return false;
	}

	public override bool performPower (int[] arrayRef)
	{
		doHorizontalPower(arrayRef);
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

	void doHorizontalPower(int[] arrayRef){
		float delay = 0f;
		float delayIncreament = 0.1f; // the delay of each piece being destroyed.

		switch(gm.boardType){ // check board type
		case JSFBoardType.Square: // square type horizontal power
			gm.animScript.doAnim(JSFanimType.ARROWH,arrayRef[0],arrayRef[1]); // perform anim
			gm.audioScript.arrowSoundFx.play(); // play arrow sound fx
			
			// the left of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.Left) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			delay = 0f; // reset the delay
			// the right of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.Right) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			break;
		case JSFBoardType.Hexagon:// hex type horizontal power
			gm.animScript.doAnim(JSFanimType.ARROWTLBR,arrayRef[0],arrayRef[1]); // perform anim
			gm.animScript.doAnim(JSFanimType.ARROWTRBL,arrayRef[0],arrayRef[1]); // perform anim
			gm.audioScript.arrowSoundFx.play(); // play arrow sound fx
			
			// the TopLeft of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.TopLeft) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			delay = 0f; // reset the delay
			// the TopRight of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.TopRight) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			delay = 0f; // reset the delay
			// the BottomLeft of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.BottomLeft) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			delay = 0f; // reset the delay
			// the BottomRight of this board...
			foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.BottomRight) ){
				gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
				delay += delayIncreament;
			}
			break;
		}


	}

	// power merge ability code
	void doPowerMergeHV(int[] arrayRef){
		float delay = 0f; // the delay variable we are using...
		float delayIncreament = 0.1f; // the delay of each piece being destroyed.
		gm.audioScript.arrowSoundFx.play(); // play arrow sound fx

		doHorizontalPower(arrayRef); // perform basic power...
		
		// then perform more power on top of basic power
		// the top of this board...
		foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.Top) ){
			gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
			delay += delayIncreament;
		}
		delay = 0f; // reset the delay
		// the bottom of this board...
		foreach(JSFBoard _board in gm.iBoard(arrayRef).getAllBoardInDirection(JSFBoardDirection.Bottom) ){
			gm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
			delay += delayIncreament;
		}
	}
}
