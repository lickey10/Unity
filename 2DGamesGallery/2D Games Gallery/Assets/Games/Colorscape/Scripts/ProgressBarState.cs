using UnityEngine;
using System.Collections;

public class ProgressBarState : MonoBehaviour {

	public void OnCircleAnimationCompleted()
	{
		switch (GameUIController.ActiveGameMode) {
		case GameMode.Classic:
			GamePlay_Classic.instance.OnGameOver ();
			break;
		case GameMode.Chrono:
			GamePlay_Chrono.instance.OnGameOver ();
			break;
		case GameMode.Findcolor:
			GamePlay_FindColor.instance.OnGameOver ();
			break;
		case GameMode.Tapcolor:
			GamePlay_TapColor.instance.OnGameOver ();
			break;
		}
	}
}
