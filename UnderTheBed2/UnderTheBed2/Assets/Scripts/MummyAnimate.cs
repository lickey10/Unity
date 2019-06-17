using UnityEngine;
using System.Collections;

public class MummyAnimate : MonoBehaviour {
    private AudioSource mAudioSource = null;
    
    private float nextJump;

	// Use this for initialization
	void Start () {
        mAudioSource = GetComponent< AudioSource >();
    }
	
	// Update is called once per frame
	void Update () {
        //if (!GetComponent<Animation>().IsPlaying("idle")) PlayAnimation("idle");
    }

    void FixedUpdate()
    {
        //MoveHero();
        //JumpHero();
    }

    void PlayAnimation(string AnimName)
    {
        if (!GetComponent< Animation > ().IsPlaying(AnimName))
            GetComponent< Animation > ().CrossFadeQueued(AnimName, 0.3f, QueueMode.PlayNow);
    }

    void CheckForIdle()
    {
        if (GetComponent< Animation > ().IsPlaying("run")) PlayAnimation("idle");
        if (!GetComponent< Animation > ().isPlaying) GetComponent< Animation > ().Play("idle");
    }

    void MoveHero()
    {
        //if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2) {
        //if (Input.GetAxis("Horizontal") > 0.02) transform.eulerAngles.y = -90;
        //else if (Input.GetAxis("Horizontal") < -0.02) transform.eulerAngles.y = 90;
        //transform.Translate(Vector3.forward * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * 3.5);
        //if (!GetComponent.<Animation>().IsPlaying("Jump")) PlayAnimation("run");
        //}
        //else CheckForIdle();

        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.name == "GameFinish")
                Application.LoadLevel("gameFinish");
        }
    }

    IEnumerator pause(float secondsToWait)
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("After Waiting 2 Seconds");
    }
}
