using UnityEngine;
using System.Collections;

/// <summary>
/// Game mode. Enum for all game different game modes.
/// </summary>
public enum GameMode
{
	Classic = 0,
	Chrono = 1,
	Findcolor = 2,
	Tapcolor = 3
}

/// <summary>
/// Select mode.
/// </summary>
public class SelectMode : MonoBehaviour {

	/// <summary>
	/// Raises the classic mode selected event.
	/// </summary>
	public void OnClassicModeSelected()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.LoadGamePlay(GameMode.Classic);
		}
	}

	/// <summary>
	/// Raises the chrono mode selected event.
	/// </summary>
	public void OnChronoModeSelected()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.LoadGamePlay(GameMode.Chrono);
		}
	}

	/// <summary>
	/// Raises the find color mode selected event.
	/// </summary>
	public void OnFindColorModeSelected()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.LoadGamePlay(GameMode.Findcolor);
		}
	}

	/// <summary>
	/// Raises the tap color mode selected event.
	/// </summary>
	public void OnTapColorModeSelected()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.AddButtonTouchEffect ();
			GameUIController.instance.LoadGamePlay(GameMode.Tapcolor);
		}
	}
}
