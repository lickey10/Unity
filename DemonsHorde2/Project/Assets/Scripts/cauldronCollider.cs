using UnityEngine;
using System.Collections;

public class cauldronCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collisionInfo ) {
		Destroy(gameObject.collider);

		lightsOut.Instance.TurnOffALight();

		SoundEffectsHelper.Instance.MakeCauldronHitSound();

		gamestate.Instance.GUIMessage("Lights out!   "+(gamestate.Instance.currentLevelTreasureMultiplier++).ToString() +"X Treasure!");
	}
}
