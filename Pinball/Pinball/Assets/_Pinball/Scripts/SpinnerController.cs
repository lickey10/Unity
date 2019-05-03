using SgLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinnerController : MonoBehaviour {

    public int Points = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(spin(50));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //spin spinner
        var vel = other.GetComponent<Rigidbody2D>().velocity;
        var speed = Vector2.SqrMagnitude(vel);

        StartCoroutine(spin(speed));
        //StartCoroutine(spin(500));
    }

    IEnumerator spin(float speed)
    {
        //number of times to spin based on speed of the ball
        int spins = Mathf.RoundToInt(speed/40);

        //make sure we spin at least once
        if (spins < 1)
            spins = 1;

        float delay = .05f; //how long to wait between spins
        float delayIncrease = .9f / spins; //used to slow down spinner over time

        for(int x = 0; x < spins; x++)
        {
            transform.localScale = transform.localScale / 3;
            yield return new WaitForSeconds(delay);

            SoundManager.Instance.PlaySound(SoundManager.Instance.tick);
            ScoreManager.Instance.AddScore(Points);

            transform.localScale = transform.localScale * 3;
            yield return new WaitForSeconds(delay);

            //slow the spin over time
            delay += delayIncrease;

            //stop spinner if going too slow
            if (delay > .7f)
                break;
        }
    }
}
