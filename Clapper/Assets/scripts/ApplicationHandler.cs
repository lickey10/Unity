using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass pluginClass = new AndroidJavaClass("android.content.pm.PackageManager");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        int flag = pluginClass.GetStatic<int>("GET_META_DATA");
        AndroidJavaObject pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject syspackages = pm.Call<AndroidJavaObject>("getInstalledApplications", flag);

        //Put the file in a folder called StreamingAssets. The file will then be included in the data files that come along with the launcher.The path of the executable at runtime can be determined like so:
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "fceux.exe");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "/fceux/fceux.exe");
        System.Diagnostics.Process.Start(filePath);
    }
}
