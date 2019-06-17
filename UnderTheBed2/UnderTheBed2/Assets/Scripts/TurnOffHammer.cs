using UnityEngine;
using System.Collections;

public class TurnOffHammer : MonoBehaviour {
    public GameObject Hammer;
    Animation hammerSwing;

	// Use this for initialization
	void Start () {
        hammerSwing = Hammer.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            hammerSwing.Stop();
        }
    }
}
