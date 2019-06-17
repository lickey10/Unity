using UnityEngine;
using System.Collections;

public class LogoScreenHandler : MonoBehaviour {

    public GameObject ParagraphText;
    public GameObject MoreButton;
    public GameObject BackButton;

    private string originalParagraphText = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowMoreInfo()
    {
        originalParagraphText = GameObject.FindGameObjectWithTag("ParagraphText").GetComponent<UnityEngine.UI.Text>().text;
        GameObject.FindGameObjectWithTag("ParagraphText").GetComponent<UnityEngine.UI.Text>().text = "Finish as fast as you can and advance right away or load all you can and get a higher score.  \r\nYou can advance as soon as the advance button pops up in the upper right corner. \r\nHave Fun!";

        MoreButton.SetActive(false);
        BackButton.SetActive(true);
    }

    public void GoBack()
    {
        GameObject.FindGameObjectWithTag("ParagraphText").GetComponent<UnityEngine.UI.Text>().text = originalParagraphText;
        MoreButton.SetActive(true);
        BackButton.SetActive(false);
    }

    public void StartGame()
    {

    }
}
