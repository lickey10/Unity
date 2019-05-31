/*
        Unity 3D: Game Start Script Source for State Manager
       
    Copyright 2012 FIZIX Digital Agency
    http://www.fizixstudios.com
       
        For more information see the tutorial at:
    http://www.fizixstudios.com/labs/do/view/id/unity-game-state-manager
       
       
    Notes:
        This script is a basic GUI script to create a new game state; you will need the statemanager.cs
        script.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gamestart : MonoBehaviour
{
	public Texture2D ButtonBackground;
	public int NumberOfLevels = 1;
	public int Health = 1;
	public int NumberOfLives = 3;
	public int LevelProgress = 1;
	public GUISkin MenuSkin;
	public Texture CursorTexture;
	public int BannerHeight = 50;

	private int activeLevel = 1;
	private gamestate loadedGameState;
	private string fbMessage = "";
	private System.Exception fbException;

	int X = (Screen.width - 240 ) / 2;
	int Y = (Screen.height + 120) / 2;
                                                                                           

	void Start () 
	{
		gamestate.Instance.Load("GameState");

//		if (CursorTexture != null)
//			gamestate.Instance.CursorTexture = CursorTexture;

		gamestate.Instance.SetBannerHeight(BannerHeight);
	}

	void Awake()
	{
		//Util.Log("Awake");
		

//		#if UNITY_WEBPLAYER
//		// Execute javascript in iframe to keep the player centred
//		string javaScript = @"
//            window.onresize = function() {
//              var unity = UnityObject2.instances[0].getUnity();
//              var unityDiv = document.getElementById(""unityPlayerEmbed"");
//
//              var width =  window.innerWidth;
//              var height = window.innerHeight;
//
//              var appWidth = " + CanvasSize.x + @";
//              var appHeight = " + CanvasSize.y + @";
//
//              unity.style.width = appWidth + ""px"";
//              unity.style.height = appHeight + ""px"";
//
//              unityDiv.style.marginLeft = (width - appWidth)/2 + ""px"";
//              unityDiv.style.marginTop = (height - appHeight)/2 + ""px"";
//              unityDiv.style.marginRight = (width - appWidth)/2 + ""px"";
//              unityDiv.style.marginBottom = (height - appHeight)/2 + ""px"";
//            }
//
//            window.onresize(); // force it to resize now";
//		Application.ExternalCall(javaScript);
//		#endif

	}

	private void SetInit()
	{
		//Util.Log("SetInit");
		enabled = true; // "enabled" is a property inherited from MonoBehaviour
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		//Util.Log("OnHideUnity");
		if (!isGameShown)
		{
			// pause the game - we will need to hide
			Time.timeScale = 0;
		}
		else
		{
			// start the game back up - we're getting focus again
			Time.timeScale = 1;
		}
	}

	// Our Startscreen GUI
	void OnGUI ()
	{
		if(ButtonBackground != null)
		{
			
			if (
				GUI.Button(
				// Center in X, 1/3 of the height in Y
				new Rect(
				X,
				Y,
				240,
				120
				)
				,ButtonBackground, new GUIStyle()
				)
				)
			{
				StartGame();


//				try {
//						string username = "";
//						username = FB.UserId;
//						username = "";
//						string theResult = "";
//			
//						if (FB.IsLoggedIn) {
////								//Debug.Log("sending to facebook");
////								Dictionary<string, string> query = new Dictionary<string, string> ();
////								//query.Add("score","5");
////								query ["score"] = "5";
////			
////								FB.API ("/" + FB.UserId + "/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { 
////										//Util.Log("Result: " + r.Text);
////										theResult = r.Text;
////								}, query);
//
//
//
//
//
//
//
////						var query = new Dictionary<string, string>(){{"score", "5"}};
////						//query["score"] = "5";
////						//FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { fbMessage = r.Text; //Util.Log("Result: " + r.Text);
////						// }, query);
////
////						FB.API("/USER_ID/scores", Facebook.HttpMethod.POST, publishActionCallback, query);
//			
//								//Debug.Log("done sending to facebook");
//
//
////						//works !!!!!!!!!
////						FB.Feed(
////							linkCaption: "I just smashed 10000000 friends! Can you beat it?",
////							picture: "http://www.friendsmash.com/images/logo_large.jpg",
				////							linkName: "Checkout my Water Balloon Drop greatness!",
////							link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")
////							);
////						//works !!!!!!!!!
//
//
//
//						FB.Feed(
//							linkCaption: "I just scored 10000000! Can you beat it?",
//							picture: "https://lh3.ggpht.com/nmT6A84KftF2lKaUYGrLiOrKLbLZJCmwSk_xVre37IJ_tbC-DJKvccswU2CV2IDe0w=w300-rw",
				//							linkName: "Checkout my Water Balloon Drop! prowess!",
//							link: "https://play.google.com/store/apps/details?id=com.SnickleFritz.HappyPlane"
//							);
//
//
////						var query = new Dictionary<string, string>();
////						query["score"] = "5";
////						FB.API("/me/scores", Facebook.HttpMethod.POST, publishActionCallback, query);
//
//						}
//				} catch (System.Exception ex) {
//
//			fbMessage = ex.Message;
//				}
				}
			}

			GUI.skin = MenuSkin;
			//if (Application.loadedLevel != mainMenuLevel) return;  // don't display anything except when in main menu
			
	}

	void OnLoggedIn()                                                                          
	{                                                                                          
		//Util.Log("Logged in. ID: " + FB.UserId);  

//		try {
//						string username = "";
//						username = FB.UserId;
//						username = "";
//						string theResult = "";
//			
//						if (FB.IsLoggedIn) {
//								//Debug.Log("sending to facebook");
//								Dictionary<string, string> query = new Dictionary<string, string> ();
//								//query.Add("score","5");
//								query ["score"] = "5";
//			
//								FB.API ("/" + FB.UserId + "/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { 
//										//Util.Log("Result: " + r.Text);
//										theResult = r.Text;
//								}, query);
//			
//								//Debug.Log("done sending to facebook");
//						}
//				} catch (System.Exception ex) {
//
//			fbMessage = ex.Message;
//				}
	} 

	public void StartGame()
	{
		print("Starting game");
		
		DontDestroyOnLoad(gamestate.Instance);

		gamestate.Instance.SetGameRunning(true);

		if(gamestate.Instance.getHighScore() > 0)
		{
			//gamestate.Instance.StartState();
			gamestate.Instance.StartState(NumberOfLevels,gamestate.Instance.getLives(),gamestate.Instance.getLevelProgress(),activeLevel,gamestate.Instance.GetHealth(), gamestate.Instance.getHighScore());
		}
		else
			gamestate.Instance.StartState(NumberOfLevels,NumberOfLives,LevelProgress,activeLevel,Health,0);

	}
}