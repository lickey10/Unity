using UnityEngine;
using System.Collections;

public class Powerup_Dancing : MonoBehaviour {

	GameObject[] currentBlocks;

	void Awake () {
		//drop powerup
		rigidbody.velocity = new Vector3(0, 0, -5);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// this will trigger this powerups abilities
	/// </summary>
	void activatePowerup()
	{
		Invoke("startPowerup",0);
		Invoke("stopPowerup",2);
	}

	void startPowerup()
	{
		currentBlocks = GameObject.FindGameObjectsWithTag("Pickup");
		SoundEffectsHelper.Instance.MakeGuitarSound();

//		GUIMessage message = new GUIMessage();
//		message.DisplayText("Now we're dancing! 2X Points!!");

		gamestate.Instance.GUIMessage("Now we're dancing! 2X Points!!");

		//iterate blocks and start dancing
		foreach(GameObject block in currentBlocks)
		{
			//play dance animation
			block.gameObject.animation["dance"].layer = 1;
			block.gameObject.animation.Play("dance");

//			(EnemyScript)(block.transform.FindChild("collider").GetComponent<EnemyScript>()).Points = ((EnemyScript)block.transform.FindChild("collider").GetComponent<EnemyScript>()).Points * 2;


				try {
					EnemyScript eScript = block.transform.FindChild("collider").GetComponent<EnemyScript>();//.Points = block.transform.GetComponentInChildren<enemyScript>().Points*2;
					eScript.Points = eScript.Points * 2;
				} catch (System.Exception ex) {
					string exceptionMessage = ex.Message;
				}

		}
	}

	void stopPowerup()
	{
		SoundEffectsHelper.Instance.StopGuitarSound();

		//iterate blocks and start dancing
		foreach(GameObject block in currentBlocks)
		{
			//play dance animation
			block.gameObject.animation["idle"].layer = 1;
			block.gameObject.animation.Play("idle");
			//block.GetComponent<enemyScript>().Points = block.GetComponent<enemyScript>().Points/2;
//			EnemyScript eScript = block.GetComponentInChildren<EnemyScript>();//.Points = block.transform.GetComponentInChildren<enemyScript>().Points*2;
//			eScript.Points = eScript.Points / 2;

			try {
				EnemyScript eScript = block.transform.FindChild("collider").GetComponent<EnemyScript>();//.Points = block.transform.GetComponentInChildren<enemyScript>().Points*2;
				eScript.Points = eScript.Points / 2;
			} catch (System.Exception ex) {
				string exceptionMessage = ex.Message;
			}
		}

		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider colliderInfo)
	{
		if(colliderInfo.gameObject.name == "Paddle")
		{
			activatePowerup();
			Destroy(this.gameObject);
		}
	}

}
