using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSFSpecialFive : JSFPieceDefinition {
	
	// only used these functions...
	// view more ovveridable functions in PieceDefinition.cs script itself OR
	// check out aPieceTemplate.cs script

	public override bool powerMerge (List<JSFBoard> chain, JSFGamePiece target,
	                                 JSFGamePiece refPiece, int[] arrayRef, bool isACheck)
	{
		if(target.pd is JSFNormalPiece && (JSFSwipeManager.lastSwipeChainBoard == target.master ||
		   JSFSwipeManager.lastSwipeChainBoard == refPiece.master) ){ // ensure we are only looking at the last swiped
			JSFSwipeManager.swipeColor = target.slotNum;
			if(!isACheck) StartCoroutine( specialFiveColored(arrayRef, target, refPiece, target.slotNum,2f,true) ); // do a power merge power
			return true; // <--- has power merge
		}
		if(target.pd is JSFVerticalPiece || target.pd is JSFHorizontalPiece){
			if(!isACheck) StartCoroutine( doPowerMergeFiveVH(arrayRef, target, refPiece, 1.5f, target.slotNum) ); // do a power merge power
			JSFSwipeManager.stopFurtherSwipes(); // do not allow further swipes
			return true; // <--- has power merge
		}
		if(target.pd is JSFBombPiece){
			if(!isACheck) StartCoroutine( doPowerMergeFiveX(arrayRef, target, refPiece, target.slotNum) ); // do a power merge power
			JSFSwipeManager.stopFurtherSwipes(); // do not allow further swipes
			return true; // <--- has power merge
		}
		if(target.pd is JSFSpecialFive){
			if(!isACheck) doPowerMergeFiveFive(arrayRef, target, refPiece); // do a power merge power
			JSFSwipeManager.stopFurtherSwipes(); // do not allow further swipes
			return true; // <--- has power merge
		}
		return false; // <--- no power merge
	}

	public override bool createPowerAtSwipeEnd (JSFGamePiece gp, int swipeLength)
	{
		if(swipeLength >= gm.minSwipeMatch + 4){ // meet min swipe + 4 length
			gp.master.convertToSpecial(this,0); // convert to this power using this skin! :)
			gp.master.panelHit(); // hits the panel as well...
			return true;
		}
		return false;
	}

	public override bool performPower (int[] arrayRef)
	{
		// perform the colored power with a random color and no visuals
		StartCoroutine( specialFiveColored(arrayRef,null,gm.iBoard(arrayRef).piece,Random.Range(0,gm.NumOfActiveType),2,true) );

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
		
		// below is the default behaviour
		// if it's not a special... set the swipeColor to the slotNum; use as first swipe
		JSFSwipeManager.allowAnyColorOnce(); // rainbow swipe the next piece..
		return true;
	}

	/// <summary>
	/// Does the power merge colored. match 5 type power ( destroys specified param color )
	/// </summary>
	/// <param name="arrayRef">Array reference.</param>
	/// <param name="slotNum">Slot number.(aka piece color)</param>
	/// <param name="delay">Delay for the specialFive trigger</param>
	/// <param name="visuals">If set to <c>true</c> show visuals.</param>
	IEnumerator specialFiveColored(int[] arrayRef, JSFGamePiece target,
	                               JSFGamePiece refPiece, int slotNum, float delay, bool visuals){
		if(visuals){ // if play visual and sound effect
			gm.audioScript.matchFiveSoundFx.play(); // play this sound fx
			gm.animScript.doAnim(JSFanimType.Special5,refPiece.master.arrayRef); // visual fx animation
		}
		gm.destroyInTimeMarked(refPiece.master.arrayRef, delay, scorePerPiece); // locks this piece & destroys after x seconds
		float delayPerPiece = 0.01f;
		yield return new WaitForSeconds(delay);
		
		foreach(JSFBoard board in gm.board){ // destroys the selected color in each board
			if(board.isFilled && board.pd is JSFNormalPiece && board.piece.slotNum == slotNum){
				gm.destroyInTime(board, delayPerPiece, scorePerPiece);
			}
		}
	}

	// power merge ability code
	IEnumerator doPowerMergeFiveVH(int[] arrayRef, JSFGamePiece target, JSFGamePiece refPiece, float delay, int slotNum){
		gm.audioScript.specialMatchSoundFx.play(); // play this sound fx

		List<JSFGamePiece> toBeDestroyed = new List<JSFGamePiece>(); // list of pieces to be destroyed
		gm.destroyInTimeMarked(refPiece.master, delay, scorePerPiece); // locks this piece & destroys after x seconds
		gm.destroyInTimeMarked(target.master, delay,scorePerPiece); // locks this piece & destroys after x seconds

		foreach(JSFBoard board in gm.board){
			if(board.isFilled && board.pd is JSFNormalPiece && board.piece.slotNum == slotNum){
				if(Random.Range(0,2) == 0){// convert the piece to this type (either vertical or horizontal)
					board.piece.specialMe(gm.pieceManager.GetComponent<JSFHorizontalPiece>());
				} else {
					board.piece.specialMe(gm.pieceManager.GetComponent<JSFVerticalPiece>());
				}
				gm.animScript.doAnim(JSFanimType.CONVERTSPEC,board.arrayRef);
				toBeDestroyed.Add(board.piece);
			}
		}

		yield return new WaitForSeconds(delay);
		int index; // variable to be used

		while(toBeDestroyed.Count > 0){ // if there are still converted boards in the list...
			index = Random.Range(0,toBeDestroyed.Count); // randomly pick a board from the list
			if(toBeDestroyed[index] != null && (toBeDestroyed[index].pd is JSFHorizontalPiece ||
			   toBeDestroyed[index].pd is JSFVerticalPiece) ){ // if it's the correct piece...
				toBeDestroyed[index].master.destroyBox(); // destroys the piece that was previously converted
				yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
			}
			toBeDestroyed.RemoveAt(index); // remove from the list after processed...
		}
	}

	// power merge ability code
	IEnumerator doPowerMergeFiveX(int[] arrayRef, JSFGamePiece target, JSFGamePiece refPiece, int slotNum){
		gm.audioScript.bombSoundFx.play(); // play this sound fx

		gm.destroyInTimeMarked(refPiece.master,4f,scorePerPiece); // destroys only after the delay

		// visual effect for a time bomb
		Vector3 newSize = Vector3.Scale(refPiece.thisPiece.transform.localScale,new Vector3(1.45f,1.45f,1f));
		LeanTween.scale( refPiece.thisPiece, newSize ,0.5f).setLoopPingPong();
		refPiece.thisPiece.GetComponent<JSFPieceTracker>().enabled = false;

		StartCoroutine( specialFiveColored(refPiece.master.arrayRef,target,refPiece,slotNum,0f,false) ); // color specific rainbow bust
		gm.pieceManager.GetComponent<JSFBombPiece>().doBombPower(target.master.arrayRef,2); // do the T match (big ver.) power!
		yield return new WaitForSeconds(2f); // wait for 2 secs
		int color = Random.Range(0,gm.NumOfActiveType); // choose a random color..
		while(color == slotNum){
			color = Random.Range(0,gm.NumOfActiveType); // make sure it's not the same color as previous
		}
		StartCoroutine( specialFiveColored(refPiece.master.arrayRef,target,refPiece,
		                                   color,2f,true) ); // blows up another color...
	}


	// power merge ability code
	void doPowerMergeFiveFive(int[] arrayRef, JSFGamePiece target, JSFGamePiece refPiece){
		gm.audioScript.bombSoundFx.play(); // play this sound fx
		
		float delayPerPiece = 0.05f;
		gm.animScript.doAnim(JSFanimType.Special6,arrayRef); // visual fx animation
		
		// destroy the special 5 piece to avoid re-occurence loop
		gm.destroyInTimeMarked(target.master, 0f, scorePerPiece);
		// destroy the special 5 piece to avoid re-occurence loop
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
