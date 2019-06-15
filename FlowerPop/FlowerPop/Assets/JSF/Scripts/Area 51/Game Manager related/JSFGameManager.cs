using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is the Mother of all script~!
/// Everything that happens during the game will be controlled in this script.
/// (with public references from support scripts too ofcourse.)
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################



// ---
// global access board checking enums
// ---

public enum JSFGravity{ UP, DOWN, LEFT, RIGHT};
public enum JSFBoardType{ Square, Hexagon};
public enum JSFsquareMode{ CrossType, Box9x9Type};
public enum JSFNewPieceMethod{ Appear, FallByGravity};
public enum JSFGameState{GamePending,GameActive,GameFinalizing,GameOver};

[RequireComponent(typeof(JSFDefaultAnimations), typeof(JSFBoardLayout), typeof(JSFWinningConditions))]
[RequireComponent(typeof(JSFVisualManager), typeof(JSFAudioPlayer), typeof(JSFVisualizedGrid) )]
public class JSFGameManager : MonoBehaviour {
	
	// ===========================
	// GLOBAL VARIABLES
	// ===========================

	[Tooltip("The type of board you want to use for this game.")]
	public JSFBoardType boardType = JSFBoardType.Square;
	[Tooltip("The Square swipe mode, cross-type limits the diagonal neighbours of a board box.")]
	public JSFsquareMode squareSwipeMode = JSFsquareMode.Box9x9Type;
	[Tooltip("The Square splash mode, cross-type limits the diagonal neighbours of a board box.")]
	public JSFsquareMode squareSplashMode = JSFsquareMode.Box9x9Type;
	[Tooltip("If you have the original 'PoolManager' asset, and want to use the integrated pooling feature.")]
	public bool usingPoolManager = false;

	[Tooltip("The current board's Width in boxes")]
	[Range(1,20)] public int boardWidth=4;
	[Tooltip("The current board's Height in boxes")]
	[Range(1,20)] public int boardHeight=4;
	[Tooltip("The current board's display size (visible in the Scene View if 'Show Grid' is enabled)")]
	public float size = 4; // the size we want the board to be
	[Tooltip("The amount of spacing between each board box. (Does not affect board size)")]
	[Range(0.0f,100.0f)] public float spacingPercentage = 0f; // the percentage of spacing user wants
	[Tooltip("The amount of padding you want for the pieces inside the board box")]
	[Range(0.0f,100.0f)] public float paddingPercentage = 20f; // the percentage of padding user wants
	[HideInInspector] public float boxPadding = 0; // the padding in each box **updated during "Awake()"
	[Tooltip("Visualize Grid : shows Corners of the board in the scene view.")]
	public bool showCorners = false;
	[Tooltip("Visualize Grid : shows the Grids of the board in the scene view.")]
    public bool showGrid = false;
	[Tooltip("Visualize Grid : shows the padded Tiles for the pieces of the board in the scene view.")]
	public bool showPaddedTile = false;
	[Tooltip("Visualize Grid : shows the extra information relating to the board grid in the scene view")]
	public bool showToolTips = false;
	[Tooltip("The number of active colors in the game. (For non-special pieces)")]
	[Range(1,9)] public int NumOfActiveType = 3; // remember not to exceed the normalPieces array~!
	[Tooltip("if Enabled, Players can only swipe when the board has settled during the last move.")]
	public bool moveOnlyAfterSettle = false; // must the player wait for board to settle before next move?
	//	public bool movingResetsCombo = true; // TODO moving rest combo currently not used in JSF

	// control timer
	[Tooltip("The update speed for the Game Engine (the routine checks of Gravity & possible moves)")]
	public float gameUpdateSpeed = 0.2f;
	[Tooltip("The amount of delay before the board initiates a board reset when no more moves are available.")]
	public float noMoreMoveResetTime = 2f;
	[Tooltip("The amount of delay before the board shows the player a legal suggested move.")]
	public float suggestionTimer = 5f;

	// swipe stuff
	[Tooltip("The current minimum swipe length the player must meet for the swipe to be accepted.")]
	[Range(1,10)] public int minSwipeMatch = 3;// swipe chain length
	[Tooltip("Shows the swipe indicator when swiping over selected pieces. " +
		"(refer to the VisualManager script to set the Swipe Indicator object)")]
	public bool showSwipedIndicator = true;
	[Tooltip("Shows the swipe line between two pieces when swiping over selected pieces. " +
	         "(refer to the VisualManager script to set the Swipe Line object)")]
	public bool showSwipeLine = true;

	[Tooltip("How the new pieces will be created after being destroyed.")]
	public JSFNewPieceMethod newPieceMode = JSFNewPieceMethod.Appear;
	// appear type...
	[Tooltip("The delay before the new pieces start appearing in 'Appear Mode'")]
	public float appearModeDelay = 0.6f;
	[Tooltip("How fast the new piece will appear in 'Appear Mode' after the delay.")]
	public float appearModeSpeed = 0.8f;

	// gravity type...
	[Tooltip("How fast the pieces will drop to the next board box.")]
	public float gravityDropSpeed = 0.25f;
	[Tooltip("Give an extra effect when pieces reach the bottom of the box (hardcoded effect)")]
	public bool pieceDropExtraEffect = true;
	[Tooltip("Makes the pieces drop faster the longer the distance to the bottom.")]
	public bool acceleratedVelocity = true; // drop pieces fall faster if it need to cover more distance
	[Tooltip("if Enabled, the pieces will be delayed (by the specified amount) before any gravity call")]
	public bool delayedGravity = true; // delay before a piece drops when there's an empty space
	[Tooltip("The amount of delay before gravity takes affect for each individual piece.")]
	public float gravityDelayTime = 0.3f; // the delay in float seconds
	[Tooltip("The current gravity direction for the board.")]
	public JSFGravity currentGravity = JSFGravity.DOWN; // initial gravity of the game
	// control variables for gravity
	Vector3 gravityVector = new Vector3(); // gravity in vector3
	JSFBoardDirection[] bd = null; // the direction array for landslide
	
