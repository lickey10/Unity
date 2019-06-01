using UnityEngine;
using System.Collections;

public class treasure : MonoBehaviour {

	public int Points = 1; //the number of points this treasure is worth
	public int Coins = 1; //the number of coins this treasure is worth
	public GameObject TheTreasurePopup;
	public int OffsetY = 2;
	public int OffsetX = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit(Collision collisionInfo ) {
		//Add X velocity..otherwise the ball would only go up&down
		Rigidbody rigid = collisionInfo.rigidbody;
		float xDistance = rigid.position.x - transform.position.x;
		rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);

		//check to see if the block is destroyed
		if(gamestate.Instance.GotKey)
		{
			//destroy the collider in case we are going to show some animation
			//Destroy(gameObject);
			
			//play open animation
			gameObject.animation["ChestAnim"].layer = 1;
			gameObject.animation.Play("ChestAnim");

			gamestate.Instance.GotKey = false;

			gamestate.Instance.Coins += Coins * gamestate.Instance.currentLevelTreasureMultiplier;
			gamestate.Instance.currentLevelCoins += Coins * gamestate.Instance.currentLevelTreasureMultiplier;
			gamestate.Instance.AddToScore(Points * gamestate.Instance.currentLevelTreasureMultiplier);

			Invoke ("showTreasure",2.5f);
		}
	}

	void showTreasure()
	{
		Vector3 pos = gameObject.transform.position;
		pos.y += OffsetY;
		pos.x += OffsetX;

		Instantiate(TheTreasurePopup, pos , Quaternion.identity);

		SoundEffectsHelper.Instance.MakeTreasureSound();

		gamestate.Instance.GUIMessage("+"+ Coins +" Coins!");
	}
}
