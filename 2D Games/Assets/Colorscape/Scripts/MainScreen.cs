using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	void OnEnable()
	{
		GetComponent<Animator> ().SetTrigger ("reset");
	}

	/// <summary>
	/// Execute event on play  button pressed. this willl load select mode screen.
	/// </summary>
	public void OnPlayButtonPressed()
	{
		if (InputManager.instance.canInput()) {
			InputManager.instance.AddButtonTouchEffect();
			GameUIController.instance.LoadSelectModeScreen();
		}
	}




}
