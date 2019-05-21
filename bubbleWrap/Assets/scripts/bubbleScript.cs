using UnityEngine;
using System.Collections;

public class bubbleScript : MonoBehaviour {

	public Sprite[] PoppedBubbles;
	private SpriteRenderer spriteRenderer;
	private bool popped = false;
	
	// Use this for initialization
	void Start() 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseUp()
	{
		string test = "";
	}

	void OnMouseOver()
	{
		string test = "";
	}

	void OnMouseEnter()
	{
		if(createBubblesScript.CrazyMode)
			popBubble();
	}

	void OnMouseExit()
	{
		string test = "";
	}

	void OnMouseDown()
	{
		popBubble();
	}

	void popBubble()
	{
		if(!popped)
		{
			//pop bubble
			SoundEffectsHelper.Instance.MakeBubblePoppingSound();
			
			spriteRenderer.sprite = PoppedBubbles[Random.Range(0,PoppedBubbles.Length-1)];

			popped = true;
		}
	}
}
