using UnityEngine;
using System.Collections;

public class BonusScript : MonoBehaviour {

    public int BonusScore = 1000;

	// Use this for initialization
	void Start () {
        gamestate.Instance.SetScore(gamestate.Instance.getScore() + BonusScore);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
