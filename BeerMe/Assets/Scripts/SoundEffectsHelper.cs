using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	public AudioClip crowdCheeringSound;
	public AudioClip beerPouringSound;
	public AudioClip haveAnotherSound;
	public AudioClip backgroundSound;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;

		AudioSource.PlayClipAtPoint(backgroundSound, transform.position);
	}

	void Start ()
	{
//		backgroundSound = GetComponent<AudioSource> ();
//		backgroundSound..Play ();


	}
	
	public void MakeCrowdCheeringSound()
	{
		MakeSound(crowdCheeringSound);
	}
	
	public void MakeBeerPouringSound()
	{
		MakeSound(beerPouringSound);
	}
	
	public void MakeHaveAnotherSound()
	{
		MakeSound(haveAnotherSound);
	}


	/// <summary>
	/// Play a given sound
	/// </summary>
	/// <param name="originalClip"></param>
	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}
