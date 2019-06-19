using UnityEngine;
using System.Collections;

public class LockTheScreen : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject win = activity.Call<AndroidJavaObject>("getWindow");
        AndroidJavaObject lp = new AndroidJavaClass("android/view/WindowManager$LayoutParams");
        win.Call("addFlags", lp.GetStatic<int>("FLAG_SHOW_WHEN_LOCKED"));
        win.Call("addFlags", lp.GetStatic<int>("FLAG_ALLOW_LOCK_WHILE_SCREEN_ON"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
