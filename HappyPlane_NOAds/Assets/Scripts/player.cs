using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForce = new Vector2(0, 300);
	public bool DieOnCollision = false;

	void Awake() {

	}
	
	// Update is called once per frame
	void Update ()
	{
		// Jump
		if (Input.GetKeyUp("space") || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
		{
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				
				if (touch.phase == TouchPhase.Began)
				{
					rigidbody2D.velocity = Vector2.zero;
					rigidbody2D.AddForce(jumpForce);
				}
			}
			else
			{
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.AddForce(jumpForce);
			}
		}
		
		// Die by being off screen
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height + 20 || screenPosition.y < 0 - 20)
		{
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			
			SoundEffectsHelper.Instance.MakeExplosionSound();
			
			if(DieOnCollision)
				gamestate.Instance.SetHealth(gamestate.Instance.GetHealth()-1);
			
			if(gamestate.Instance.GetHealth() <= 0)
			{
				this.gameObject.SetActive(false);
				Invoke("Die",2f);	
			}
		}

		if (screenPosition.x > Screen.width)
		{
			//player is off the right of the screen
			//level complete
			Application.LoadLevel("levelComplete");
		}
	}
	
	// Die by collision
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider is PolygonCollider2D)
		{
			SpecialEffectsHelper.Instance.Explosion(transform.position);

			SoundEffectsHelper.Instance.MakeExplosionSound();

			if(DieOnCollision)
				gamestate.Instance.SetHealth(gamestate.Instance.GetHealth() - 1);

			if(gamestate.Instance.GetHealth() <= 0)
			{
				this.gameObject.SetActive(false);
				Invoke("Die",2f);	
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if(coll is BoxCollider2D)
			gamestate.Instance.SetScore(gamestate.Instance.getScore() + 1);
	}

	void Die()
	{
		Destroy(this.gameObject);

		gamestate.Instance.SetLives(gamestate.Instance.getLives() - 1);

		if(gamestate.Instance.getLives() <= 0)
		{
			Application.LoadLevel("gameover");
		}
		else
			Application.LoadLevel(Application.loadedLevel);
	}
}
