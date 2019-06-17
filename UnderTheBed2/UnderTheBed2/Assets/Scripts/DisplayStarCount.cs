using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStarCount : MonoBehaviour {
    Text txtStarCount;

	// Use this for initialization
	void Start () {
        txtStarCount = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        txtStarCount.text = gamestate.Instance.StarCount.ToString();
    }
}
