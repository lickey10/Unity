using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class DungeonScraper : MonoBehaviour
{
    WebClient client;
    bool downloading = false;
    byte[] data = null;

    // Start is called before the first frame update
    void Start()
    {
        client = new WebClient();
        client.DownloadDataAsync(new System.Uri("http://locator.wizards.com/#brand=magic&a=search&p=Fort+Collins,+CO,+USA&c=40.5852602,-105.08442300000002&massmarket=no"));

        downloading = true;
        client.DownloadDataCompleted += Client_DownloadDataCompleted;

        //client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
        //client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
    }

    private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
    {
        data = e.Result;

        var str = System.Text.Encoding.Default.GetString(data);
        File.WriteAllBytes("DungeonList.txt", data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
