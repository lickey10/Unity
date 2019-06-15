using UnityEngine;
using System.Collections;
using PathologicalGames;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is the animation/particles section.
/// "Powers" and in "PowerMerge" references animations from this script;
/// which in turn will generate the called animations.
///  
/// </summary> ##################################

public enum JSFanimType{GLOBALDESTROY, NOMOREMOVES, ARROWH, ARROWV, ARROWVH,
	ARROWTRBL, ARROWTLBR, ARROWTX, BOMB, Special5, Special6, ROCKHIT, LOCKHIT,
	ICEHIT, SHADEHIT, CONVERTSPEC, TREASURECOLLECTED};

public class JSFDefaultAnimations : MonoBehaviour {

	public GameObject PieceDestroyEffect; // global piece destroy effect
	public GameObject noMoreMoves; // no more moves effect
	public GameObject horizontalAnim;
	public GameObject verticalAnim;
	public GameObject topRBottomLAnim;
	public GameObject topLBottomRAnim;
	public GameObject starAnim;
	public GameObject rainbowAnim;
	public GameObject bombAnim;
	public GameObject rockAnim;
	public GameObject lockAnim;
	public GameObject iceAnim;
	public GameObject shadedAnim;
	public GameObject convertingAnim;
	public GameObject treasureCollectedAnim;
	
	JSFGameManager gm {get{return JSFUtils.gm;}}
	string animPoolName {get{return JSFUtils.particlePoolName;}}

	
	/*
	 * NOTES :
	 * 
	 * Use "gm.board[x,y].position" to get the origin location of the caller
	 * gm.boardWidth / gm.boardHeight    <--- the width and height of the current board
	 * 
	 * ---------------------------
	 * 
	 * IMPORTANT ~!!
	 * 
	 * Pool Manager version of the script has an auto-despawn function
	 * located in the "Lifespan.cs" script found in area 51/GUI Related/
	 * 
	 * 
	 */

	// OVERLOADED FUNCTION for doAnim
	public void doAnim(JSFanimType animType, int[] arrayRef){
		doAnim( animType, arrayRef[0], arrayRef[1] ); // call main function
	}

	// External scripts will call this function
	// From here, CustomAnimations script will select the appropriate anim to use.
	public void doAnim(JSFanimType animType, int x, int y){
		switch (animType){
		case JSFanimType.GLOBALDESTROY :
			if(PieceDestroyEffect){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(PieceDestroyEffect.transform, gm.board[x,y].position, Quaternion.identity);
				} else {
					Instantiate(PieceDestroyEffect, gm.board[x,y].position, Quaternion.identity);
				}
			}
			break;
		case JSFanimType.NOMOREMOVES :
			if(noMoreMoves){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(noMoreMoves.transform);
				}
				else {
					Instantiate(noMoreMoves);
				}
			}
			break;
		case JSFanimType.ARROWH :
			if(horizontalAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(horizontalAnim.transform, gm.board[x,y].position, Quaternion.identity);
				} else {
					Instantiate(horizontalAnim,gm.board[x,y].position,Quaternion.identity);
				}
				
			}
			break;
		case JSFanimType.ARROWV :
			if(verticalAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(verticalAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(verticalAnim,gm.board[x,y].position,Quaternion.identity);
				}
				
			}
			break;
		case JSFanimType.ARROWVH :
			if(verticalAnim != null && horizontalAnim != null){ // animation effect
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(verticalAnim.transform,gm.board[x,y].position,Quaternion.identity);
					PoolManager.Pools[animPoolName].Spawn(horizontalAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(verticalAnim,gm.board[x,y].position,Quaternion.identity);
					Instantiate(horizontalAnim,gm.board[x,y].position,Quaternion.identity);
				}

			}
			break;
		case JSFanimType.ARROWTLBR :
			if(topLBottomRAnim != null){ // animation effect
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(topLBottomRAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(topLBottomRAnim,gm.board[x,y].position,Quaternion.identity);
				}
				
			}
			break;
		case JSFanimType.ARROWTRBL :
			if(topRBottomLAnim != null){ // animation effect
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(topRBottomLAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(topRBottomLAnim,gm.board[x,y].position,Quaternion.identity);
				}
				
			}
			break;
		case JSFanimType.ARROWTX : // is when match-4 power combine with match-T
			if(verticalAnim != null && horizontalAnim != null){ // animation effect
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(verticalAnim.transform,gm.board[x,y].position,Quaternion.identity);
					PoolManager.Pools[animPoolName].Spawn(horizontalAnim.transform,gm.board[x,y].position,Quaternion.identity);
					if(x+1 < gm.boardWidth){
						PoolManager.Pools[animPoolName].Spawn(verticalAnim.transform,gm.board[x+1,y].position,Quaternion.identity);}
					if(x-1 >= 0){
						PoolManager.Pools[animPoolName].Spawn(verticalAnim.transform,gm.board[x-1,y].position,Quaternion.identity);}
					if(y+1 < gm.boardHeight){
						PoolManager.Pools[animPoolName].Spawn(horizontalAnim.transform,gm.board[x,y+1].position,Quaternion.identity);}
					if(y-1 >= 0){
						PoolManager.Pools[animPoolName].Spawn(horizontalAnim.transform,gm.board[x,y-1].position,Quaternion.identity);}				
				} else {
					Instantiate(verticalAnim,gm.board[x,y].position,Quaternion.identity);
					Instantiate(horizontalAnim,gm.board[x,y].position,Quaternion.identity);
					if(x+1 < gm.boardWidth){
						Instantiate(verticalAnim,gm.board[x+1,y].position,Quaternion.identity);}
					if(x-1 >= 0){
						Instantiate(verticalAnim,gm.board[x-1,y].position,Quaternion.identity);}
					if(y+1 < gm.boardHeight){
						Instantiate(horizontalAnim,gm.board[x,y+1].position,Quaternion.identity);}
					if(y-1 >= 0){
						Instantiate(horizontalAnim,gm.board[x,y-1].position,Quaternion.identity);}				
				}
			}
			break;
		case JSFanimType.BOMB :
			if(starAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(starAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(starAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.Special5 :
			if(rainbowAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(rainbowAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(rainbowAnim,gm.board[x,y].position,Quaternion.identity);
				}

			}
			break;
		case JSFanimType.Special6 :
			if(bombAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(bombAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(bombAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.LOCKHIT :
			if(lockAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(lockAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(lockAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.ROCKHIT :
			if(rockAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(rockAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(rockAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.ICEHIT :
			if(iceAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(iceAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(iceAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.SHADEHIT :
			if(shadedAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(shadedAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(shadedAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.CONVERTSPEC :
			if(convertingAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(convertingAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(convertingAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		case JSFanimType.TREASURECOLLECTED :
			if(treasureCollectedAnim != null){
				if(JSFUtils.isPooling){
					PoolManager.Pools[animPoolName].Spawn(treasureCollectedAnim.transform,gm.board[x,y].position,Quaternion.identity);
				} else {
					Instantiate(treasureCollectedAnim,gm.board[x,y].position,Quaternion.identity);
				}
			}
			break;
		}
	}
}
