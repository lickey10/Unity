using UnityEngine;
using System.Collections;

public class SceneManager1 : MonoBehaviour {
    int numberOfPieces = 0;
    int numberOfPiecesLoaded = 0;
    public GameObject[] Explosives;
    public GameObject ResetLocation;
    public GameObject AdvanceButton;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void BuildingExploded()
    {
        numberOfPieces = GameObject.FindGameObjectsWithTag("Explosion").Length;
    }

    public void LoadedPieces(int numberOfPieces)
    {
        numberOfPiecesLoaded += numberOfPieces;
        gamestate.Instance.SetScore(gamestate.Instance.getScore() + 10);//add 10 points for each piece

        if(numberOfPiecesLoaded == 10)//apply a bonus
        {
            AdvanceButton.SetActive(true);
            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 100);//add 100 points for each 10 pieces
        }
        else if (numberOfPiecesLoaded == 20)//apply a bonus
        {

            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 100);//add 100 points for each 10 pieces
        }
        else if (numberOfPiecesLoaded == 30)//apply a bonus
        {

            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 100);//add 100 points for each 10 pieces
        }
        else if (numberOfPiecesLoaded == 40)//apply a bonus
        {

            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 100);//add 100 points for each 10 pieces
        }
        if (numberOfPiecesLoaded == 50)//apply bonus
        {
            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 100);//add 100 points for each 10 pieces
        }
        if (numberOfPiecesLoaded == 100)//win level
        {
            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 10000);//add 100 points for each 10 pieces
        }
    }

    public void BlowUp()
    {
        foreach(GameObject explosions in Explosives)
        {
            explosions.SetActive(true);
        }

        Destroy(GameObject.FindGameObjectWithTag("PropToExplode"));
        Destroy(GameObject.FindGameObjectWithTag("BlowUpButton"));
    }

    public void Reset()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Player.transform.position = ResetLocation.transform.position;
        Player.transform.rotation = ResetLocation.transform.rotation;
    }

    public void AdvanceLevel()
    {
        if(gamestate.Instance.getActiveLevel() < gamestate.Instance.GetNumberOfLevels())
            UnityEngine.SceneManagement.SceneManager.LoadScene("levelComplete");
        else //game is over
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;

        //display score
        GUI.Label(new Rect(5, 5 + gamestate.Instance.GetBannerHeight(), 100, 50), "Score: " + gamestate.Instance.getScore(), labelStyle);

        //display lives
        labelStyle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(Screen.width - 110, 5 + gamestate.Instance.GetBannerHeight(), 100, 50), "Pieces: " + numberOfPiecesLoaded.ToString() + "/"+ numberOfPieces.ToString(), labelStyle);

        //display high score
        GUI.Label(new Rect((Screen.width - 100) / 2, 5 + gamestate.Instance.GetBannerHeight(), 100, 50), "Hi Score: " + gamestate.Instance.getHighScore(), labelStyle);

        GUI.color = Color.yellow;

        //display score shadow
        //GUI.Label(new Rect(6, 6 + gamestate.Instance.GetBannerHeight(), 100, 50), "Score: " + gamestate.Instance.getScore(), labelStyle);

        ////display pieces shadow
        //labelStyle.alignment = TextAnchor.UpperRight;
        //GUI.Label(new Rect(Screen.width - 110+6, 6 + gamestate.Instance.GetBannerHeight(), 100, 50), "Pieces: " + numberOfPiecesLoaded.ToString() + "/" + numberOfPieces.ToString(), labelStyle);

        ////display high score shadow
        //GUI.Label(new Rect((Screen.width - 100) / 2+6, 6 + gamestate.Instance.GetBannerHeight(), 100, 50), "Hi Score: " + gamestate.Instance.getHighScore(), labelStyle);
    }
}
