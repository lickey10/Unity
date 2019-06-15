using UnityEngine;
using System.Collections;

/*
 * JSFPanelDefinition main class
 * ==========================
 * 
 * it is the holder as well as the definition of all its children.
 * There are functions all children must implement as well as certain functions
 * that users can change to suit their piece properly.
 * 
 * 
 */

public abstract class JSFPanelDefinition : MonoBehaviour {

	public bool isInFront = false;
	public bool hasStartingPiece = true;
	public bool hasDefaultPanel = true;
	public bool hasNoSkin = false;
	public int defaultStrength = 0;
	public SkinList skinListToUse = SkinList.SquareList;
	[HideInInspector] public JSFGameManager gm {get{return JSFUtils.gm;}} // easy reference call
	public GameObject[] skinSquare; // how the panel will look like in square mode
	public GameObject[] skinHex; // how the panel will look like in hex mode
	public GameObject[] skin{get{ // skin variable for game engine to call
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


	#region virtuals
	// ===============================
	// virtual functions - scripters can modify as they see fit or choose to use the defaults :)
	// ===============================

	// for external scripts to call, will indicate that the panel got hit
	public virtual bool gotHit(JSFBoardPanel bp){
		playAudioVisuals(bp); // play audio visual for selected panels
		bp.durability--;
		return true;
	}

	// called by Board during GameManager game-start phase
	// different from Start() as that is unity start, not neccessarily the game is set-up yet
	public virtual void onGameStart(JSFBoard board){
		// do nothing....
	}

	// optional onCreate function to define extra behaviours
	public virtual void onPanelCreate(JSFBoardPanel bp){
		// do nothing...
	}
	// optional onDestroy function to define extra behaviours
	public virtual void onPanelDestroy(JSFBoardPanel bp){
		// do nothing...
	}
	// optional onPlayerMove called by GameManager when player makes the next move
	public virtual void onPlayerMove(JSFBoardPanel bp){
		// do nothing...
	}
	// optional onBoardStabilize called by GameManager when board stabilize and gets a suggestion
	public virtual void onBoardStabilize(JSFBoardPanel bp) {
		// do nothing...
	}

	// optional onSkinChange called by BoardPanel when panel changes skin
	public virtual void onSkinChange(JSFBoardPanel bp) {
		// do nothing...
	}

	// optional onPanelClicked called by JSFRelay when panel is clicked
	public virtual void onPanelClicked(JSFBoardPanel bp) {
		// do nothing...
	}
	
	// for external scripts to call, if splash damage hits correct panel type, perform the hit
	public virtual bool splashDamage(JSFBoardPanel bp){
		// do nothing...
		return false; // default behaviour
	}
	#endregion virtuals

	#region abstracts
	// ===============================
	// abstract functions - must be present and defined in each child script
	// ===============================
	
	// function to check if pieces can fall into this board box
	public abstract bool allowsGravity(JSFBoardPanel bp);

	// function to check if pieces can re-appear on this board box
	public abstract bool allowsAppearReplacement(JSFBoardPanel bp);
	
	// if the piece here can be added to the swipe chain
	public abstract bool isSwippable(JSFBoardPanel bp);
	
	// if the piece here (if any) can be destroyed
	public abstract bool isDestructible(JSFBoardPanel bp);
	
	// function to check if pieces can be stolen from this box by gravity
	public abstract bool isStealable(JSFBoardPanel bp);
	
	// function to for resetBoard() to know which panel can be resetted
	public abstract bool isFillable(JSFBoardPanel bp);
	
	// function to check if this board is a solid panel
	// determines if neighbour pieces will landslide; true = landslide / false = does not landslide
	public abstract bool isSolid(JSFBoardPanel bp);
	
	// function to play the audio visuals of this panel
	public abstract void playAudioVisuals(JSFBoardPanel bp);

	#endregion abstracts

}
