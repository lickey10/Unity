/// <summary>
///
//      ALIyerEdon - Winter 2017 - Orginally writed for OBB Downloader
//
/// </summary>
using UnityEngine;
using System.Collections;
using System.Net;
using System.ComponentModel;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class FastDownloader : MonoBehaviour {


	[Header("Informations")]
	public string savePath = @"D:\utilities\Programming\Unity\Projects\trunk\PartyQuestGathering";
    public string downloadUrl = "";//"https://www.googleapis.com/drive/v3/files/16__jYYkmq7SfDNzuHvaeY6hUDBg5Ys0b?alt=media&key=AIzaSyBG-ivXJ5Pkg7eBWMuhNWg4sQAQ4CQFq6E";

    [Header("Options")]
	public bool persistentDataPath = false;
	public bool showBytes = true;
	public bool onStart = false;
    public bool saveToFile = true;

	[Header("Display")]
	public Slider progressBar;

	public Text progressText;
	public Text bytesText;

	[Header("Finish")]
	// Activate this button when download has finished
	public GameObject finishedButton;
	// De Activate this button when download has finished
	public GameObject downloadButton;

	public bool UseOrginalName = true;
	// File name to save file in location + extension (.exe ,.zip and ...)
	public string newFileName = "example.zip";

    public delegate void DownloadCompleted();
    public static DownloadCompleted DownloadComplete;
    public string DownloadedContent = "";


    // internal usage
    string uri;
	float progress ;
	string bytes;
	bool downloading;
	bool finished;


	void Start () 
	{
        if (progressBar)
        {
            // Set progress bar (UI Slider) max value to 100 (%) 
            if (progressBar.maxValue != 100f)
                progressBar.maxValue = 100f;

            // Starting value is 0
            progressBar.value = 0;
        }

		uri = downloadUrl;

		// Use orginal downloaded file name or not
		if(UseOrginalName)
			newFileName = Path.GetFileName(uri);

		// Check directory exists
		DirectoryInfo df = new DirectoryInfo(savePath);
		if (!df.Exists)
			Directory.CreateDirectory (savePath);


		if(onStart)
			DownloadFile ();

	}
	WebClient client;

	// Main download function (public for ui button)     
	public void DownloadFile()
	{
        DownloadFile(downloadUrl);
	}

    public void DownloadFile(string downloadURL)
    {
        if (downloadButton)
            downloadButton.SetActive(false);

        cancelled = false;

        client = new WebClient();

        if (!saveToFile)
        {
            client.DownloadDataAsync(new System.Uri(downloadURL));
            client.DownloadDataCompleted += Client_DownloadDataCompleted;
        }
        else
        {
            if (!persistentDataPath)
                client.DownloadFileAsync(new System.Uri(downloadURL), savePath + "/" + newFileName);
            else
                client.DownloadFileAsync(new System.Uri(uri), Application.persistentDataPath + "/" + newFileName);
            
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
        }

        downloading = true;
    }

    private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
    {
        string result = Encoding.UTF8.GetString(e.Result, 0, e.Result.Length);

        DownloadedContent = result;

        DownloadComplete();
    }

    // Manage download state on unity main thread
    void Update()
	{
		if (downloading) {
            if (progressBar)
            {
                progressBar.value = progress;
                progressText.text = progress.ToString() + "% ";
            }

			if (showBytes && bytesText)
				bytesText.text = "Recieved : " + bytes + " kb";
		} 


		if (finished) {
			if (cancelled) {
				bytesText.text = "Canceled";

                if(progressText)
				    progressText.text = "0 %"; 

				finished = false;
			}
			else
			{
                if (finishedButton)
                {
                    if (!finishedButton.activeSelf)
                    {
                        finishedButton.SetActive(true);
                        downloadButton.SetActive(false);
                    }
                }

                if(bytesText)
				    bytesText.text = "Recieved : " +"Completed";

                if(progressText)
				    progressText.text = "100 %";
			}
		}
		
	}
	void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
	{
		if (cancelled) {
			Debug.Log ("Canceled");
			downloading = false;
			finished = true;

		}
		else
		{
		if (e.Error == null)
		{
			Debug.Log ("Completed");
			downloading = false;
			finished = true;
		} else
			Debug.Log (e.Error.ToString ());
		}
        
        DownloadComplete();
    }

	void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		progress = (e.BytesReceived * 100 / e.TotalBytesToReceive);
		bytes = e.BytesReceived/1000+ " / " +  e.TotalBytesToReceive/1000;
	}

	// Use this for game start button when download has finished
	public void LoadLevel(string name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
	}

	bool cancelled;

	public void CancelDownload()
	{
		cancelled = true;
		if (client != null) {
			client.CancelAsync ();


			/*if(!persistentDataPath)
				File.Delete ( savePath+ "/" + newFileName);
			else
				File.Delete ( Application.persistentDataPath+ "/" + newFileName);
			*/
		}

		downloadButton.SetActive (true);

	}

	// Cancel download when script has disabled
	void OnDisable()
	{
		cancelled = true;
		if(client!=null)
			client.CancelAsync ();
	}


}

