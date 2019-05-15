using UnityEngine;
using System.Collections;

//two scripts are made of the same kind with little changes, this one used in arcade mode scene and other textinstantiate1 is used in timetrial scene


public class textinstantiate : MonoBehaviour {

	//a hell lot of prefabs are created for creating the illusion
	//which are drag droped in this public text gameobject
	public GameObject[] text;

	//a copy of each instantiated text object is added in this clone gameobject
	GameObject[] clone;

	//this one is useless, if you wanna use the OnGUI for touch buttons, a commented code is added at the bottom
	public GUIStyle[] styles;

	//just variables to for string comparision
	string textcolor="q";
	string color="p";
	string c="z";
	string txt="x";

	//public score guitext drag-n-dropped here
	public GUIText score;

	//for scoring
	int count=0;

	//pointer objects(blinking indicators on screen)
	public GameObject w1, w2;

	//colors and text guitexture are grag-dropped here from the "game" named object in hierarchy
	public GUITexture[] colors;
	//colors guitexture from "common" gameobject are added here
	public GUITexture[] common;

	bool flag=false;

	//button animations
	public Animation[] anim;

	//other gameobject from hierarchy
	public GameObject gameover, game, commongui;

	//gameover ui components
	public GameObject[] gameovergui;
	public Sprite[] gameovertouch;
	public Sprite[] tempgameover;

	void Awake()
	{
		Time.timeScale=1;
		PlayerPrefs.SetInt("timetrial", 0);
		PlayerPrefs.SetInt("gameover", 0);
		PlayerPrefs.Save();
		foreach(Animation a in anim)
		{
			a.wrapMode=WrapMode.Once;
		}

		//if not level 3 "common" gameobject is set to active else "game" gameobject is set to active from hierarchy
		if(PlayerPrefs.GetInt("level")!=3)
		{
			commongui.SetActive(true);
		}
		else
			game.SetActive(true);
	}
	
	// Use this for initialization
	void Start () 
	{

		//instantiation of texts from invokerepeating function
		if(PlayerPrefs.GetInt("level")==3)
		{
			InvokeRepeating("create", 0, 2f);
		}
		else
			InvokeRepeating("create", 0, 1f);


		//blinking of pointers through invokerepeating
		InvokeRepeating("fun", 0, 1f);
	}

	void fun()
	{
		if(flag)
		{
			w1.SetActive(false);
			w2.SetActive(false);
			flag=false;
		}
		else
		{
			w1.SetActive(true);
			w2.SetActive(true);
			flag=true;
		}
	}

	void create()
	{
		Instantiate(text[Random.Range(0, text.Length)], new Vector3(0, 4, 0), Quaternion.identity);
		clone=GameObject.FindGameObjectsWithTag("text");
		
		foreach(GameObject g in clone)
		{
			g.transform.position=new Vector2(0, g.transform.position.y-2f);
		}
	}


	//this function recives the values from "textcontrol script" via sendmessage function
	void pickme(string str1)
	{
		if(PlayerPrefs.GetInt("level")==3)
		{
			textcolor=str1[0].ToString();
			color=str1[1].ToString();
			//Debug.Log(textcolor+"textcolor");
			//Debug.Log(color+"color");
		}
		else
			textcolor=str1;
	}

	//destroying the instantiated text objects when buttons(color or text buttons from scene) are pressed at the right time
	void remove()
	{
		clone=GameObject.FindGameObjectsWithTag("text");
		foreach(GameObject g in clone)
		{
			if(g.transform.position.y==-2f)
			{
				count++;
				Destroy(g);
			}
		}
	}

