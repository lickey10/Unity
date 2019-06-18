using UnityEngine;
using System.Collections;

public class PickupHandler : MonoBehaviour {
	public AudioClip CoinSound = null;
	public AudioClip GoalReachedSound = null;
	public AudioClip CauldronLitSound = null;
	public GameObject CauldronFire = null;
	private AudioSource mAudioSource = null;

	// Use this for initialization
	void Start () {
		mAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			Destroy (other.gameObject);

			if (mAudioSource != null && CoinSound != null) {
				mAudioSource.PlayOneShot (CoinSound);
			}

			//add time to timer
			gamestate.Instance.SetTimer (10);

			gamestate.Instance.SetCoinsCollected (gamestate.Instance.GetCoinsCollected () + 1);
		} else if (other.gameObject.tag.Equals ("Goal")) {
			if (mAudioSource != null && GoalReachedSound != null) {
				mAudioSource.PlayOneShot (GoalReachedSound);
			}

		} else if (other.gameObject.tag.Equals ("Cauldron")) {
			if (mAudioSource != null && CauldronLitSound != null) {
				mAudioSource.PlayOneShot (CauldronLitSound);
			}
			//remove collider so we don't light the fire more than once
			GameObject.Destroy (other);

			Transform fireLocation = other.transform.Find ("FireLocation");

			//light the fire
			Instantiate (CauldronFire, fireLocation.position, fireLocation.rotation);

			gamestate.Instance.SetTorchesLit (gamestate.Instance.GetTorchesLit () + 1);
		} else if (other.gameObject.tag.Equals ("Falling")) {
			//they fell off - the game is over
			gamestate.Instance.SetGameRunning(false);
		}
	}
}
