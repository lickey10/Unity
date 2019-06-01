using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int Health = 1; //the number of hits it takes to kill this guy
	public int Points = 10; //the number of points this enemy is worth when he is killed
	public int Damage = 1; //the amount of damage this enemy does

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit(Collision collisionInfo ) {
		Rigidbody rigid = collisionInfo.rigidbody;

		//Add X velocity..otherwise the ball would only go up&down

		float xDistance = rigid.position.x - transform.position.x;
		rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);


		SoundEffectsHelper.Instance.MakeEnemyHitSound();

		Health--;

		if(Health <= 0)//they are dead
		{
			gamestate.Instance.AddToScore(Points);
			gamestate.Instance.currentLevelKills++;

			//destroy the collider in case we are going to show some animation
			Destroy(gameObject);
			
			//play die animation
			gameObject.transform.parent.gameObject.animation["die"].layer = 1;
			gameObject.transform.parent.gameObject.animation.Play("die");

			BreakoutGame.SP.HitBlock(rigid.position);
			
			//destroy the parent game object
			Destroy(gameObject.transform.parent.gameObject,2);
		}
		else //attack
		{
			gameObject.transform.parent.gameObject.animation["attack"].layer = 1;
			gameObject.transform.parent.gameObject.animation.Play("attack");
		}

	}
}