	void Update()
	{

		if(PlayerPrefs.GetInt("pause")==0)
		{
			Time.timeScale=1;
		}

		//here you can see the implementation of touch control via two different methods...using input.touch and from input.mousebuttondown...
		//each button in hierarchy represents a its color name or text name...which are assigned in the variables when touched
		score.text=count+"";
		if(PlayerPrefs.GetInt("level")==3)
		{
		if (Input.touchCount > 0) 
		{
			for(int i = 0; i < Input.touchCount; i++) 
				{
				Touch t = Input.GetTouch(i);
				
				if (t.phase == TouchPhase.Began) {
					if (colors[0].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
							c="b";
							anim[0].Play();
					}
					
					if (colors[1].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="c";
							anim[1].Play();
					}

					if (colors[2].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="g";
							anim[2].Play();
					}


					if (colors[3].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="m";
							anim[3].Play();
					}

					if (colors[4].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="o";
							anim[4].Play();
					}

					if (colors[5].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="r";
							anim[5].Play();
					}

					if (colors[6].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="v";
							anim[6].Play();
					}

					if (colors[7].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						c="y";
							anim[7].Play();
					}
					if (colors[8].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="b";
							anim[8].Play();
					}
					
					if (colors[9].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="c";
							anim[9].Play();
					}
					
					if (colors[10].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="g";
							anim[10].Play();
					}
					
					
					if (colors[11].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="m";
							anim[11].Play();
					}
					
					if (colors[12].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="o";
							anim[12].Play();
					}
					
					if (colors[13].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="r";
							anim[13].Play();
					}
					
					if (colors[14].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="v";
							anim[14].Play();
					}
					
					if (colors[15].HitTest(t.position, Camera.main)) {
							GetComponent<AudioSource>().Play();
						txt="y";
							anim[15].Play();
					}
				}
				if (t.phase == TouchPhase.Ended) {
					// Stop all movement
					Debug.Log("touch ended");
					c="a";
				}
			}
		}
		}

		if(Input.GetMouseButtonDown(0))
		{

			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);


			if (common[0].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="b";
				anim[16].Play();
			}
			if (common[1].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="c";
				anim[17].Play();
			}
			if (common[2].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="g";
				anim[18].Play();
			}
			if (common[3].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="m";
				anim[19].Play();
			}
			if (common[4].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="o";
				anim[20].Play();
			}
			if (common[5].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="r";
				anim[21].Play();
			}
			if (common[6].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="v";
				anim[22].Play();
			}
			if (common[7].HitTest(Input.mousePosition, Camera.main)) {
				GetComponent<AudioSource>().Play();
				txt="y";
				anim[23].Play();
			}

			if (hit.collider.name=="home") {
				GetComponent<AudioSource>().Play();
				Application.LoadLevel("menu");
				hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=gameovertouch[0];
			}
			if (hit.collider.name=="restart") {
				GetComponent<AudioSource>().Play();
				Application.LoadLevel(Application.loadedLevel);
				hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=gameovertouch[1];
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			txt="a";
			gameovergui[0].GetComponent<SpriteRenderer>().sprite=tempgameover[0];
			gameovergui[1].GetComponent<SpriteRenderer>().sprite=tempgameover[1];
		}
		
//		if(PlayerPrefs.GetInt("pause")==1)
//		{
//			//pause.transform.position=Vector2.Lerp(pause.transform.position, new Vector2(0, 0), Time.deltaTime*10);
//			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(-20f, 0), Time.deltaTime*10);
//			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(-20f, 0), Time.deltaTime*10);
//		}
//		if(PlayerPrefs.GetInt("pause")==0)
//		{
//			//pause.transform.position=Vector2.Lerp(pause.transform.position, new Vector2(1.5f, 0), Time.deltaTime*10);
//			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(0, 0), Time.deltaTime*10);
//			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(0, 0), Time.deltaTime*10);
//		}
		
		if(PlayerPrefs.GetInt("gameover")==1)
		{
			gameover.transform.position=Vector2.Lerp(gameover.transform.position, new Vector2(0, 0), Time.deltaTime*10);
			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(20f, 0), Time.deltaTime*10);
			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(20f, 0), Time.deltaTime*10);

		}

