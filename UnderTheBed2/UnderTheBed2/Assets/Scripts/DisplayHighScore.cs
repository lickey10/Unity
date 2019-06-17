using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour {
    Text txtHighScore;

    // Use this for initialization
    void Start()
    {
        txtHighScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txtHighScore.text = gamestate.Instance.getHighScore().ToString();
    }
}
