using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class InputManager_colorSpin : Singleton_colorSpin<InputManager_colorSpin>
{
	public static event Action OnBackButtonPressedEvent;
	static bool isTouchAvailable = true;
	public EventSystem eventSystem;

	public bool canInput (float delay = 0.25F, bool disableOnAvailable = true)
	{
		bool status = isTouchAvailable;
		if (status && disableOnAvailable) 
		{
			isTouchAvailable = false;
			eventSystem.enabled = false;

			StopCoroutine ("EnbaleTouchAfterDelay");
			StartCoroutine ("EnbaleTouchAfterDelay", delay);
		}
		return status;
	}

	public void DisableTouch()
	{
		isTouchAvailable = false;
		eventSystem.enabled = false;
	}

	public void DisableTouchForDelay (float delay = 0.25F)
	{
		isTouchAvailable = false;
		eventSystem.enabled = false;

		StopCoroutine ("EnbaleTouchAfterDelay");
		StartCoroutine ("EnbaleTouchAfterDelay", delay);
	}

	public void EnableTouch ()
	{
		isTouchAvailable = true;
		eventSystem.enabled = true;
	}

	public IEnumerator EnbaleTouchAfterDelay (float delay)
	{
		yield return new WaitForSeconds (delay);
		EnableTouch ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (OnBackButtonPressedEvent != null) {
				OnBackButtonPressedEvent ();
			}
		}
	}
}