//		if(PlayerPrefs.GetInt("gameover")==1 && !flag)
//		{
//			PlayerPrefs.SetInt("ad", PlayerPrefs.GetInt("ad")+1);
//			PlayerPrefs.Save();
//			if(PlayerPrefs.GetInt("ad")>2)
//			{
//				GoogleMobileAd.StartInterstitialAd();
//				PlayerPrefs.SetInt("ad", 0);
//				PlayerPrefs.Save();
//			}
//			Debug.Log(PlayerPrefs.GetInt("ad").ToString());
//			flag=true;
//		}
		
	}



	//main logic of the game, simple comparision of the strings are done to check the incoming instantiated text objects when they reach the blinking pointer
	//this way it is checked if button color pressed and the instantiated text's color are same // this game is all illusion, i would recommend you to play it several times before going through scripts to understand it.
	//even you dont learn the game u can learn touch controls and menu transitions from this
	void FixedUpdate()
	{
		if(PlayerPrefs.GetInt("level")==3)
		{
			if(color.Equals(c) && textcolor.Equals(txt))
			{
				remove();
			}
		}
		else
		{
			if(textcolor.Equals(txt))
			{
				remove();
			}
		}
		
	}
	
//	void OnGUI()
//	{
//		
//		if(PlayerPrefs.GetInt("gameover")==1)
//		{
//			if(GUI.Button(new Rect(Screen.width*.4f, Screen.height*.4f, Screen.width*.2f, Screen.height*.2f), "Restart"))
//			{
//				Application.LoadLevel(Application.loadedLevel);
//			}
//		}
//
//		/*
//		if(PlayerPrefs.GetInt("level")==3)
//		{
//			if(GUI.Button(new Rect(Screen.width*.02f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[0]))
//			{
//				Debug.Log("yello");
//				if(textcolor.Equals("y"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//				
//			}
//			if(GUI.Button(new Rect(Screen.width*.02f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[1]))
//			{
//				if(textcolor.Equals("c"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.02f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[2]))
//			{
//				if(textcolor.Equals("m"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.02f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[3]))
//			{
//				if(textcolor.Equals("v"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.14f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[4]))
//			{
//				if(textcolor.Equals("r"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.14f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[5]))
//			{
//				if(textcolor.Equals("g"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.14f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[6]))
//			{
//				if(textcolor.Equals("b"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.14f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[7]))
//			{
//				if(textcolor.Equals("o"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}
//
//			//color dots
//
//			if(GUI.Button(new Rect(Screen.width*.76f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[0]))
//			{
//				
//				if(color.Equals("y"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//				
//			}
//			if(GUI.Button(new Rect(Screen.width*.76f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[1]))
//			{
//				if(color.Equals("c"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.76f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[2]))
//			{
//				if(color.Equals("m"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.76f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[3]))
//			{
//				if(color.Equals("v"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.88f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[4]))
//			{
//				if(color.Equals("r"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.88f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[5]))
//			{
//				if(color.Equals("g"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.88f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[6]))
//			{
//				if(color.Equals("b"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.88f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[7]))
//			{
//				if(color.Equals("o"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}
//
//
//		}
//
//		else
//		{
//*/
//
//		if(PlayerPrefs.GetInt("level")!=3)
//		{
//			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[0]))
//			{
//
//				if(textcolor.Equals("y"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//
//			}
//			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[1]))
//			{
//				if(textcolor.Equals("c"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[2]))
//			{
//				if(textcolor.Equals("m"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[3]))
//			{
//				if(textcolor.Equals("v"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.8f, Screen.height*.2f, Screen.width*.10f, Screen.width*.10f), "" , styles[4]))
//			{
//				if(textcolor.Equals("r"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.8f, Screen.height*.4f, Screen.width*.10f, Screen.width*.10f), "" , styles[5]))
//			{
//				if(textcolor.Equals("g"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.8f, Screen.height*.6f, Screen.width*.10f, Screen.width*.10f), "" , styles[6]))
//			{
//				if(textcolor.Equals("b"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}if(GUI.Button(new Rect(Screen.width*.8f, Screen.height*.8f, Screen.width*.10f, Screen.width*.10f), "" , styles[7]))
//			{
//				if(textcolor.Equals("o"))
//				{
//					clone=GameObject.FindGameObjectsWithTag("text");
//					foreach(GameObject g in clone)
//					{
//						if(g.transform.position.y==-2f)
//						{
//							count++;
//							Destroy(g);
//						}
//					}
//				}
//			}
//		}
//	}
}
