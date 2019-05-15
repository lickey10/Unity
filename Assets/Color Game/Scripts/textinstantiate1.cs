using UnityEngine;
using System.Collections;


// this script is similar to the "textinstantiate" script only have a little changes...
//everything you need know is decribed in another script
public class textinstantiate1 : MonoBehaviour {

	public GameObject[] text;
	GameObject[] clone;
	public GUIStyle[] styles;
	string textcolor="p";
	string color="q";
	public GUIText score, gtime;
	int count=0;
	public GameObject w1, w2;
	bool flag=false;
	string c="s";
	string txt="t";
	public GUITexture[] colors;
	public Animation[] anim;
	float time=31;
	public GameObject gameover, pause, game, commongui;
	public GameObject[] gameovergui;
	public Sprite[] gameovertouch;
	public Sprite[] tempgameover;
	public GameObject[] pausegui;
	public Sprite[] pausetouch;
	public Sprite[] temppause;
	public GUITexture[] common;
	void Awake()
	{
		Time.timeScale=1;
		
		PlayerPrefs.SetInt("ad", PlayerPrefs.GetInt("ad")+1);
		PlayerPrefs.SetInt("timetrial", 1);
		PlayerPrefs.SetInt("gameover", 0);
		PlayerPrefs.SetInt("pause", 2);
		PlayerPrefs.Save();
		foreach(Animation a in anim)
		{
			a.wrapMode=WrapMode.Once;
		}
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
		if(PlayerPrefs.GetInt("level")==3)
		{
			InvokeRepeating("create", 0, 2f);
		}
		else
			InvokeRepeating("create", 0, 1f);
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
		if(PlayerPrefs.GetInt("gameover")!=1)
		{
			time--;
			gtime.text=time.ToString();
			Debug.Log(time.ToString());
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

	void pickme(string str1)
	{
		if(PlayerPrefs.GetInt("level")==3)
		{
			textcolor=str1[0].ToString();
			color=str1[1].ToString();
		}
		else
			textcolor=str1;
	}

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

		score.text=count+"";
		if(PlayerPrefs.GetInt("level")==3)
		{
		if (Input.touchCount > 0) 
		{
			for(int i = 0; i < Input.touchCount; i++) 
				{
				Touch t = Input.GetTouch(i);
				
				if (t.phase == TouchPhase.Began) {
						GetComponent<AudioSource>().Play();
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

			if(time>1)
			{
				if(hit.collider.name=="pausebg")
				{
					Invoke("farzi", .5f);
					PlayerPrefs.SetInt("pause", 1);
					PlayerPrefs.Save();
				}
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
			if(hit.collider.name=="resume_p")
			{
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetInt("pause", 0);
				PlayerPrefs.SetInt("ad", PlayerPrefs.GetInt("ad")+1);
				PlayerPrefs.Save();
				hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=pausetouch[0];
			}
			if(hit.collider.name=="restart_p")
			{
				GetComponent<AudioSource>().Play();
				Application.LoadLevel(Application.loadedLevel);
				hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=pausetouch[1];
			}
			if(hit.collider.name=="home_p")
			{
				GetComponent<AudioSource>().Play();
				Application.LoadLevel("menu");
				hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=pausetouch[2];
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			txt="a";
			gameovergui[0].GetComponent<SpriteRenderer>().sprite=tempgameover[0];
			gameovergui[1].GetComponent<SpriteRenderer>().sprite=tempgameover[1];
			pausegui[0].GetComponent<SpriteRenderer>().sprite=temppause[0];
			pausegui[1].GetComponent<SpriteRenderer>().sprite=temppause[1];
			pausegui[2].GetComponent<SpriteRenderer>().sprite=temppause[2];
		}

		if(PlayerPrefs.GetInt("pause")==1)
		{
			pause.transform.position=Vector2.Lerp(pause.transform.position, new Vector2(0, 0), Time.deltaTime*10);
			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(-20f, 0), Time.deltaTime*10);
			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(-20f, 0), Time.deltaTime*10);
		}
		if(PlayerPrefs.GetInt("pause")==0)
		{
			pause.transform.position=Vector2.Lerp(pause.transform.position, new Vector2(20f, 0), Time.deltaTime*10);
			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(0, 0), Time.deltaTime*10);
			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(0, 0), Time.deltaTime*10);
		}

		if(time==0)
		{

			gameover.transform.position=Vector2.Lerp(gameover.transform.position, new Vector2(0, 0), Time.deltaTime*10);
			game.transform.position=Vector2.Lerp(game.transform.position, new Vector2(20f, 0), Time.deltaTime*10);
			commongui.transform.position=Vector2.Lerp(game.transform.position, new Vector2(20f, 0), Time.deltaTime*10);
			//Invoke("farzi2", 1);
			PlayerPrefs.SetInt("gameover", 1);
		}


	}

	//farzi is hindi word...means useless but this one is used
	void farzi()
	{
		Time.timeScale=0;
	}

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

}
