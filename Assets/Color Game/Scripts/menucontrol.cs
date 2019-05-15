using UnityEngine;
using System.Collections;

public class menucontrol : MonoBehaviour {


	//drag and drop menu sprites in this public gameobject
	public GameObject[] mainmenu;

	//help and level heading
	public GUIText mode, mode_h;
	float newtime, starttime;
	bool time=false;

	void Awake()
	{
		Time.timeScale=1;
		PlayerPrefs.SetInt("arcade", -1);
		PlayerPrefs.Save();
	}
	// Update is called once per frame
	void Update () 
	{


		//on escape quit code(in mobile back button)
		if(mainmenu[0].transform.position.x>-1 && mainmenu[0].transform.position.x<1)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		if (Input.GetMouseButtonDown(0)) {

			//touch control through raycasting on sprite(a different concept from input.touch or gui.hitest)
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

			if(hit.collider.name=="back_h")
			{
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetInt("pos", 1);
				PlayerPrefs.Save();
				fun ();
			}

			if(hit.collider.name=="timed")
			{
				GetComponent<AudioSource>().Play();
				fun();
				PlayerPrefs.SetInt("pos", 0);
				PlayerPrefs.SetInt("arcade", 0);
				PlayerPrefs.Save();
				mode.text="Time Trial";
			}

			if(hit.collider.name=="endless")
			{
				GetComponent<AudioSource>().Play();
				fun();
				PlayerPrefs.SetInt("pos", 0);
				PlayerPrefs.SetInt("arcade", 1);
				PlayerPrefs.Save();
				mode.text="Endless";
			}

			if(hit.collider.name=="help")
			{
				GetComponent<AudioSource>().Play();
				fun();
				PlayerPrefs.SetInt("pos", 2);
				PlayerPrefs.Save();
				mode_h.text="Help";
			}

			if(hit.collider.name=="back")
			{
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetInt("pos", 1);
				PlayerPrefs.Save();
				fun ();
			}


		}



		//animating the each menu by transitioning through lerp
		if(time)
		{
			newtime+=Time.deltaTime;
			if(PlayerPrefs.GetInt("pos")==0)
			{
				mainmenu[0].transform.position=Vector2.Lerp(mainmenu[0].transform.position, new Vector2(-20f, 0), Time.deltaTime*5);
				mainmenu[1].transform.position=Vector2.Lerp(mainmenu[1].transform.position, new Vector2(0, 0), Time.deltaTime*5);
				mainmenu[2].transform.position=Vector2.Lerp(mainmenu[2].transform.position, new Vector2(-40f, 0), Time.deltaTime*5);
			}
			else if(PlayerPrefs.GetInt("pos")==1)
			{
				mainmenu[0].transform.position=Vector2.Lerp(mainmenu[0].transform.position, new Vector2(0, 0), Time.deltaTime*5);
				mainmenu[1].transform.position=Vector2.Lerp(mainmenu[1].transform.position, new Vector2(20f, 0), Time.deltaTime*5);
				mainmenu[2].transform.position=Vector2.Lerp(mainmenu[2].transform.position, new Vector2(-20f, 0), Time.deltaTime*5);
			}
			else if(PlayerPrefs.GetInt("pos")==2)
			{
				mainmenu[0].transform.position=Vector2.Lerp(mainmenu[0].transform.position, new Vector2(20f, 0), Time.deltaTime*5);
				mainmenu[1].transform.position=Vector2.Lerp(mainmenu[1].transform.position, new Vector2(40f, 0), Time.deltaTime*5);
				mainmenu[2].transform.position=Vector2.Lerp(mainmenu[2].transform.position, new Vector2(0, 0), Time.deltaTime*5);
			}
			if(newtime>2)
			{
				time=false;
			}
		}
	}

	void fun()
	{
		newtime=0;
		time=true;
	}

}
