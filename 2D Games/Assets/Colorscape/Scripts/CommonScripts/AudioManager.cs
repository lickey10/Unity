﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public static event Action<bool> OnSoundStatusChangedEvent;
	public static event Action<bool> OnMusicStatusChangedEvent;

	[HideInInspector] public bool isSoundEnabled = true;
	[HideInInspector] public bool isMusicEnabled = true;

	public AudioSource audioSource;	//	Source of the audio
	public AudioClip clickSound;	//  Plays this sound on each button click.
	public AudioClip answerSound;	//	Plays this sound when player enters answer.
	public AudioClip gameOverSound;	//	This sound will play on loading gameover screen.

	private static AudioManager _instance;	

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static AudioManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<AudioManager>();
			}
			return _instance;
		}
	}
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (_instance != null ) {
			if(_instance.gameObject != gameObject)
			{
				Destroy (gameObject);
				return;
			}
		}
		_instance = GameObject.FindObjectOfType<AudioManager>();
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		initAudioStatus ();
	}

	/// <summary>
	/// Inits the audio status.
	/// </summary>
	public void initAudioStatus ()
	{
		isSoundEnabled = (PlayerPrefs.GetInt ("isSoundEnabled", 0) == 0) ? true : false;
		isMusicEnabled = (PlayerPrefs.GetInt ("isMusicEnabled", 0) == 0) ? true : false;

		if ((!isSoundEnabled) && (OnSoundStatusChangedEvent != null)) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
		if ((!isMusicEnabled) && (OnMusicStatusChangedEvent != null)) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
	}

	/// <summary>
	/// Toggles the sound status.
	/// </summary>
	public void ToggleSoundStatus ()
	{
		isSoundEnabled = (isSoundEnabled) ? false : true;
		PlayerPrefs.SetInt ("isSoundEnabled", (isSoundEnabled) ? 0 : 1);

		if (OnSoundStatusChangedEvent != null) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
	}

	/// <summary>
	/// Toggles the music status.
	/// </summary>
	public void ToggleMusicStatus ()
	{
		isMusicEnabled = (isMusicEnabled) ? false : true;
		PlayerPrefs.SetInt ("isMusicEnabled", (isMusicEnabled) ? 0 : 1);

		if (OnMusicStatusChangedEvent != null) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
	}

	/// <summary>
	/// Plaies the button click sound.
	/// </summary>
	public void PlayButtonClickSound()
	{
		if (AudioManager.instance.isSoundEnabled) {
			audioSource.PlayOneShot (clickSound);
		}
	}

	/// <summary>
	/// Plaies the answer sound.
	/// </summary>
	public void PlayAnswerSound()
	{
		if (AudioManager.instance.isSoundEnabled) {
			audioSource.PlayOneShot (answerSound);
		}
	}

	/// <summary>
	/// Plaies the game over sound.
	/// </summary>
	public void PlayGameOverSound()
	{
		if (AudioManager.instance.isSoundEnabled) {
			audioSource.PlayOneShot (gameOverSound);
		}
	}

	/// <summary>
	/// Plays the sound given.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlaySound(AudioClip clip)
	{
		if (AudioManager.instance.isSoundEnabled) {
			audioSource.PlayOneShot(clip);
		}
	}
}
