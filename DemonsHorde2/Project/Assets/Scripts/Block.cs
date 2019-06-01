using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public int BlockToughness = 1;//the number of hits it takes to destroy the block

	private int blockHitCount = 0;//the number of times this block has been hit

	void OnTriggerEnter () {
//        BreakoutGame.SP.HitBlock();
//        Destroy(gameObject);
	}

	void OnCollisionExit(Collision collisionInfo ) {
		//Add X velocity..otherwise the ball would only go up&down
		Rigidbody rigid = collisionInfo.rigidbody;
		float xDistance = rigid.position.x - transform.position.x;
		rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);

		//check to see if the block is destroyed
		if(blockHitCount >= BlockToughness)
		{
			//destroy the collider in case we are going to show some animation
			Destroy(gameObject);

			//play die animation
			gameObject.transform.parent.gameObject.animation["die"].layer = 1;
			gameObject.transform.parent.gameObject.animation.Play("die");

			blockHitCount++;
			BreakoutGame.SP.HitBlock(rigid.position);

			//destroy the parent game object
			Destroy(gameObject.transform.parent.gameObject,2);
		}
	}
}
