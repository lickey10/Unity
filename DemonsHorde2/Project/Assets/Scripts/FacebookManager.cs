using UnityEngine;
using System.Collections;

public class FacebookManager : MonoBehaviour {

	public GUISkin MenuSkin;
	public Texture2D ButtonBackground;
	public string NewHighScoreMessage = "I just got a new High Score: $score$! Can you beat it?";
	public string NewHigherLevelMessage = "I just reached level $level$! Can you beat it?";
	public string DefaultMessage = "I got a score of $score$!";
	public string LinkName = "Checkout my Skills!";
	public string LinkUrl = "https://play.google.com/store/apps/details?id=com.SnickleFritz.SantasChristmasRun";
	public int ButtonWidth = 200;
	public int ButtonHeight = 60;
	public Vector2 ButtonLocation = new Vector2(0,0);
	public string KeyHash = "";

	private string fbMessage = "";
	private System.Exception fbException;
	private bool sharedToFB = false;

	void Awake()
	{
		FB.Init(SetInit,OnHideUnity);
	}
	
	private void SetInit()
	{
		enabled = true;
		if(FB.IsLoggedIn)
		{
			OnLoggedIn();
		}
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		if(!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
			Time.timeScale = 1;
	}
	
	void LoginCallback(FBResult result)
	{
		if(FB.IsLoggedIn)
			OnLoggedIn();
	}
	
	void OnLoggedIn()
	{
		
	}

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR

		//for touch device
		#elif (UNITY_ANDROID)
		//gets the keyhash from the certificate
		KeyHash = FB.Android.KeyHash;
		#elif (UNITY_IPHONE)

		#elif (UNITY_WP8)

		#endif

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.skin = MenuSkin;
		
		if (!FB.IsLoggedIn)
		{			
			if(ButtonLocation.x == 0 && ButtonLocation.y == 0)
			{
				//center the button on the screen
				ButtonLocation.x = (Screen.width-ButtonWidth)/2;
				ButtonLocation.y = (Screen.height-ButtonHeight)/2;
			}

			try {
//				if (GUI.Button(new Rect(
//					ButtonLocation.x,
//					ButtonLocation.y,
//					ButtonWidth,
//					ButtonHeight
//					), "", MenuSkin.GetStyle("button_login")))
//				{
//					try {
//						FB.Login("publish_actions", LoginCallback);
//					} catch (System.Exception ex) {
//						fbException = ex;
//						fbMessage = ex.Message;
//						
//					}
//				}

				if (GUI.Button(new Rect(
					ButtonLocation.x,
					ButtonLocation.y,
					ButtonWidth,
					ButtonHeight
					), ButtonBackground, new GUIStyle()))
				{
					try {
						FB.Login("publish_actions", LoginCallback);
					} catch (System.Exception ex) {
						fbException = ex;
						fbMessage = ex.Message;
						
					}
				}
			} catch (System.Exception ex) {
				fbException = ex;
				fbMessage = ex.Message;
			}
		}
		else if(!sharedToFB) //display share dialog
		{
			try {
				//check for a new highscore and modify message
				string newHighScoreMessage = "";
				string newHigherLevelMessage = "";
				
				if(gamestate.Instance.NewHighScore)
				{
					//newHighScoreMessage = "I just got a new High Score on MX Adventure: "+ gamestate.Instance.GetScore() +"! Can you beat it?";
					newHighScoreMessage = NewHighScoreMessage.Replace("$score$",gamestate.Instance.GetScore().ToString());
				}
				else if(gamestate.Instance.NewHigherLevel)
				{
					//newHigherLevelMessage = "I just reached level "+ gamestate.Instance.getLevelProgress() +"! Can you beat it?";
					newHigherLevelMessage = NewHigherLevelMessage.Replace("$level$",gamestate.Instance.getLevelProgress().ToString());
				}
				else
				{
					//newHighScoreMessage = "I got a score of "+ gamestate.Instance.GetScore() +" on MX Adventure!";
					newHighScoreMessage = DefaultMessage.Replace("$score$",gamestate.Instance.GetScore().ToString());
				}

				FB.Feed(
					linkCaption: newHighScoreMessage + newHigherLevelMessage,
					//picture: "http://www.promotocross.com/sites/default/files/WPS_FLYracing_logos_640x120_v3.jpg",
					linkName: LinkName,
					link: LinkUrl
					);
				
				sharedToFB = true;
			} catch (System.Exception ex) {
				print(ex.Message);
			}
		}
	}
}
