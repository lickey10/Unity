using UnityEngine;
using System.Collections;

public class AnswerButtonInfo : MonoBehaviour {
	public int OutcomeID = -1;
	public int NextQuestionID = -1;
	public string Answer = "";
	public GetQuestions getQuestions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveAnswer()
	{
		if (NextQuestionID > 0) {
				gamestate.Instance.CurrentQuestionID = NextQuestionID;
				getQuestions.DisplayCurrentQuestion ();
		} else if (OutcomeID > 0) { //it's an outcome 
				gamestate.Instance.OutcomeID = OutcomeID;
				gamestate.Instance.CurrentQuestionID = -1;
				getQuestions.DisplayOutcome ();
		} 
		else //start genie 
		{ 
			getQuestions.StartOver ();
		}
	}
}
