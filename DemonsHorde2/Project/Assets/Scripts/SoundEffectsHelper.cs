using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	public AudioClip EnemyHitSound;
	public AudioClip CauldronHitSound;
	public AudioClip DemonHitSound;
	public AudioClip PaddleHitSound;
	public AudioClip TreasureSound;
	public AudioClip PickupSound;
	public AudioClip gameOverSound;
	public AudioClip youWonSound;
	public AudioClip levelCompleteSound;
	public AudioClip levelEndedSound;
	public AudioClip levelLockedSound;
	public AudioClip levelStartedSound;
	public AudioClip GuitarSound;
	public AudioClip MultiBallSound;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	
	public void MakeEnemyHitSound()
	{
		MakeSound(EnemyHitSound);
	}
	
	public void MakeDemonHitSound()
	{
		MakeSound(DemonHitSound);
	}

	public void MakePaddleHitSound()
	{
		MakeSound(PaddleHitSound);
	}

	public void MakeGameOverSound()
	{
		MakeSound(gameOverSound);
	}

	public void MakeYouWonSound()
	{
		MakeSound(youWonSound);
	}

	public void MakeLevelCompleteSound()
	{
		MakeSound(levelCompleteSound);
	}

	public void MakeLevelEndedSound()
	{
		MakeSound(levelEndedSound);
	}

	public void MakeTreasureSound()
	{
		MakeSound(TreasureSound);
	}

	public void MakeCauldronHitSound()
	{
		MakeSound(CauldronHitSound);
	}

	public void MakePickupSound()
	{
		MakeSound(PickupSound);
	}

	public void MakeLevelStartedSound()
	{
		MakeSound(levelStartedSound);
	}
	
	public void MakeLevelLockedSound()
	{
		MakeSound(levelLockedSound);
	}
	
	public void MakeGuitarSound()
	{
		MakeSound(GuitarSound);
	}

	public void StopGuitarSound()
	{
		StopSound(GuitarSound);
	}


	public void MakeMultiBallSound()
	{
		MakeSound(MultiBallSound);
	}
	
	public void StopMultiBallSound()
	{
		StopSound(MultiBallSound);
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

	private void StopSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.Destroy(originalClip);
	}
}
