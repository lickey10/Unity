using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
	public int CarScore = 0;
	public int BalloonBonus = 0;//number of balloons awarded for this car


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Balloon")
		{
			//honk horn
			SoundEffectsHelper.Instance.MakeHornSound();

			//assign rewards for hitting car (points, power ups, bonuses, etc)
			gamestate.Instance.ScoreCurrentLevel = gamestate.Instance.ScoreCurrentLevel + CarScore * gamestate.Instance.getActiveLevel();
			gamestate.Instance.SetScore(gamestate.Instance.getScore() + CarScore * gamestate.Instance.getActiveLevel());
			gamestate.Instance.SetLives(gamestate.Instance.getLives() + BalloonBonus);
		}
	}
}
