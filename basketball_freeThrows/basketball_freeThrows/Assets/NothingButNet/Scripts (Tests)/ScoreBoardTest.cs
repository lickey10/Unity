using UnityEngine;
using System.Collections;

public class ScoreBoardTest : MonoBehaviour {
	
	public ScoreBoard scoreboard; 
	
	// Use this for initialization
	void Start () {
		scoreboard.SetTime( "60" ); 
		scoreboard.SetPoints( "100" ); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
