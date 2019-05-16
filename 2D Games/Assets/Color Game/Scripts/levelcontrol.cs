using UnityEngine;
using System.Collections;

public class levelcontrol : MonoBehaviour {


	//here touch buttons are default buttons
	//level menu ui touch buttons
	public GameObject[] level;

	//level menu ui touch buttons
	public Sprite[] level_n;

	//level menu ui normal buttons
	public Sprite[] level_t;

	void Awake()
	{
		Time.timeScale=1;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {

			//touch control through raycasting on sprite(a different concept from input.touch or gui.hitest)
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

			if(PlayerPrefs.GetInt("arcade")==0)
			{
				if (hit.collider.name=="text_mode") {
					GetComponent<AudioSource>().Play();
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[0];
					PlayerPrefs.SetInt("level", 0);
					PlayerPrefs.Save();
					Application.LoadLevel("TimeTrial");
				}
				if (hit.collider.name=="color_mode") {
					GetComponent<AudioSource>().Play();
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[1];
					PlayerPrefs.SetInt("level", 2);
					PlayerPrefs.Save();
					Application.LoadLevel("TimeTrial");
				}
				if (hit.collider.name=="mix_mode") {
					GetComponent<AudioSource>().Play();
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[2];
					PlayerPrefs.SetInt("level",3 );
					PlayerPrefs.Save();
					Application.LoadLevel("TimeTrial");
				}
			}
			else
			{
				if (hit.collider.name=="text_mode") {
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[0];
					PlayerPrefs.SetInt("level", 0);
					PlayerPrefs.Save();
					Application.LoadLevel("Arcade");
				}
				if (hit.collider.name=="color_mode") {
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[1];
					PlayerPrefs.SetInt("level", 2);
					PlayerPrefs.Save();
					Application.LoadLevel("Arcade");
				}
				if (hit.collider.name=="mix_mode") {
					hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite=level_t[2];
					PlayerPrefs.SetInt("level", 3);
					PlayerPrefs.Save();
					Application.LoadLevel("Arcade");
				}
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			for(int i=0; i<level.Length;i++)
			{
				level[i].GetComponent<SpriteRenderer>().sprite=level_n[i];
			}
		}

	}
}

