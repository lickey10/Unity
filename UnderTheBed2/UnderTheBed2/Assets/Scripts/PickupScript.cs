using UnityEngine;
using System.Collections;
//using Scripts;

public class PickupScript : MonoBehaviour
{
    public AudioClip PickupSound = null;
    public AudioClip GoalReachedSound = null;
    public int CoinValue = 10;
    private AudioSource mAudioSource = null;
    
    // Use this for initialization
    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Pickup"))
        {
            var PointScript = gameObject.AddComponent<Point>();
            PointScript.PointCurrent = 50;

            Destroy(other.gameObject);

            if (mAudioSource != null && PickupSound != null)
            {
                mAudioSource.PlayOneShot(PickupSound);
            }

            gamestate.Instance.StarCount += 1;
            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 50);

            //GameObject scripts = GameObject.FindWithTag("Scripts");
            //SceneManager gameManager = scripts.GetComponent<SceneManager>();
            //gameManager.Coins = gameManager.Coins + CoinValue;
            //DBStoreController.singleton.balance += CoinValue;

            //add time to timer
            //gamestate.Instance.SetTimer (10);

            //gamestate.Instance.SetCoinsCollected (gamestate.Instance.GetCoinsCollected () + 1);
        }
        else if (other.gameObject.tag.Equals("Goal"))
        {
            if (mAudioSource != null && GoalReachedSound != null)
            {
                mAudioSource.PlayOneShot(GoalReachedSound);
            }

        }
    }
}
