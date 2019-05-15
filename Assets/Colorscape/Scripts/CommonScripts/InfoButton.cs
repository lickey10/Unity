using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour 
{
	public Button btnInfo;
	public string NavigationURL;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnInfo.onClick.AddListener(() => 
		                             {
			if (InputManager.instance.canInput ()) {
				InputManager.instance.DisableTouchForDelay ();
				InputManager.instance.AddButtonTouchEffect ();
				Application.OpenURL(NavigationURL);
			}
		});
	}
}
