using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
    Text txtScore;

    // Use this for initialization
    void Start()
    {
        txtScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txtScore.text = gamestate.Instance.getScore().ToString();
    }
}
