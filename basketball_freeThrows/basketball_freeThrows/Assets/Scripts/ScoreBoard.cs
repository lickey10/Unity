using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {		
	
	/// <summary>
	/// reference to the associated Points TextMesh 
	/// </summary>
	public TextMesh pointsTextMesh; 
	
	/// <summary>
	/// reference to the associated Time TextMesh
	/// </summary>
	public TextMesh timeRemainingTextMesh; 
			
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetTime( string timeRemaining ){
		timeRemainingTextMesh.text = timeRemaining; 	
	}
	
	public void SetPoints( string points ){
		pointsTextMesh.text = points; 	
	}	
}
