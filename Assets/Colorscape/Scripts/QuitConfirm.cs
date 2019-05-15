using UnityEngine;
using System.Collections;

public class QuitConfirm : MonoBehaviour {

	public void OnYesButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();

			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}
	}

	public void OnNoButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.OnQuitConfirmCancel();
		}
	}
}
