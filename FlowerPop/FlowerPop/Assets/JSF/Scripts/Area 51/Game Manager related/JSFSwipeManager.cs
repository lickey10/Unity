using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public static class JSFSwipeManager {

	public static bool rainbowSwipe = false; // for any color swipe
	public static bool limitedSwipe = false; // to limit further swipes from happening
	public static bool isSwiping = false; // is currently swiping?
	static bool hasPowerMerge = false; // has a powerMerge within the swipe
	static List<JSFBoard> swipeChain = new List<JSFBoard>(); // the current swipe chain
	static List<GameObject> swipeIndicatorChain = new List<GameObject>(); // the indicators in a list
	static List<GameObject> swipeLineChain = new List<GameObject>(); // the indicator-line in a list
	public static int swipeColor; // the current swipe color (AKA slotNum)
	static JSFGameManager gm {get{return JSFUtils.gm;}} // easy access variable
	static JSFVisualManager vm {get{return JSFUtils.vm;}} // easy access variable

	// easy access variables
	public static JSFBoard lastSwipeChainBoard { get{
			if(swipeChain.Count-1 >= 0){
				return swipeChain[swipeChain.Count-1];
			} return null;
		}}
	// easy access variables
	public static JSFBoard secondLastSwipeChainBoard { get{
			if(swipeChain.Count-2 >= 0){
				return swipeChain[swipeChain.Count-2];
			} return null;
		}}
	public static int currentSwipeCount{get{return swipeChain.Count;}}


	#region public functions

	// function to get the GamePiece reference of previous added swipe chain using index and num of index previous
	public static JSFGamePiece getPrevChainedFromHere(JSFBoard refIndex, int num){
		int newIndex = swipeChain.IndexOf(refIndex) - num;
		if(newIndex >= 0 && newIndex < swipeChain.Count){ // if index not out of bounds...
			return swipeChain[newIndex].piece; // return the index..
		}
		return null; // out of bounds, return null
	}

	// function to void (cancels) the current swipe if the current swiped chain is modified...
	public static void voidSwipeIfNeeded(int x,int y ){
		if(isSwiping){
			foreach(JSFBoard check in swipeChain){
				if(check.arrayRef[0] == x && check.arrayRef[1] == y){ // if the swipe current has this board selected..
					voidCurrentSwipe(); // resets the swipe...
					break;
				}
			}
		}
	}

	// function to void (cancels) the current swipe no matter what...
	public static void voidCurrentSwipe(){
		// resets the swipe...
		swipeChain.Clear();
		removeSwipeVisuals(0);
		limitedSwipe = false; // reset swipe limit
		hasPowerMerge = false; // reset powerMerge status
		rainbowSwipe = false; // resets the any swipe status
		isSwiping = false; // disable swiping after validated
	}

	// function to allow any swipe color once
	public static void allowAnyColorOnce(){
		rainbowSwipe = true; // allow it ( will only work once and then reset during the next swipe )
	}

	// function to stop further swipes
	public static void stopFurtherSwipes(){
		limitedSwipe = true; // stops further swipes to add to chain.. (max chain reach for this swipe)
	}
	// function to allow swipes again
	public static void allowFurtherSwipes(){
		limitedSwipe = false; // allow swipes again..
	}

	public static void swipeStart(JSFBoard board){
		if(isSwiping){
			validateSwipe(); // prevent refresh bug hack
		}

		if(!isLegalSwipe(board)){
			return; // not a legal swipe... do not proceed with swiping
		}
		
		if( board.pd.useAsFirstSwipe(board.piece,false) ){ // check if pieceDefinition allows to swipe start
			isSwiping = true;
			swipeAdd(board);
		}
	}

	public static void swipeCall(JSFBoard board){
		// check backtracking?
		if(swipeChain.Contains(board) ){ // check if we are backtracking the current selection
			hasPowerMerge = false; // resets any powerMerge status
			rainbowSwipe = false; // resets the any swipe status
			limitedSwipe = false; // resets the limit of swipe
			backTrackSelection(board); // default proceeds with back tracking...
			return; // do not continue further... backtracked
		}

		// check legality of swipe
		if(!isSwiping || limitedSwipe || !isLegalSwipe(board) ){
			return; // not legal... do not continue
		}
		// check distance requirements
		int distance = gm.boardRadiusDistance( board, lastSwipeChainBoard);
		if( distance < board.pd.minSwipeDistance(board.piece) || distance > board.pd.maxSwipeDistance(board.piece) ){
			return; // not within distance... do not proceed with swipe...
		}

		// check for powerMerge...
		foreach(JSFBoard checkBoard in swipeChain ){
			if(checkBoard.pd.powerMerge(swipeChain,board.piece,checkBoard.piece,board.arrayRef,true) ||
			   board.pd.powerMerge(swipeChain,checkBoard.piece,board.piece,board.arrayRef,true)){ // powerMerge?
				rainbowSwipe = false; // resets the any swipe status
				swipeAdd(board); // add to chain
				hasPowerMerge = true;
				return; // added as powerMerge...
			}
		}

		// does the pieceDefinition allow to add to swipe chain?
		if(board.pd.addToSwipeChain(board.piece,swipeColor,false) || // normal add to swipe OR
		   		(rainbowSwipe && !board.pd.isSpecial) ) { // check for rainbow OR
			if(rainbowSwipe && !board.pd.isSpecial) swipeColor = board.piece.slotNum; // change to this color if it was from rainbowSwipe
			rainbowSwipe = false; // resets the any swipe status
			swipeAdd(board); // add to chain
		}

	}

	// function to validate the swipe chain
	public static void validateSwipe(){
		bool validateStatus = false;
		int swipeNum = currentSwipeCount;
		int comboTracker = 0;
		if( gm.gameState == JSFGameState.GameActive && // game has not ended...
			(swipeChain.Count >= gm.minSwipeMatch || hasPowerMerge)){ // meet the minimum required length
			gm.audioScript.matchSoundFx.play();
			gm.playerMadeAMove();
			if(hasPowerMerge){ // perform powerMerges if there is...
				List<JSFBoard> mergeCheck = new List<JSFBoard>(); // a power merge check list
				bool powerMerged = false;
				for(int w = swipeChain.Count-1; w >= 0; w-- ){
					mergeCheck.Clear(); // clear the list for each check
					mergeCheck.AddRange(swipeChain); // copy the current list
					mergeCheck.Remove(swipeChain[w]); // as to not check itself
					mergeCheck.Reverse(); // reverse the list to start from last swiped
					foreach(JSFBoard innerLoop in mergeCheck){ // check against all swiped pieceDefinitions
						if(innerLoop.pd.powerMerge(swipeChain,swipeChain[w].piece,innerLoop.piece,swipeChain[w].arrayRef,false) ||
						   swipeChain[w].pd.powerMerge(swipeChain,innerLoop.piece,swipeChain[w].piece,swipeChain[w].arrayRef,false) ){
							powerMerged = true; // powerMerge called...
							break; // done power merging... ( break inner loop )
						}
					}
					if(powerMerged){
						break; // done power merging... ( break outer loop too )
					}
				}
			}

			foreach(JSFBoard board in swipeChain){
				comboTracker++;
				if(comboTracker > gm.minSwipeMatch){
					gm.increaseCombo(); // increases combo by 1 for each after the min swipe num
				}
				if(board.isFilled){ // if not already destroyed by power merge
					JSFRelay.onSwipeValidating(board.piece); // relay call
				}
			}
			validateStatus = true; // swipe chain has been validated...
		} else {
			swipeChain.Reverse(); // change order from back to front...
			foreach(JSFBoard board in swipeChain){
				JSFRelay.onSwipeFailed(board.piece); // calls the individual swipeFailed function
			}
			if(swipeNum > 1){
				gm.audioScript.badMoveSoundFx.play();
			}
		}
		swipeChain.Clear();
		removeSwipeVisuals(0);
		limitedSwipe = false; // reset swipe limit
		hasPowerMerge = false; // reset powerMerge status
		rainbowSwipe = false; // resets the any swipe status
		isSwiping = false; // disable swiping after validated
		JSFRelay.onSwipeValidated(validateStatus,swipeNum); // relay call
	}
	#endregion public functions

	#region private functions

	// function to check for swipe legality (can users swipe?)
	static bool isLegalSwipe(JSFBoard board){
		if(gm.moveOnlyAfterSettle){ // move only after settle?
			if(!gm.checkedPossibleMove){
				return false; // not ready to move
			}
		}
		return (gm.canMove && gm.gameState == JSFGameState.GameActive && gm.isLegalSwipe(board) ); // meets criteria?
	}

	// function to backtrack the swipe chain
	static void backTrackSelection(JSFBoard board){
		// ==== BACK TRACK SWIPE CODE ====
		if(swipeChain.Contains(board)){
			int index = swipeChain.IndexOf(board);
			for(int x = 0; x <= swipeChain.Count-1-index; x++){
				JSFRelay.onSwipeRemoved(swipeChain[swipeChain.Count-1-x].piece,false);
			}
			swipeChain.RemoveRange( index, swipeChain.Count - index );
			removeSwipeVisuals(index);
			JSFRelay.onSwipeBackTracked(board.piece,false); // relay call
			if(swipeChain.Count == 0){
				isSwiping = false;
				swipeStart(board); // add back this board with its call criterias
			} else {
				rainbowSwipe = true; // in case we have changed color before this... revert to this color
				swipeCall(board); // add back this board with its call criterias
			}


		}
	}
	
	// function to add to the swipe chain
	static void swipeAdd(JSFBoard board){
		isSwiping = true; // swiping is active
		// ==== SWIPE CHAIN CODE ====
		swipeChain.Add(board);
		addVisualChain(board.arrayRef[0],board.arrayRef[1]);
		
		JSFRelay.onSwipeAdded(board.piece,false); // relay call
	}

	#endregion private functions

	#region swipeGUI
	// ##########################
	// SWIPE GUI SECTION
	// ##########################

	static void addVisualChain(int x, int y){
		if(gm.showSwipedIndicator) addSwipeIndicator(x,y);
		if(gm.showSwipeLine) addSwipeLine(x,y);
	}

	static void addSwipeIndicator(int x, int y){
		if(vm.swipeIndicatorObj == null){
			Debug.LogError("Warning : No swipe indicator object. Check JSFVisualManager.");
			return; // no object defined
		}
		GameObject obj;
		if(gm.usingPoolManager){ // POOLMANAGER CODE
			obj = PoolManager.Pools[JSFUtils.miscPoolName].Spawn(vm.swipeIndicatorObj.transform).gameObject;
		} else { // NON-POOLMANAGER CODE
			obj = GameObject.Instantiate(vm.swipeIndicatorObj) as GameObject;
		}

		obj.transform.position = gm.board[x,y].position + new Vector3(0,0,-20);
		JSFUtils.autoScalePadded(obj);
		swipeIndicatorChain.Add(obj);
	}

	static void addSwipeLine(int x, int y){
		if(vm.swipeLineObj == null){
			Debug.LogError("Warning : No swipe line object. Check JSFVisualManager.");
			return; // no object defined
		}
		if(swipeChain.Count < 2){
			return; // not enough to create a line between two objects yet
		}
		GameObject obj;
		if(gm.usingPoolManager){ // POOLMANAGER CODE
			obj = PoolManager.Pools[JSFUtils.miscPoolName].Spawn(vm.swipeLineObj.transform).gameObject;
		} else { // NON-POOLMANAGER CODE
			obj = GameObject.Instantiate(vm.swipeLineObj) as GameObject;
		}
		JSFBoard board = swipeChain[swipeChain.Count-2]; // get the previous swipe entry
		JSFUtils.creatSwipeLine(obj, gm.getBoardPosition(x,y), board.position, -19.99f );
		swipeLineChain.Add(obj);
	}

	static void removeSwipeVisuals(int index){
		if(gm.showSwipedIndicator) removeSwipeIndicator(index);
		if(gm.showSwipeLine) removeSwipeLine(index);

		if(index == 0){
			swipeIndicatorChain.Clear();
			swipeLineChain.Clear();
		}
	}

	static void removeSwipeIndicator(int index){
		// remove swipe indicators
		foreach(GameObject go in swipeIndicatorChain.GetRange(index, swipeIndicatorChain.Count - index )){
			if(gm.usingPoolManager){
				PoolManager.Pools[JSFUtils.miscPoolName].Despawn(go.transform); // POOLMANAGER CODE
			} else {
				GameObject.Destroy(go); // NON-POOLMANAGER CODE
			}
		}
		swipeIndicatorChain.RemoveRange( index, swipeIndicatorChain.Count - index );
	}

	static void removeSwipeLine(int index){
		index = Mathf.Max(index-1,0); // to compensate for swipe line less 1 array
		// remove swipe lines
		foreach(GameObject go in swipeLineChain.GetRange(index, swipeLineChain.Count - index )){
			if(gm.usingPoolManager){
				PoolManager.Pools[JSFUtils.miscPoolName].Despawn(go.transform); // POOLMANAGER CODE
			} else {
				GameObject.Destroy(go); // NON-POOLMANAGER CODE
			}
		}
		swipeLineChain.RemoveRange( index, swipeLineChain.Count - index );
	}
	#endregion swipeGUI
}
