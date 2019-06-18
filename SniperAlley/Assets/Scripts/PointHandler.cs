using UnityEngine;
using System.Collections;

public class PointHandler : MonoBehaviour {
    public float Score = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ApplyDamagePlayer(float damage)
    {
        var PointScript = gameObject.AddComponent<Point>();
        PointScript.Point = Score * damage;

        print("found it");
    }
}
