using UnityEngine;
using System.Collections;

public class JSFVisualManager : MonoBehaviour {

	[HideInInspector] public JSFGameManager gm {get{return JSFUtils.gm;}}
	public GameObject defaultSquareBackPanel;
	public GameObject defaultAltSquareBackPanel;
	public GameObject defaultHexBackPanel;
	public bool displayScoreHUD = false;
	public GameObject scoreHUD;
	public TextMesh scoreTxtObject; // reference to the text score counter
    public bool displayHighScoreHUD = false;
    public GameObject highScoreHUD;
    public TextMesh highScoreTxtObject; // reference to the text high score counter
    public TextMesh movesTxtObject; // reference to the text moves counter
	public GameObject comboTxtObject; // reference to the text combo combo
	public GameObject swipeIndicatorObj; // a gameObject for the swipe indicator
	public GameObject swipeLineObj; // a gameObject for the swipe indicator
    public GameObject ComboTextPopUp;

    private long highScore = 0;
    private string highScoreString = "";
    private int moves = 0;
    private GameObject instantiatedPopup = null;
	
	// Use this for initialization
	void Start () {
		SyncReferences();
		StartCoroutine( GUIUpdate() ); // widgets update
        //PlayerPrefs.SetString("HighScore", "0|0");//reset high score
        highScoreString = PlayerPrefs.GetString("HighScore", "|");
        long.TryParse(highScoreString.Split('|')[0], out highScore);
        int.TryParse(highScoreString.Split('|')[1], out moves);
        highScoreTxtObject.text = "High Score: " + highScore.ToString() + " / " + moves.ToString();
    }

	void SyncReferences(){
		if(comboTxtObject != null){
			gm.comboScript = comboTxtObject.GetComponent<JSFComboPopUp>(); // find and assign the combo script
		}
	}

	IEnumerator GUIUpdate(){
		while(JSFUtils.gm.gameState != JSFGameState.GameOver){ // while is not gameOver... update the GUIs
			txtUpdate();
			yield return new WaitForSeconds(gm.gameUpdateSpeed);
		}
	}

	// to output the score to the text label
	void txtUpdate(){
		if(scoreTxtObject != null){
			scoreTxtObject.text = "Score: "+ gm.score.ToString();

            if (highScoreTxtObject != null)
            {
                if (gm.score > highScore || (gm.score == highScore && gm.moves < moves))// new high score
                {
                    highScore = gm.score;
                    moves = gm.moves;  

                    highScoreTxtObject.text = "High Score: "+ highScore.ToString() +" / "+ moves.ToString();

                    PlayerPrefs.SetString("HighScore",highScore.ToString() +"|"+ moves.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
        
        if (movesTxtObject != null){
			movesTxtObject.text = "Moves: "+ gm.moves.ToString();
		}
	}

    public void DisplayComboPopUp()
    {
        if (ComboTextPopUp)
        {
            TextMesh tm = ComboTextPopUp.GetComponentInChildren<TextMesh>();
            tm.text = "Combo " + gm.currentCombo.ToString();

            if (instantiatedPopup != null)
                Destroy(instantiatedPopup);

            instantiatedPopup = Instantiate(ComboTextPopUp);
        }
    }
}
