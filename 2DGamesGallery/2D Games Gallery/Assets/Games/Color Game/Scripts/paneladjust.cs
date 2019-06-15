using UnityEngine;
using System.Collections;

public class paneladjust : MonoBehaviour {
		
	public GUIText scoretext, score;

	void  Awake() 
	{

		if(Screen.width==2560 && Screen.height==1600)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==2560 && Screen.height==1536)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==2048 && Screen.height==1536)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1920 && Screen.height==1200)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1920 && Screen.height==1152)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1536 && Screen.height==1152)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==640 && Screen.height==480)
		{
			scoretext.fontSize=50;
			score.fontSize=50;
		}
		if(Screen.width==1280 && Screen.height==768)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1024 && Screen.height==768)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1280 && Screen.height==800)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==432 && Screen.height==240)
		{
			scoretext.fontSize=50;
			score.fontSize=50;
		}
		if(Screen.width==400 && Screen.height==240)
		{
			scoretext.fontSize=50;
			score.fontSize=50;
		}
		if(Screen.width==320 && Screen.height==240)
		{
			scoretext.fontSize=40;
			score.fontSize=40;
		}
		if(Screen.width==1024 && Screen.height==600)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==854 && Screen.height==480)
		{
			scoretext.fontSize=60;
			score.fontSize=60;
		}
		if(Screen.width==800 && Screen.height==480)
		{
			scoretext.fontSize=60;
			score.fontSize=60;
		}
		if(Screen.width==480 && Screen.height==320)
		{
			scoretext.fontSize=50;
			score.fontSize=50;
		}
		if(Screen.width==1824 && Screen.height==1200)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1400 && Screen.height==900)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1920 && Screen.height==1080)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==960 && Screen.height==540)
		{
			scoretext.fontSize=60;
			score.fontSize=60;
		}
		if(Screen.width==1200 && Screen.height==768)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
		if(Screen.width==1280 && Screen.height==720)
		{
			scoretext.fontSize=100;
			score.fontSize=100;
		}
	}
}