	// pieces & panels prefabs
	[Tooltip("The reference for the PieceManager Object.")]
	public GameObject pieceManager;
	[Tooltip("The reference for the PanelManager Object.")]
	public GameObject panelManager;
	[HideInInspector] public JSFPieceDefinition[] pieceTypes;
	[HideInInspector] public JSFPanelDefinition[] panelTypes;
	
	public JSFBoard[,] board; // the board array
		
	// scoring stuff
	[HideInInspector] public long score = 0;
	[HideInInspector] public int currentCombo = 0;
	[HideInInspector] public int maxCombo = 0;
	[HideInInspector] public JSFComboPopUp comboScript;
	[HideInInspector] public int moves = 0;
	[HideInInspector] public int[] matchCount = new int[9];
	
	// suggestion variables
	[HideInInspector] public bool checkedPossibleMove = false;
	[HideInInspector] public bool isCheckingPossibleMoves = false;
	bool hasPowerMerge = false;
	Vector3 pieceOriginalSize;
	List<GameObject> suggestedPieces = new List<GameObject>();
	//TODO canMove variable currently not being used... but it has been integrated into the engine
	[HideInInspector] public bool canMove = true; // switch to determine if player can make the next move
	
	// other helper scripts
	[HideInInspector] public JSFAudioPlayer audioScript;
	[HideInInspector] public JSFDefaultAnimations animScript;

	// environment control variable
	[HideInInspector] public JSFGameState gameState = JSFGameState.GameActive;

	#region Easy Access Functions
	// ================================================
	// Easy Access FUNCTIONS
	// ================================================
	// an easy access function to call the board from an int-array
	public JSFBoard iBoard(int[] arrayRef){
		return board[arrayRef[0],arrayRef[1]];
	}
	
	public Vector3 getBoardPosition(int[] boardPosition){ // OVERLOAD METHOD for int array
		return board[boardPosition[0],boardPosition[1]].position;
	}
	public Vector3 getBoardPosition(int x, int y){ // OVERLOAD METHOD for int x & y
		return board[x,y].position;
	}
	#endregion Easy Access Functions


	// ================================================
	// ENGINE FUNCTIONS
	// ================================================

	#region Misc Functions
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	// Misc. functions
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	
	// start game preparation
	void initializeGame() {

		boxPadding = 1f-(paddingPercentage/100); // set the padding value

		pieceTypes = pieceManager.GetComponents<JSFPieceDefinition>();
		panelTypes = panelManager.GetComponents<JSFPanelDefinition>();


		// support sub-scripts initialization
		audioScript = GetComponent<JSFAudioPlayer>();
		animScript = GetComponent<JSFDefaultAnimations>();
		
		// creates a 2D board
		board = new JSFBoard[boardWidth,boardHeight];
		
		//
		// loop to create the board with blocks
		//

		switch(boardType){
		case JSFBoardType.Square : /// For square type
			// for the board width size
			for( int x = 0; x < boardWidth; x++) {
				// for the board height size
				for( int y = 0; y < boardHeight; y++) {
					// create board centralized to the game object in unity
					Vector3 pos = new Vector3( x - (boardWidth/2.0f) + 0.5f, y -(boardHeight/2.0f) + 0.5f, 0);
					board[x,y] = new JSFBoard(this, new int[2]{x,y}, pos*size ) ;
					//place a cube here to start with...
					board[x,y].createObject(pieceTypes[0], ranType());
				}
			}
			break;

		case JSFBoardType.Hexagon : /// For hexagon type
			// for the board width size
			for( int x = 0; x < boardWidth; x++) {
				// for the board height size
				for( int y = 0; y < boardHeight; y++) {
					Vector3 pos;
					if(x%2 == 0){ // displacement for hexagon type
						// create board centralized to the game object in unity
						pos = new Vector3( (x - (boardWidth/2.0f) + 0.5f)*0.865f, y -(boardHeight/2.0f) + 0.75f, 0);
					} else {
						// create board centralized to the game object in unity
						pos = new Vector3( (x - (boardWidth/2.0f) + 0.5f)*0.865f, y -(boardHeight/2.0f) + 0.25f, 0);
					}
					board[x,y] = new JSFBoard(this, new int[2]{x,y}, pos*size ) ;
					//place a cube here to start with...
					board[x,y].createObject(pieceTypes[0], ranType());
				}
			}
			break;
		}

		foreach(JSFBoard _board in board){
			_board.initNeighbourReferences();
		}
	}

	void preGameSetup(){
		// call the board panels preGameSetup...
		GetComponent<JSFBoardLayout>().setupGamePanels();
		
		// call the board piece preGameSetup...
		GetComponent<JSFBoardLayout>().setupGamePieces();
	}

	// the gravity check as a function call - to keep the updater() neat
	void gravityCheck(){
		for(int x = 0; x < boardWidth ; x++){
			for( int y = 0; y < boardHeight ; y++) {
				dropPieces(x,y);
			}
		}
	}
	
	// primarily for the suggestion functions... 
	// but you can do other stuff when the board change as you like...
	public void notifyBoardHasChanged(){
		checkedPossibleMove = false; // board has changed, will check possible moves again
		foreach(JSFBoard _board in board){
			_board.isActive = true; // make the boards active for checks again
		}

		StopCoroutine("suggestPiece"); // if it was still pending, stop the routine from executing...
	}
	
	// increase the combo counter & display to GUI(dont worry, combo is reset elsewhere)
	public void increaseCombo() {
		// increase combo count!
		currentCombo += 1;

		JSFRelay.onCombo();

        JSFUtils.vm.DisplayComboPopUp();

		// relay to the combo script
		if(comboScript != null){ // show combo to GUI (relay to GUI script)
			comboScript.executeCombo(currentCombo);
		}
        
        if (maxCombo < currentCombo){
			maxCombo = currentCombo; // just to keep track of the max combo
		}
	}

