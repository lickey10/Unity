using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelDisplayTip : MonoBehaviour {

	// Use this for initialization
	string tipContextType = "";
		Transform panel;
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	bool locker;
//	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
//	{
//		// Add event handler code here
//		switch (control.name) {
//		case "btnDisplayOK":
//			GameManager.getInstance ().playSfx ("click");
//			gameObject.GetComponent<dfPanel> ().IsVisible  = false;
//			GameData.getInstance().isLock = false;
//			Time.timeScale = 1;		
//			break;
//		}
//	}
		public void close(){
			ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 500, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));

		}

	public void showMe(){
//		GetComponent<dfPanel> ().IsVisible = isVis;
		//show tip freely during level if once displayed.
				panel = transform.Find ("panel");

				initView ();

				GameData.getInstance().cLvShowedTip = true;
		
				ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo","oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
		
				GameData.getInstance ().lockGame (true);

	}
	
		void initView(){
				panel.transform.Find ("txtDisplayTipTitle").GetComponent<Text> ().text = Localization.Instance.GetString ("tipTitle");
				panel.transform.Find ("txtDisplayTipContent").GetComponent<Text> ().text = Localization.Instance.GetString ("level"+GameData.getInstance().cLevel+ "tip");
				panel.transform.Find ("btnClose").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnClose");		
		}

		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}

		void OnShowCompleted(){
				GameData.getInstance ().lockGame (true);
		}

	


}
