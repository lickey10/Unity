using UnityEngine;
using System.Collections;
//using Scripts;

public class CoinScript : MonoBehaviour
{
    public AudioClip CoinSound = null;
    public AudioClip GoalReachedSound = null;
    public int CoinValue = 10;
    public bool Spin = false;
    public float SpinSpeed = 3;
    public GameObject PointsDisplayObject;
    public enum SpinDirections { x,y,z};
    public SpinDirections SpinDirection = CoinScript.SpinDirections.y;
    private AudioSource mAudioSource = null;
    
    // Use this for initialization
    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Spin)
        {
            switch (SpinDirection)
            {
                case SpinDirections.x:
                    transform.Rotate(new Vector3(SpinSpeed, 0f, 0f));
                    break;
                case SpinDirections.y:
                    transform.Rotate(new Vector3(0f, SpinSpeed, 0f));
                    break;
                case SpinDirections.z:
                    transform.Rotate(new Vector3(0f, 0f, SpinSpeed));
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (PointsDisplayObject)
            {
                PointsDisplayObject.GetComponentInChildren<UILabel>().text = "+" + CoinValue.ToString();
                PointsDisplayObject.transform.Find("pointsEnd").position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);

                Instantiate(PointsDisplayObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
            }

            Destroy(this.gameObject);
            
            if (mAudioSource != null && CoinSound != null)
            {
                mAudioSource.PlayOneShot(CoinSound);
            }

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
