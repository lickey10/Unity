using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardVideoAd : MonoBehaviour 
{
	[SerializeField]
	string UnityAdID;

	[SerializeField]
	GameObject Clock;
	ClockScript CSK;

	public GameObject VideoDialogPanel;
	public bool IsAdShown;

	GameObject InputManager;

	void Start () 
	{
		CSK = Clock.GetComponent<ClockScript> ();
		Advertisement.Initialize(UnityAdID, false);
	}

	public void ShowVideoDialog()
	{
		VideoDialogPanel.SetActive (true);
		Time.timeScale = 0;
	}
	
	public void GameOver()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(3);
	}

	public void ShowAd()
	{
		if (UnityAdID != null) 
		{
			ShowOptions Options = new ShowOptions ();
			Options.resultCallback = AdCallbackhandler;
		
			if (Advertisement.IsReady ("rewardedVideoZone")) 
			{
				Advertisement.Show ("rewardedVideoZone", Options);
			}
			else 
			{
				Advertisement.Initialize (UnityAdID, false);
			}
		}
	}
	
	void AdCallbackhandler(ShowResult Result)
	{
		switch(Result)
		{
		case ShowResult.Finished:
			IsAdShown = true;
			VideoDialogPanel.SetActive (false);
			CSK.TimeLeft = 5;
			CSK.CountDown ();
			Time.timeScale = 1;
			CSK.AllowTouch = true;
			break;
		}
	}
}
