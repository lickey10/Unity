using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float moveSpeed = 15;
	private bool dragging = false;
		
	void Update () {
        float moveInput = 0;

		if (Input.GetButtonDown("Fire1"))
		{
			dragging = true;
		}
		else if(Input.GetAxis("Horizontal") != 0)
		{
			moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

			transform.position += new Vector3(moveInput, 0, 0);
			
			float max = 14.0f;
			if (transform.position.x <= -max || transform.position.x >= max)
			{
				float xPos = Mathf.Clamp(transform.position.x, -max, max); //Clamp between min -5 and max 5
				transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
			}
		}

		if (Input.GetButtonUp("Fire1"))
		{
			dragging = false;
		}

		if(dragging)
			movePaddle();
	}

	void movePaddle()
	{
		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);


		#if UNITY_EDITOR
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		//for touch device
		#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z));
		curScreenSpace = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z);

		#endif

		var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

		transform.position = new Vector3(curPosition.x,transform.position.y,transform.position.z);
	}

	/// <summary>
	/// Controls the ball after it hits the paddle
	/// </summary>
	/// <param name="collisionInfo">Collision info.</param>
	void OnCollisionExit(Collision collisionInfo ) {
		if(collisionInfo.gameObject.tag == "Player")
		{
			SoundEffectsHelper.Instance.MakePaddleHitSound();

			//Add X velocity..otherwise the ball would only go up&down
	        Rigidbody rigid = collisionInfo.rigidbody;
	        float xDistance = rigid.position.x - transform.position.x;

			if(xDistance > -1.5f)
				xDistance = -1.5f;

	        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y*10f, rigid.velocity.z);
		}
    }
}
