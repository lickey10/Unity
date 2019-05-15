using UnityEngine;
using System.Collections;

public class TouchHandlerScript : MonoBehaviour 
{
	[SerializeField]
	Sprite PurpleSprite; 
	[SerializeField]
	Sprite GreySprite;

	[SerializeField]
	GameObject TouchEffect;

	[SerializeField]
	GameObject Clock;

	[SerializeField]
	int Row;
	[SerializeField]
	int Column;

	public bool IsClicked;
	GameObject GameManager;
	GameObject DataManager;
	CheckForMatchScript CMS;
	ThemeChangeScript CGS;
	ClockScript CSK;

	void Start () 
	{
		GameManager = GameObject.FindGameObjectWithTag ("GM");
		CMS = GameManager.GetComponent<CheckForMatchScript> ();
		DataManager = GameObject.FindGameObjectWithTag ("DM");
		CGS = DataManager.GetComponent<ThemeChangeScript> ();
		CSK = Clock.GetComponent<ClockScript> ();
	}

	void OnMouseDown()
	{
		if (CSK.AllowTouch) 
		{
			if (!IsClicked) 
			{
				gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R2, CGS.G2, CGS.B2, 255);
				//gameObject.GetComponent<SpriteRenderer> ().sprite = PurpleSprite;
				Instantiate (TouchEffect, transform.position, Quaternion.Euler (0.0f, 0.0f, 45.0f));
				CMS.PlayAudio ();
				CMS.SetGridValue (Row, Column, true);
			} 
			else 
			{
				gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (CGS.R1, CGS.G1, CGS.B1, 255);
				//gameObject.GetComponent<SpriteRenderer> ().sprite = GreySprite;
				Instantiate (TouchEffect, transform.position, Quaternion.Euler (0.0f, 0.0f, 45.0f));

				CMS.SetGridValue (Row, Column, false);
			}
			IsClicked = !IsClicked;
		}
	}
}
