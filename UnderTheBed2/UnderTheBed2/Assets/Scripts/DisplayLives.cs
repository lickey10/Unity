using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour {
    Text txtLives;

    // Use this for initialization
    void Start()
    {
        txtLives = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txtLives.text = gamestate.Instance.getLives().ToString();
    }
}
