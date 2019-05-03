using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

    public GameObject Barrier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Barrier.activeSelf)
        {
            Barrier.SetActive(false);

            StartCoroutine(CloseGate());
        }
    }

    IEnumerator CloseGate()
    {
        if (!Barrier.activeSelf)
        {
            yield return new WaitForSeconds(1);

            Barrier.SetActive(true);
        }
    }
}
