using UnityEngine;
using System.Collections;

public class lightTorch : MonoBehaviour {

	public int Points = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {
		//make sure the torch is off before adding points
		if(gameObject.transform.parent.GetComponent<Torchelight>().IntensityLight != gameObject.transform.parent.GetComponent<Torchelight>().MaxLightIntensity)
		{
			gameObject.transform.parent.GetComponent<Torchelight>().IntensityLight = gameObject.transform.parent.GetComponent<Torchelight>().MaxLightIntensity;
			gamestate.Instance.AddToScore(Points);
			gamestate.Instance.currentLevelTreasureMultiplier += 1;

			gamestate.Instance.GUIMessage((gamestate.Instance.currentLevelTreasureMultiplier).ToString() +"X Treasure!");
		}
	}
}
