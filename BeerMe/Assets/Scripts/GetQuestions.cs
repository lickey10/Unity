using UnityEngine;
using System.Collections;
using System.Xml;

public class GetQuestions : MonoBehaviour {
	public UILabel UiLabel;
	private UILabel UiLabelAnswer1;
	private UILabel UiLabelAnswer2;
	private AnswerButtonInfo answerButtonInfo1;
	private AnswerButtonInfo answerButtonInfo2;
	public GameObject AnswerButton1;
	public GameObject AnswerButton2;
	public GameObject StartOverButton;
	public GameObject StartButton;

	private static readonly string dataBasePath = "Assets\\Data" + "\\";

	// Use this for initialization
	void Start () {
		UiLabelAnswer1 = AnswerButton1.GetComponentInChildren<UILabel> ();
		answerButtonInfo1 = AnswerButton1.GetComponentInChildren<AnswerButtonInfo> ();
		UiLabelAnswer2 = AnswerButton2.GetComponentInChildren<UILabel> ();
		answerButtonInfo2 = AnswerButton2.GetComponentInChildren<AnswerButtonInfo> ();

		StartButton.SetActive (true);
		StartOverButton.SetActive (false);
		AnswerButton1.SetActive (false);
		AnswerButton2.SetActive (false);

		//Invoke ("SoundEffectsHelper.Instance.MakeHaveAnotherSound", 2);
		//SoundEffectsHelper.Instance.MakeHaveAnotherSound ();
		//StartOver();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartOver()
	{
		StartButton.SetActive (false);
		StartOverButton.SetActive (false);
		AnswerButton1.SetActive (true);
		AnswerButton2.SetActive (true);

		gamestate.Instance.OutcomeID = -1;
		gamestate.Instance.CurrentQuestionID = 507;

		DisplayCurrentQuestion ();
	}

	public void DisplayCurrentQuestion()
	{
		UiLabel.text = GetCurrentQuestion ();
	}

	public string GetCurrentQuestion()
	{
		XmlReader reader = XmlReader.Create(dataBasePath +"questions.xml");
		//while there is data read it
		while(reader.Read())
		{
			//when you find a ROW tag with the correct questionID - get the question
			if(reader.IsStartElement("ROW") && reader.GetAttribute("ID") == gamestate.Instance.CurrentQuestionID.ToString())
			{
				GetAnswers ();
				return reader.GetAttribute("question");
			}
		}

		return "NA";
	}

	public void GetAnswers()
	{
		int answerCounter = 0;
		XmlReader reader = XmlReader.Create(dataBasePath +"answers.xml");
		//while there is data read it
		while(reader.Read())
		{
			//when you find a ROW tag with the correct questionID - get the question and answers
			if(reader.IsStartElement("ROW") && reader.GetAttribute("answer").Length > 0 && reader.GetAttribute("questionID") == gamestate.Instance.CurrentQuestionID.ToString())
			{
				if(answerCounter == 0)//this is the first answer
				{
					answerButtonInfo1.Answer = reader.GetAttribute("answer");
					UiLabelAnswer1.text = answerButtonInfo1.Answer;
					if(reader.GetAttribute("outcomesID").Length > 0)
						answerButtonInfo1.OutcomeID = int.Parse(reader.GetAttribute("outcomesID"));
					else
						answerButtonInfo1.OutcomeID = -1;

					if(reader.GetAttribute("nextQuestionID").Length > 0)
						answerButtonInfo1.NextQuestionID = int.Parse(reader.GetAttribute("nextQuestionID"));
					else
						answerButtonInfo1.NextQuestionID = -1;
				}
				else
				{
					answerButtonInfo2.Answer = reader.GetAttribute("answer");
					UiLabelAnswer2.text = answerButtonInfo2.Answer;
					if(reader.GetAttribute("outcomesID").Length > 0)
						answerButtonInfo2.OutcomeID = int.Parse(reader.GetAttribute("outcomesID"));
					else
						answerButtonInfo2.OutcomeID = -1;

					if(reader.GetAttribute("nextQuestionID").Length > 0)
						answerButtonInfo2.NextQuestionID = int.Parse(reader.GetAttribute("nextQuestionID"));
					else
						answerButtonInfo2.NextQuestionID = -1;

					break;
				}

				answerCounter++;
			}
		}
	}

	public void DisplayOutcome()
	{
		XmlReader reader = XmlReader.Create(dataBasePath +"outcomes.xml");
		//while there is data read it
		while(reader.Read())
		{
			//when you find a ROW tag with the correct questionID - get the question and answers
			if(reader.IsStartElement("ROW") && reader.GetAttribute("ID") == gamestate.Instance.OutcomeID.ToString())
			{
				UiLabel.text = reader.GetAttribute("Outcomes");

				AnswerButton1.SetActive(false);
				AnswerButton2.SetActive(false);
				StartOverButton.SetActive(true);

				break;
			}
		}
	}
}
