using UnityEngine;
using System.Collections;

public class textcontrol : MonoBehaviour {


	string txt1=null;
	int i=0;

	void Awake()
	{
		//levels value is assigned in a variable
		//it will return 0 for text, 2 for color and 3 for mix mode
		i=PlayerPrefs.GetInt("level");
	}

	void Start () 
	{

		//level 3 contains text and color buttons on left and right of the screen
		//other levels will have either text or color buttons
		if(PlayerPrefs.GetInt("level")==3)
		{
			//first and last character of the name of the game object is used to compare it with color buttons shown on screen
			char c=gameObject.name[0];
			char c1=gameObject.name[2];
			txt1=c.ToString()+c1.ToString();
			//Debug.Log(txt1);
		}
		else
		{
			char c=gameObject.name[i];
			txt1=c.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		//scaling of gameobject is done according to its position
		if(transform.position.y==2)
		{
			transform.localScale=new Vector2(.4f, .4f);
		}

		if(transform.position.y==0)
		{
			transform.localScale=new Vector2(.6f, .6f);
		}

		//if the object comes in the line of arrows(pointer) shown in screen it will send a message to textinstantiate script on Main Camera
		if(transform.position.y==-2)
		{
			transform.localScale=new Vector2(1,1);
			//here the the value of txt is passed via sendmessage function
			GameObject.Find("Main Camera").SendMessage("pickme", txt1);

		}


		//if the timetrial mode is selected then the game will continue else in infinity mode if text crosses the pointer game is over
		if(PlayerPrefs.GetInt("timetrial")==1)
		{

		}
		else
		{
			if(transform.position.y==-4)
			{
				//Time.timeScale=0;
				PlayerPrefs.SetInt("gameover", 1);
				PlayerPrefs.Save();

			}
		}

		//finally after it crosses the final position game object is destroyed
		if(transform.position.y<-4f)
		{
			Destroy(gameObject);
		}

	}
}
