using UnityEngine;
using System.Collections;

public class MoveRabbit : MonoBehaviour {
    public AudioClip JumpSound = null;
    public AudioClip DieSound = null;
    public AudioClip DieMusic = null;
    public AudioClip DefaultMusic = null;
    public AudioSource BackgroundMusic = null;
    public GameObject StartPosition = null;
    private AudioSource mAudioSource = null;
    public bool Move = false;
    private float nextJump;

	// Use this for initialization
	void Start () {
        mAudioSource = GetComponent< AudioSource >();
        Move = false;
        StartPosition = GameObject.FindGameObjectWithTag("Start");
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void FixedUpdate()
    {
        MoveHero();
        JumpHero();
    }

    void PlayAnimation(string AnimName)
    {
        switch(AnimName)
        {
            case "Run":
                GetComponent<Animator>().SetBool("Run", true);
                break;
            case "Jump":
                GetComponent<Animator>().SetTrigger("Jump");
                break;
            case "Idle":
                GetComponent<Animator>().SetBool("Run", false);
                break;
        }

        //if (!GetComponent< Animation > ().IsPlaying(AnimName))
        //    GetComponent< Animation > ().CrossFadeQueued(AnimName, 0.3f, QueueMode.PlayNow);
    }

    void CheckForIdle()
    {
        //if (GetComponent<Animation>().IsPlaying("run")) PlayAnimation("idle");
        //if (!GetComponent<Animation>().isPlaying) GetComponent<Animation>().Play("idle");
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

        if (Move)
        {
            transform.position.Set(transform.position.x, transform.position.y-90, transform.position.z);
            transform.Translate(Vector3.forward * Mathf.Abs(.6f) * Time.deltaTime * 3.5f);
            //if (!GetComponent< Animation > ().IsPlaying("Jump")) PlayAnimation("run");
            PlayAnimation("Run");
        }
    }

    void JumpHero()
    {
        if ((Input.GetButton("Jump") || (Lean.Touch.LeanTouch.Fingers.Count > 0)) && nextJump < Time.time)
        {
            GetComponent< Rigidbody > ().AddForce(Vector3.up * 25000);

            if (mAudioSource != null && JumpSound != null)
            {
                mAudioSource.clip = JumpSound;
                mAudioSource.Play();
            }

            PlayAnimation("Jump");
            nextJump = Time.time + 1;

            //yield WaitForSeconds(0.7f); PlayAnimation("idle");
            //StartCoroutine(pause(.7f));
            //PlayAnimation("idle");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.name == "GameFinish")
                Application.LoadLevel("gameFinish");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Lava"))//they die
        {
            StartCoroutine(die(2));
        }
        else if(other.gameObject.tag.Equals("Trap"))
        {
            StartCoroutine(die(0));
        }
    }

    public void MoveCharacter()
    {
        Move = true;
    }

    IEnumerator die(int secondsToWait)
    {
        if (mAudioSource != null && DieSound != null)
        {
            mAudioSource.clip = DieSound;
            mAudioSource.Play();
        }

        if (BackgroundMusic != null && DieMusic != null)
        {
            BackgroundMusic.clip = DieMusic;
            BackgroundMusic.Play();
        }

        //PlayAnimation("idle");
        Move = false;

        gamestate.Instance.SetLives(gamestate.Instance.getLives() - 1);

        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(secondsToWait);
        Debug.Log("After Waiting 2 Seconds");
        
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<CountdownTimer>().count = true;
        
        if (BackgroundMusic != null && DefaultMusic != null)
        {
            BackgroundMusic.clip = DefaultMusic;
            BackgroundMusic.Play();
        }

        if (StartPosition != null)
            this.gameObject.transform.position = StartPosition.gameObject.transform.position;
    }
}
