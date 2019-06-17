using UnityEngine;
using System.Collections;

public class bubbleScript : MonoBehaviour {
	public AudioClip[] bubblePoppingSounds;

	// Use this for initialization
	void Start() 
	{

	}

	void OnMouseUp()
	{
		//string test = "";
	}

	void OnMouseOver()
	{
		//string test = "";
	}

	void OnMouseEnter()
	{
		popBubble();
	}

	void OnMouseExit()
	{
		//string test = "";
	}

	void OnMouseDown()
	{
		popBubble();
	}

	void popBubble()
	{
		if (this.transform.parent != null)
			DestroyObject (this.transform.parent.gameObject);
		else
			DestroyObject (this.gameObject);

			//pop bubble
			SoundEffectsHelper.Instance.MakeBubblePoppingSound();
	}
}
