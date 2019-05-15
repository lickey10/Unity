using UnityEngine;
using System.Collections;

public class PopupScoreScript : MonoBehaviour 
{
	ScoreMgmt SMG;
	GameObject GameManager;

	void Start () 
	{
		GameManager = GameObject.FindGameObjectWithTag ("GM");
		SMG = GameManager.GetComponent<ScoreMgmt> ();

		gameObject.GetComponent<MeshRenderer> ().sortingOrder = 10;
		gameObject.GetComponent<TextMesh> ().text = "+" + SMG.Score.ToString ();
	}

	void Update () 
	{
		if(transform.position.y < 2)
		{
			transform.Translate (Vector2.up * 0.1f);
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
