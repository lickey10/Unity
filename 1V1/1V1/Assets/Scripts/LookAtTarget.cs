using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {
    public GameObject Target;

	// Use this for initialization
	void Start () {
        Target = GameObject.FindGameObjectWithTag("Fighter");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Target.transform);
	}
}
