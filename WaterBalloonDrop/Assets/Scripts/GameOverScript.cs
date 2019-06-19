using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	public GUIStyle customGuiStyle;
	public Sprite[] Backgrounds;
	public GUISkin MenuSkin;

	int logoX;
	int logoY;
	private string fbMessage = "";
	private System.Exception fbException;
	private bool sharedToFB = false;

	void Awake()
	{
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


	string meQueryString = "/v2.0/me?fields=id,first_name,friends.limit(100).fields(first_name,id,picture.width(128).height(128)),invitable_friends.limit(100).fields(first_name,id,picture.width(128).height(128))";


	void Start()
	{
		gamestate.Instance.SetGameRunning(false);
		
		try {
			//save gamestate to player prefs
			gamestate.Instance.Save("GameState");
		} catch (System.Exception ex) {
			string message = ex.Message;
		}

		//set background
		if(GameObject.FindGameObjectWithTag("background") != null)
			GameObject.FindGameObjectWithTag("background").GetComponent<SpriteRenderer>().sprite = Backgrounds[gamestate.Instance.getActiveLevel() - 1];
	}

	void OnStart()
	{
		customGuiStyle = new GUIStyle();
		
		customGuiStyle.font = (Font)Resources.Load("Fonts/advlit");
		customGuiStyle.active.textColor = Color.red; // not working
		customGuiStyle.hover.textColor = Color.blue; // not working
		customGuiStyle.normal.textColor = Color.green;
		customGuiStyle.fontSize = 50;
		
		customGuiStyle.stretchWidth = true; // ---
		customGuiStyle.stretchHeight = true; // not working, since backgrounds aren't showing
	}

	// Update is called once per frame
	void Update () {


	}

	void OnGUI()
	{
		logoX = (Screen.width - 300 ) / 2;
		logoY = (Screen.height - 450) / 2;

		//logo
		//drop shadow
//		customGuiStyle.normal.textColor = Color.black;
//		GUI.Box(new Rect( logoX+3, logoY+3, 450, 30 ), "Game Over!" ,customGuiStyle);
//		
//		customGuiStyle.normal.textColor = Color.green;
//		GUI.Box(new Rect( logoX, logoY, 450, 30 ), "Game Over!" ,customGuiStyle);


		GUI.skin = MenuSkin;


	}

	void OnLoggedIn()                                                                          
	{                                                                                          

	} 
}