	// OVERLOAD FUNCTION for increaseScore
	public void increaseScore(int num, int[] arrayRef) {
		increaseScore(num,arrayRef[0],arrayRef[1]);
	}

	// increase the score counter (for external scripts to update)
	public void increaseScore(int num, int x, int y) {
		num = JSFRelay.onScoreIssue(num,x,y); // relay call for modified score
		if(currentCombo > 0){
			num = (int) (num * (1.5+(currentCombo/10.0)) ); // increase with multiplier from combo
		}

		if(JSFUtils.vm.displayScoreHUD && board[x,y].scoreHUD != null){ // display the HUD?
			board[x,y].scoreHUD.display(num);
		}
		score += num; // add to the game score
	}


	// OVERLOAD METHOD for destroyInTime
	public void destroyInTime(int[] arrayRef, float delay, int mScore){
		destroyInTime(iBoard(arrayRef),delay,mScore);
	}

	// OVERLOAD METHOD for destroyInTime
	public void destroyInTime(int x, int y,float delay, int mScore){
		destroyInTime(board[x,y],delay,mScore);
	}

	// OVERLOAD METHOD for destroyInTime
	public void destroyInTime(JSFBoard _board,float delay, int mScore){
		StartCoroutine( destroyInTimeRoutine(_board,delay,mScore) );
	}

	// destroys the box after a given time so that it looks cooler
	IEnumerator destroyInTimeRoutine(JSFBoard _board,float delay, int mScore){
		if( _board.isFilled && _board.piece.markedForDestroy ){ // ignore those marked for destroy
			yield break; // do not continue... it is already marked
		}

		yield return new WaitForSeconds(delay); // wait for it...
		
		if(_board.isFilled && !_board.piece.markedForDestroy && _board.piece.pd.isDestructible){
			increaseScore( mScore, _board.arrayRef ); // add to the score
		}
		
		_board.destroyBox();
		if(!_board.panel.isDestructible()){ // if the panel is NOT a solid type with no piece to destroy...
			_board.panelHit(); // got hit by power attack~!
		}
	}

	// OVERLOAD METHOD for destroyInTimeMarked
	public void destroyInTimeMarked(int[] arrayRef, float delay, int mScore){
		destroyInTimeMarked(arrayRef[0],arrayRef[1],delay,mScore);
	}
	// OVERLOAD METHOD for destroyInTimeMarked
	public void destroyInTimeMarked(JSFBoard _board, float delay, int mScore){
		destroyInTimeMarked(_board.arrayRef[0],_board.arrayRef[1],delay,mScore);
	}
	// OVERLOAD METHOD for destroyInTimeMarked
	public void destroyInTimeMarked(int x, int y, float delay, int mScore){
		StartCoroutine( destroyInTimeMarkedRoutine(x, y, delay, mScore) );
	}

	// destroys the box after a given time so that it looks cooler - object being marked for delayed destruction
	IEnumerator destroyInTimeMarkedRoutine(int x, int y, float delay, int mScore){
		if(!board[x,y].isFilled){
			board[x,y].isFalling = false;
			yield break;
		}

		// save the piece reference
		JSFGamePiece refPiece = board[x,y].piece;

		if(refPiece.markedForDestroy){
			yield break; // do not continue as it is already marked...
		}

		// mark the piece as to be destroyed later
		refPiece.markedForDestroy = true;
		refPiece.thisPiece.GetComponent<JSFPieceTracker>().enabled = false; // no longer movable
		
		yield return new WaitForSeconds(delay); // wait for it...
		
		if(refPiece.master.isFilled){
			increaseScore( mScore, refPiece.master.arrayRef[0], refPiece.master.arrayRef[1] ); // add to the score
		}
		
		refPiece.master.destroyMarked();
		
		if(!refPiece.master.panel.isDestructible()){ // if the panel is a solid type with no piece to destroy...
			refPiece.master.panelHit(); // got hit by power attack~!
		}
	}

	// function call for the ieNumerator version
	public void lockJustCreated (int x, int y, float time){
		StartCoroutine( lockJustCreatedRoutine(x,y,time) );
	}

	// function to lock a piece from being destroyed with a cooldown timer
	public IEnumerator lockJustCreatedRoutine (int x, int y, float time){
		// lock the piece so that it isnt destroyed so fast
		JSFGamePiece refPiece = null;
		if(board[x,y].isFilled){
			refPiece = board[x,y].piece;
			refPiece.justCreated = true;
			refPiece.master.isActive = false;
			yield return new WaitForSeconds(time); // wait for it...
			// un-lock the piece again
			refPiece.justCreated = false;
			refPiece.master.isActive = true;
		}
	}

	#endregion Misc Functions

	#region Routine Checks Related
	// ##################################################
	// Routine checks and it's related functions
	// ##################################################
	
	// status update on given intervals
    IEnumerator updater () {
		while (gameState != JSFGameState.GameOver){  // loop again (infinite) until game over
			if(JSFSwipeManager.isSwiping && !Input.GetMouseButton(0)){
				JSFSwipeManager.validateSwipe(); // validate current swipes (if any)
			}
			gravityCheck(); // for dropping pieces into empty board box
			detectPossibleMoves(); // to make sure the game doesn't get stuck with no more possible moves
			yield return new WaitForSeconds(gameUpdateSpeed); // wait for the given intervals
		}
    }

	
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	// possible moves detector + suggestor  ( DO NOT TOUCH UNLESS NECCESSARY~! )
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	
	// detects any possibles moves, find suggestions or resets the board is necessary
	void detectPossibleMoves() {
		//checks through each board boxes
		if(!checkedPossibleMove && !isCheckingPossibleMoves && !JSFSwipeManager.isSwiping){
			isCheckingPossibleMoves = true;
			for (int x = 0; x < boardWidth ; x++){
				for (int y = 0; y < boardHeight ; y++) {
					if( board[x,y].isBeingDelayed || board[x,y].isFalling ) {
						// "board.isActive" is no longer used atm *to be evaluated of its use*
						isCheckingPossibleMoves = false;
						return; // do not continue, wait for board to clear and stabilize
					}
				}
			}
			checkedPossibleMove = true; // once we checked, no need to check again until needed

			JSFRelay.onBoardStabilize();

			// to hold all the pieces for the suggested move
			List<JSFBoard> suggestedBoards = findSuggestionMove();
			
			if (suggestedBoards.Count == 0) { // no more possible moves
				StartCoroutine( resetBoard() ); // reset board in co-routine mode for delayed event
			}
			else {
				// suggest the found possible move to player
				suggestedPieces.Clear(); // clear the current list
				foreach(JSFBoard _board in suggestedBoards){
					suggestedPieces.Add(_board.piece.thisPiece); // add the new chain to the list
				}
				suggestedBoards.Clear(); // remove stored memory
				pieceOriginalSize = suggestedPieces[0].transform.localScale; // remember the current size
				StartCoroutine("suggestPiece"); // its a string coroutine so that we can use StopCoroutine!
				isCheckingPossibleMoves = false;
			}
		}
	}

