using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Counter : MonoBehaviour 
{
	public Text txtCounter;
	int counterIndex = 3;

	public void StartCounter()
	{
		if (counterIndex >= 0) {
			if (counterIndex == 0) {
				txtCounter.text = "Go";
			} else {
				txtCounter.text = counterIndex.ToString ();
			} 
			txtCounter.GetComponent<Animator> ().SetTrigger ("CounterFade");
			counterIndex--;
		} 
		else 
		{
			txtCounter.GetComponent<Animator> ().SetTrigger("TurnOff");
	
			switch (GameUIController.ActiveGameMode) {
			case GameMode.Classic:
				GamePlay_Classic.instance.StartFillCircle ();
				break;
			case GameMode.Chrono:
				GamePlay_Chrono.instance.StartFillCircle ();
				break;
			case GameMode.Findcolor:
				GamePlay_FindColor.instance.StartFillCircle ();
				break;
			case GameMode.Tapcolor:
				GamePlay_TapColor.instance.StartFillCircle ();
				break;
			}

			counterIndex = 3;
		}
	}

	public void OnCounterAnimationDone()
	{	
		StartCounter ();
	}
}
