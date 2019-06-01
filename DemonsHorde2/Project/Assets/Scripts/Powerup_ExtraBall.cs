using UnityEngine;
using System.Collections;

public class Powerup_ExtraBall : MonoBehaviour {

	GameObject[] currentBalls;
	bool extraBallCreated = false;

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
		Invoke("stopPowerup",1.5f);
	}

	void startPowerup()
	{
		SoundEffectsHelper.Instance.MakeMultiBallSound();

		BreakoutGame.SP.SpawnBall();
		extraBallCreated = true;

		gamestate.Instance.GUIMessage("Extra Ball!");

//		GUIMessage gUIMessage = Instantiate(GUIMessage, new Vector3(0, 0, 0), Quaternion.identity);
//		gUIMessage.DisplayText("Extra Ball!");
	}

	void stopPowerup()
	{
		SoundEffectsHelper.Instance.StopMultiBallSound();

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