	// function to find a valid chain of suggestion boards
	List<JSFBoard> findSuggestionMove(){
		// remember current swipe status before using swipeManager for checks...
		bool limitedSwipe = JSFSwipeManager.limitedSwipe;
		bool rainbowSwipe = JSFSwipeManager.rainbowSwipe;
		int swipeColor = JSFSwipeManager.swipeColor;

		List<JSFBoard> randomised = new List<JSFBoard>(); // to select a random board for suggestion check
		List<JSFBoard> list = new List<JSFBoard>(); // the list of boards in the suggestion found

		foreach(JSFBoard _board in board){
			list.Add(_board); // add to the list of all available boards
		}

		int randomNum = 0;
		while(list.Count > 0){ // transfer all the boards to the randomise list
			randomNum = Random.Range(0,list.Count);
			randomised.Add(list[randomNum]); // add to the random list
			list.RemoveAt(randomNum); // remove from this list
		} // at the end will have a randomised list

		foreach(JSFBoard _board in randomised){
			hasPowerMerge = false; // reset status for powerMerge
			JSFSwipeManager.limitedSwipe = false; // reset status for limitedSwipe
			list.Clear(); // clear it each time before checking
			if(!_board.isFilled || !isLegalSwipe(_board)){
				continue; // cannot proceed... move on to the next board...
			}
			if( _board.pd.useAsFirstSwipe(_board.piece,true) ) { // simulate a swipe start
			
				list.Add(_board);
				JSFSwipeManager.rainbowSwipe = false; // uses up the rainbow swipe each time...
				JSFRelay.onSwipeAdded(_board.piece,true); // simulate a swipe added...

				list = recursiveFindSuggestion(list,_board);

				list.Reverse(); // reverses the list for swipeRemove function
				foreach(JSFBoard bt in list){
					JSFRelay.onSwipeRemoved(bt.piece,true); // simulate a removed piece
				}

				if(list.Count >= minSwipeMatch || hasPowerMerge){
					break; // found a valid suggestion list
				}
			}
			list.Clear(); // clear it each time after checking
		}

		//reset the old status of the swipe...
		JSFSwipeManager.limitedSwipe = limitedSwipe;
		JSFSwipeManager.rainbowSwipe = rainbowSwipe;
		JSFSwipeManager.swipeColor = swipeColor;

		return list; // returns the list (be it empty OR with suggestions)
	}

	// function to recursively add valid boards to the suggestion chain
	List<JSFBoard> recursiveFindSuggestion(List<JSFBoard> list,JSFBoard _board){
		if(list.Count >= minSwipeMatch || hasPowerMerge){
			return list; // already found a suggested list... go back!
		}
		List<JSFBoard> temp = new List<JSFBoard>(); // a temp list being chained...
		List<JSFBoard> candidates = new List<JSFBoard>(); // list of all the candidate boards
		for(int w = _board.pd.minSwipeDistance(_board.piece); 
		    w <= _board.pd.maxSwipeDistance(_board.piece); w++){ // within min/max range
			candidates.AddRange( getBoardsFromDistance(_board,w) ); // adds the boards as candidates
		}

		foreach(JSFBoard neighbour in candidates){
			temp.Clear(); // reset the temp list
			temp.AddRange(list); // shallow copy to a new temp list
			if(!temp.Contains(neighbour)){ // not part of the current simulated swipe chain
				if(!neighbour.isFilled || !isLegalSwipe(neighbour)){
					continue; // no piece here / not legal swipe ... move on to the next board...
				}
				// check for powerMerge...
				foreach(JSFBoard tempBoard in temp){
					if(tempBoard.pd.powerMerge(temp,neighbour.piece,tempBoard.piece,neighbour.arrayRef,true) ||
					   neighbour.pd.powerMerge(temp,tempBoard.piece,neighbour.piece,neighbour.arrayRef,true)){ // powerMerge?
						temp.Add(neighbour);
						JSFSwipeManager.rainbowSwipe = false; // uses up the rainbow swipe each time...
						JSFRelay.onSwipeAdded(neighbour.piece,true); // simulate a swipe added...
						hasPowerMerge = true;
						return temp; // found a valid suggestion list
					} else {
						JSFSwipeManager.limitedSwipe = false; // reset limitedSwipe status
					}
				}

				if(neighbour.pd.addToSwipeChain(neighbour.piece,JSFSwipeManager.swipeColor,true) ){
					temp.Add(neighbour);
					JSFSwipeManager.rainbowSwipe = false; // uses up the rainbow swipe each time...
					JSFRelay.onSwipeAdded(neighbour.piece,true); // simulate a swipe added...
					if(!JSFSwipeManager.limitedSwipe) temp = recursiveFindSuggestion(temp,neighbour);
					if(temp.Count >= minSwipeMatch){
						return temp; // found a valid suggestion list
					} else {
						JSFSwipeManager.limitedSwipe = false; // reset limitedSwipe status
					}
				}
			}
		}
		return temp; // return the list without any new results
	}
	
	
	// resets the board due to no more moves
	IEnumerator resetBoard() {
		animScript.doAnim(JSFanimType.NOMOREMOVES,0,0);
		JSFRelay.onNoMoreMoves();
		yield return new WaitForSeconds(noMoreMoveResetTime);
		notifyBoardHasChanged(); // reset the board status
		// for the board width size
		for( int x = 0; x < boardWidth; x++) {
			// for the board height size
			for( int y = 0; y < boardHeight; y++) {
				//reset the pieces with a random type..
				board[x,y].reset(pieceTypes[0], ranType());
			}
		}
		JSFRelay.onComboEnd();
		JSFRelay.onBoardReset();
		isCheckingPossibleMoves = false;
	}
	
	// suggest a piece after a given time...
	IEnumerator suggestPiece() {
		yield return new WaitForSeconds(suggestionTimer); // wait till it's time
		if(gameState != JSFGameState.GameActive){
			yield break; // game no longer active... do not display suggestion...
		}
		foreach(GameObject go in suggestedPieces){
			if(go == null || !go.activeSelf){
				notifyBoardHasChanged(); // something changed... perform checks again!
				yield break;
			}
			float currentSize = pieceOriginalSize.x;
			// main scaler loop
			LeanTween.value(go,suggestPieceScaler,currentSize*0.75f,currentSize*1.25f,1f)
				.setLoopPingPong().setOnUpdateParam(go);
			// sub rotate loop
			go.transform.localEulerAngles = new Vector3(0,0,340f);
			LeanTween.rotateZ(go,20f,0.8f).setLoopPingPong();
		}
	}

	// the function for leanTween to scale the suggested pieces
	void suggestPieceScaler(float val, object go){
		if(checkedPossibleMove){
			((GameObject)go).transform.localScale = new Vector3(val,val,1); // scale to value
		} else {
			LeanTween.cancel((GameObject)go); // cancel all tweens
			((GameObject)go).transform.localScale = pieceOriginalSize; // resets scale to normal
			((GameObject)go).transform.localEulerAngles = Vector3.zero; // resets rotate to normal
			JSFUtils.autoScalePadded((GameObject)go); // as a precaution to reset size
		}
	}
	
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	// Board Piece position Fall by gravity function ( DO NOT TOUCH UNLESS NECCESSARY~! )
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	
	// (main gravity function)
	public void dropPieces(int x, int y) {
		if( !(x >= 0 && x < boardWidth && y >=0 && y < boardHeight) ){
			return; // index out of bounds... do not continue~!
		}
		if( board[x,y].replacementNeeded()) {
			board[x,y].isBeingDelayed = true; // status to verify that board already active in drop sequence
			switch(newPieceMode){
			case JSFNewPieceMethod.FallByGravity : // gravity replacement
				StartCoroutine( movePieces(x,y) ); // coroutine that can be delayed
				break;
			case JSFNewPieceMethod.Appear : // self replacement (appear from itself)
				StartCoroutine( appearModePieces(x,y) ); // coroutine that can be delayed
				break;
			}
		}
	}

	IEnumerator appearModePieces(int x, int y){
		board[x,y].isFalling = true;

		yield return new WaitForSeconds(appearModeDelay); // wait for the delay..

		// for custom pieces spawn rate
		JSFPieceDefinition spawned;
		for(int w = 0; w < pieceTypes.Length; w++){
			spawned = pieceTypes[w].chanceToSpawnThis(x,y);
			if(spawned != null){
				board[x,y].spawnNewAppear(spawned,appearModeSpeed,spawned.skinToUseDuringSpawn(x,y) );
				break;
			}
			if( w == pieceTypes.Length - 1){
				// reached the end, no custom spawn... spawn the default
				board[x,y].spawnNewAppear(pieceTypes[0],appearModeSpeed,ranType());
			}
		}
		notifyBoardHasChanged(); // board structure changed, so notify the change~!

		yield return new WaitForSeconds(appearModeSpeed); // wait for appear mode speed
		board[x,y].isFalling = false;
		board[x,y].isBeingDelayed = false; // reset status once delay is over
	}
	
	// secondary gravity function as a coroutine for delay ability
	IEnumerator movePieces(int x, int y){
		if(delayedGravity && board[x,y].mustWait){ // if delay is required by GameManager or by board
			yield return new WaitForSeconds(gravityDelayTime); // delay time between each dropped pieces
		}
		board[x,y].mustWait = false; // change status of board to drop other pieces without delay
		board[x,y].isBeingDelayed = false; // reset status once delay is over

		JSFBoard tBoard = null;

		switch(currentGravity){
		case JSFGravity.DOWN :
			gravityVector = new Vector3(0,-size,0); // gravity in vector3
			tBoard = board[x,y].top;
			switch(boardType){ // get the landslide's position to take from...
			case JSFBoardType.Square : default:
				bd = new JSFBoardDirection[]{JSFBoardDirection.Left,
					JSFBoardDirection.Right,JSFBoardDirection.Bottom};
				break;
			case JSFBoardType.Hexagon :
				bd = new JSFBoardDirection[]{JSFBoardDirection.BottomLeft,
					JSFBoardDirection.BottomRight, JSFBoardDirection.Bottom};
				break;
			}
			break;
		case JSFGravity.UP :
			gravityVector = new Vector3(0,size,0); // gravity in vector3
			tBoard = board[x,y].bottom;
			switch(boardType){ // get the landslide's position to take from...
			case JSFBoardType.Square : default:
				bd = new JSFBoardDirection[]{JSFBoardDirection.Left,
					JSFBoardDirection.Right,JSFBoardDirection.Top};
				break;
			case JSFBoardType.Hexagon :
				bd = new JSFBoardDirection[]{JSFBoardDirection.TopLeft,
					JSFBoardDirection.TopRight, JSFBoardDirection.Top};
				break;
			}
			break;
		case JSFGravity.LEFT :
			gravityVector = new Vector3(-size,0,0); // gravity in vector3

			switch(boardType){ // get the landslide's position to take from...
			case JSFBoardType.Square : default:
				tBoard = board[x,y].right;
				bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
					JSFBoardDirection.Bottom,JSFBoardDirection.Left};
				break;
			case JSFBoardType.Hexagon :
				if(Random.Range(0,2) == 0){
					tBoard = board[x,y].bottomRight;
					bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
						JSFBoardDirection.Bottom, JSFBoardDirection.TopLeft};

				} else {
					tBoard = board[x,y].topRight;
					bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
						JSFBoardDirection.Bottom, JSFBoardDirection.BottomLeft};
				}
				break;
			}
			break;
		case JSFGravity.RIGHT :
			gravityVector = new Vector3(size,0,0); // gravity in vector3
			switch(boardType){ // get the landslide's position to take from...
			case JSFBoardType.Square : default:
				tBoard = board[x,y].left;
				bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
					JSFBoardDirection.Bottom,JSFBoardDirection.Right};
				break;
			case JSFBoardType.Hexagon :
				if(Random.Range(0,2) == 0){
					tBoard = board[x,y].bottomLeft;
					bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
						JSFBoardDirection.Bottom, JSFBoardDirection.TopRight};
				} else {
					tBoard = board[x,y].topLeft;
					bd = new JSFBoardDirection[]{JSFBoardDirection.Top,
						JSFBoardDirection.Bottom, JSFBoardDirection.BottomRight};
				}
				break;
			}
			break;
		}

		if(tBoard == null){ // if board to steal from...
			StartCoroutine(spawnNew(x,y,gravityVector)); // spawn a new piece
			yield break; // finished gravity on this pass... move to the next
		}

		if( tBoard.causesLandslideEffect() ){ // check for landslide effect
			JSFBoard boardL = tBoard.boardEnumToReference(bd[0]); // the board's hypothetical left
			JSFBoard boardR = tBoard.boardEnumToReference(bd[1]); // the board's hypothetical right

			// landslide code
			if(boardL != null &&
			   !boardL.causesLandslideEffect() && boardL.allowGravity() ){ // find pieces on the left
				tBoard = boardL;
			} else if(boardR != null &&
			          !boardR.causesLandslideEffect() && boardR.allowGravity() ){ // find pieces on the right
				tBoard = boardR;
			}

			if(tBoard != boardL && tBoard != boardR ){ // needs to look deeper down...
				// cause landslide below as the top is blocked...
				List<JSFBoard> list = tBoard.getAllBoardInDirection(bd[2]); // list of boards hypothetical bottom
				
				foreach(JSFBoard boardB in list){
					if(boardB.boardEnumToReference(bd[2]) == null){
						break; // we reached the bottom of the board... do not continue...
					}
					x = boardB.boardEnumToReference(bd[2]).arrayRef[0];
					y = boardB.boardEnumToReference(bd[2]).arrayRef[1];
					
					if(!board[x,y].replacementNeeded()){
						break; // if the board here already has a piece, do not continue...
					}
					
					boardL = boardB.boardEnumToReference(bd[0]); // the board's hypothetical left
					boardR = boardB.boardEnumToReference(bd[1]); // the board's hypothetical right
					if(boardL != null && !boardL.causesLandslideEffect()
					   && boardL.allowGravity() ){ // find pieces on the left
						tBoard = boardL;
						break;
					} else if(boardR != null && !boardR.causesLandslideEffect()
					          && boardR.allowGravity() ){ // find pieces on the right
						tBoard = boardR;
						break;
					}
				}
			}

		}


		if( tBoard != null && tBoard.allowGravity() ){ // a valid target to steal a piece from...
			JSFSwipeManager.voidSwipeIfNeeded(x,y); // void any swipes if needed
			if(board[x,y].piece != null){
				board[x,y].piece.removePiece(); // just in case the reference is lost without removal
			}
			board[x,y].piece = tBoard.piece; // steal the piece
			tBoard.piece = null;
			StartCoroutine(animateMove(x,y)); // animate the change
			
			// do the same check on the board we stole from as itself needs replacement
			dropPieces(tBoard.arrayRef[0],tBoard.arrayRef[1]);
		}
	}
	
	// sub-function to update the board box and tween the piece due to gravity movement
    IEnumerator animateMove (int x, int y) {
		// update the local data...
		board[x,y].isFalling = true; // board is falling...
		
		int	distance = countBlockedUnfilled(x,y, false);
		float delay = gravityDropSpeed;
		if(acceleratedVelocity){
			delay =  gravityDropSpeed / Mathf.Max(distance, 1);
		}
		
		board[x,y].applyTweening(delay);
		notifyBoardHasChanged(); // board structure changed, so notify the change~!
		
		// the timer according to the drop speed or updatespeed (whichever longer)
   		yield return new WaitForSeconds(delay);
		
		// update the board box once animation has finished..
		board[x,y].isFalling = false; // no longer falling into position
		board[x,y].isActive = true; // piece is active for checks
		
		if( distance < 1 ){ // check if it has reached bottom
			board[x,y].mustWait = true; // reached bottom, re-activate gravity delay
			if(pieceDropExtraEffect){ // if extra effect is enabled
				board[x,y].applyTweeningAfterEffects(gravityDropSpeed, getVectorEffect(x,y) );
			}
			audioScript.DropSoundFx.play(); // play the drop sound
		}else {
			// check if this new piece needs to fall or not...
			if(board[x,y].boardEnumToReference(bd[2]) != null){
				dropPieces(board[x,y].boardEnumToReference(bd[2]).arrayRef[0],
				           board[x,y].boardEnumToReference(bd[2]).arrayRef[1]);
			}
		}
    }
	
	// gravity effect after falling down - simulates easeInBack
	Vector3[] getVectorEffect(int x, int y){
		
		float offset = 0.35f * size; // the amount of offset you wish for effect
		Vector3 position = board[x,y].position;
		if(board[x,y].isFilled){
			position.z = board[x,y].piece.thisPiece.transform.position.z; // ensure the Z order stays when tweening
		}

		Vector3 pos;
		
		switch(currentGravity){
		case JSFGravity.DOWN : default :
			pos = new Vector3( 0f , offset, 0f);
			return new Vector3[] {position, (position - pos ), position, position};
		case JSFGravity.UP :
			pos = new Vector3( 0f , offset/2.5f, 0f);
			return new Vector3[] {(position + pos), position, position, position };
		case JSFGravity.LEFT :
			pos = new Vector3( offset/3, 0f , 0f);
			return new Vector3[] {(position - pos), position, position, position };
		case JSFGravity.RIGHT :
			pos = new Vector3( offset/3, 0f , 0f);
			return new Vector3[] {(position + pos), position, position, position };
		}
	}
	
	// sub-function to compensate delay of a new spawned piece tweening process
    public IEnumerator spawnNew (int x, int y, Vector3 spawnPoint){
		board[x,y].isFalling = true; // board is falling...
		
		int	distance = countBlockedUnfilled(x,y, false);
		float delay = gravityDropSpeed;
		if(acceleratedVelocity){
			delay =  gravityDropSpeed / Mathf.Max(distance, 1);
		}

		// for custom pieces spawn rate
		JSFPieceDefinition spawned;
		for(int w = 0; w < pieceTypes.Length; w++){
			spawned = pieceTypes[w].chanceToSpawnThis(x,y);
			if(spawned != null){
				board[x,y].spawnNew(spawned, spawnPoint, delay, spawned.skinToUseDuringSpawn(x,y) );
				break;
			}
			if( w == pieceTypes.Length - 1){
				// reached the end, no custom spawn... spawn the default
				board[x,y].spawnNew(pieceTypes[0],spawnPoint, delay, ranType() );
			}
		}
		
		notifyBoardHasChanged(); // board structure changed, so notify the change~!
		
		// the timer according to the drop speed or updatespeed (whichever longer)
   		yield return new WaitForSeconds(delay);
		// update the board box once animation has finished..
		board[x,y].isFalling = false;
		board[x,y].isActive = true;
		board[x,y].mustWait = true; // reached bottom, re-activate gravity delay
		if( distance < 1 ){ // check if it has reached bottom			
			if(pieceDropExtraEffect){ // if extra effect is enabled
				board[x,y].applyTweeningAfterEffects(gravityDropSpeed, getVectorEffect(x,y) );
			}
			audioScript.DropSoundFx.play(); // play the drop sound
		} else {
			// check if this new piece needs to fall or not...
			if(board[x,y].boardEnumToReference(bd[2]) != null){
				dropPieces(board[x,y].boardEnumToReference(bd[2]).arrayRef[0],
				           board[x,y].boardEnumToReference(bd[2]).arrayRef[1]);
			}
		}
    }

	// used to determine the number of unfilled board boxes beyond the current panel
	// limited by panels that pieces cannot pass through
	public int countUnfilled(int x, int y, bool ignoreTotalCount){ // extra function currently un-used by GameManager...
		int count = 0;
		switch(currentGravity){
		case JSFGravity.UP :
			for(int cols = y+1; cols < boardHeight; cols++){
				if(board[x,cols].replacementNeeded() ){
					count++;
					if(ignoreTotalCount) return count; // performance saver, reduce redundant check
				} 
				if(!board[x,cols].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
			}
			break;
		case JSFGravity.DOWN :
			for(int cols = y-1; cols >= 0 ; cols--){
				if(board[x,cols].replacementNeeded() ){
					count++;
					if(ignoreTotalCount) return count; // performance saver, reduce redundant check
				} 
				if(!board[x,cols].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
			}
			break;
		case JSFGravity.RIGHT : 
			for(int rows = x+1; rows < boardWidth; rows++){
				if(board[rows,y].replacementNeeded() ){
					count++;
					if(ignoreTotalCount) return count; // performance saver, reduce redundant check
				}
				if(!board[rows,y].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
			}
			break;
		case JSFGravity.LEFT :
			for(int rows = x-1; rows >=0 ; rows--){
				if(board[rows,y].replacementNeeded() ){
					count++;
					if(ignoreTotalCount) return count; // performance saver, reduce redundant check
				}
				if(!board[rows,y].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
			}
			break;
		}
		return count;
	}
	
	// used to determine the number of unfilled board boxes beyond the current panel
	// limited by panels that block gravity
	public int countBlockedUnfilled(int x, int y, bool ignoreTotalCount){
		int count = 0;
		if(!board[x,y].panel.isStealable()){
			return count; // cannot proceed.. distance = 0
		}
		switch(currentGravity){
		case JSFGravity.UP :
			for(int cols = y+1; cols < boardHeight; cols++){
				if(board[x,cols].replacementNeeded() ){
					count++;
					if(ignoreTotalCount && count > 0 ) return count; // performance saver, reduce redundant check
				}
				if(!board[x,cols].panel.allowsGravity() || !board[x,cols].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
				if(!board[x,cols].panel.pnd.hasStartingPiece ) count--;
			}
			break;
		case JSFGravity.DOWN :
			for(int cols = y-1; cols >= 0 ; cols--){
				if(board[x,cols].replacementNeeded() ){
					count++;
					if(ignoreTotalCount && count > 0 ) return count; // performance saver, reduce redundant check
				}
				if(!board[x,cols].panel.allowsGravity() || !board[x,cols].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
				if(!board[x,cols].panel.pnd.hasStartingPiece ) count--;
			}
			break;
		case JSFGravity.RIGHT : 
			for(int rows = x+1; rows < boardWidth; rows++){
				if(board[rows,y].replacementNeeded() ){
					count++;
					if(ignoreTotalCount && count > 0 ) return count; // performance saver, reduce redundant check
				}
				if(!board[rows,y].panel.allowsGravity() || !board[rows,y].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
				if(!board[rows,y].panel.pnd.hasStartingPiece ) count--;
			}
			break;
		case JSFGravity.LEFT :
			for(int rows = x-1; rows >=0 ; rows--){
				if(board[rows,y].replacementNeeded() ){
					count++;
					if(ignoreTotalCount && count > 0 ) return count; // performance saver, reduce redundant check
				}
				if(!board[rows,y].panel.allowsGravity() || !board[rows,y].panel.isStealable() ){
					break; // do not check further as it cannot pass through here
				}
				if(!board[rows,y].panel.pnd.hasStartingPiece ) count--;
			}
			break;
		}
		return count;
	}

	#endregion Routine Checks Related


	#region Helper Functions
	// ===========================
	// Helper FUNCTIONS
	// ===========================

	// random cubeType generator , just coz the code is too long
	public int ranType() {
		return Random.Range(0,Mathf.Min( NumOfActiveType, pieceTypes[0].skin.Length) );
		// limited by normalpieces types available if numOfActiveType is declared out of bounds
	}

	// OVERLOADED FUNCTION of getBoardsDistance *range of distance Type*
	public List<JSFBoard> getBoardsFromDistance(int[] point, int distMin, int distMax){
		return getBoardsFromDistance(iBoard(point),distMin, distMax );
	}
	// get all the board from a specific distance range~!
	public List<JSFBoard> getBoardsFromDistance(JSFBoard point, int distMin, int distMax){
		List<JSFBoard> temp = new List<JSFBoard>();
		for(int x = distMin; x <= distMax; x++){
			temp.AddRange(getBoardsFromDistance(point,x)); // add the boards within the range specified
		}
		return temp;
	}

	// OVERLOADED FUNCTION of getBoardsDistance
	public List<JSFBoard> getBoardsFromDistance(int[] point, int dist){
		return getBoardsFromDistance(iBoard(point),dist);
	}

	// get all the board from a specific distance
	public List<JSFBoard> getBoardsFromDistance(JSFBoard point, int dist){
		List<JSFBoard> temp = new List<JSFBoard>();

		foreach(JSFBoard itr in board){
			if(boardRadiusDistance(point,itr) == dist){ // is on this specific distance
				temp.Add(itr); // add the board to the list
			}
		}
		return temp;
	}

	// OVERLOADED FUNCTION of boardRadiusDistance
	public int boardRadiusDistance(JSFBoard boardA, JSFBoard boardB ){ 
		return  boardRadiusDistance(boardA.arrayRef,boardB.arrayRef);
	}
	// function to calculate the relative distance between two board locations
	public int boardRadiusDistance(int[] bPosA, int[] bPosB ){
		switch(boardType){
		case JSFBoardType.Square : default :
			int totalX = Mathf.Abs( bPosA[0] - bPosB[0] );
			int totalY = Mathf.Abs( bPosA[1] - bPosB[1] );

			if(squareSwipeMode == JSFsquareMode.CrossType){ // specific for cross-type square
				return totalX + totalY; // each box = 1 distance... no diagonals
			} else {
				return  Mathf.Max(totalX, Mathf.Max(totalY, Mathf.Abs( totalX - totalY ) ) );
			}
		case JSFBoardType.Hexagon :
			Vector3 vecA = hexGetCalcVector(bPosA);
			Vector3 vecB = hexGetCalcVector(bPosB);

			return (int) Mathf.Max(new float[]{ // hex distance formula
				Mathf.Abs(vecA.x - vecB.x),
				Mathf.Abs(vecA.y - vecB.y),
				Mathf.Abs(vecA.z - vecB.z)
			});
		}
	}

	public void playerMadeAMove(){
		// TODO moving rest combo currently not used in JSF
//		if(movingResetsCombo) JSFRelay.onComboEnd(); // end the combo if no special override...
		moves++; // merging, so number of moves increase
		
		JSFRelay.onPlayerMove();
		notifyBoardHasChanged(); // notify the change~!
	}
	
	public bool isLegalSwipe(JSFBoard board){ // can users make a swipe call?
		// add any other criteria here (if any)
		return ( board.isLegalSwipe() ); // meet criteria?
	}

	#endregion Helper Functions

	#region HEXAGON related functions
	// ===========================
	// HEXAGON FUNCTIONS
	// ===========================

	// returns the unsquiggled Hexagon grid
	public int[] hexUnsquiggleArray(int[] array){
		return new int[] { array[0],array[1] - array[0] + (array[0]/2)};
	}

	// returns a vector3 array for distance calculation
	public Vector3 hexGetCalcVector(int[] array){
		array = hexUnsquiggleArray(array);
		return new Vector3(array[0],array[1],(array[0] + array[1])*-1);
	}

	#endregion HEXAGON related functions

	#region game-start sequence

	public void StartGame() { // when the game is actually running...
		if(gameState == JSFGameState.GamePending){
			gameState = JSFGameState.GameActive; // change the state to active...
			// Initialize Timers and settings
			StartCoroutine(updater()); // initiate the update loop
			canMove = true; // allows player to move the pieces
			// call the gameStart for the board objects
			foreach(JSFBoard _board in board){
				_board.onGameStart();
			}
			JSFRelay.onGameStart();
		} else {
			Debug.Log("Game already started... cannot start the game again!");
		}
		
	}

	#endregion game-start sequence
	
	#region Unity Functions
	// ===========================
	// UNITY FUNCTIONS
	// ===========================

	void Awake () { // board needs to be initialized before other scripts can access it
		JSFUtils.gm = this; // make a easy reference to the GameManager ( this script ! ) 
		JSFUtils.wc = GetComponent<JSFWinningConditions>(); // make a easy reference to the WinningConditions script~!
		JSFUtils.vm = GetComponent<JSFVisualManager>(); // make a easy reference to the GUIManager script~!
		JSFRelay.onPreGameStart();
		initializeGame();
		preGameSetup();

		canMove = false; // initially cannot be moved...
		gameState = JSFGameState.GamePending; // game is waiting to be started...
	}

	void Start(){
		// init the board objects
		foreach(JSFBoard _board in board){
			_board.init(); // to show the GUIs for the objects
		}
	}

	#endregion Unity Functions
}
