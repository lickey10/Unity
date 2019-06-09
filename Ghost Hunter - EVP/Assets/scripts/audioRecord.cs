using UnityEngine;
using System.Collections;
using System;

public class audioRecord : MonoBehaviour
{

    AudioClip myAudioClip;
    string filepath = "";
    public GameObject text;
    float startRecordingTime = 0;
    float clipCounter = 1;
    void Start() { }

    void Update()
    {
        if(startRecordingTime > 0)
        {
            if(Time.time >= startRecordingTime + 10f * clipCounter)
            {
                clipCounter++;
                Save();
            }
        }
    }



    void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 10, 60, 50), "Record"))
        //{
           
        //}
        //if (GUI.Button(new Rect(10, 70, 60, 50), "Save"))
        //{
            

        //    //GetComponent<AudioSource>().Play();
        //}
    }

    public void Record()
    {
        myAudioClip = Microphone.Start(null, true, 10, 44100);
        startRecordingTime = Time.time;
        
    }

    public void Save()
    {
        SavWav.Save("RecordedAudio_"+ Time.time, myAudioClip);
        WwwGoogleSpeechRequest("tmpFlac", 44000);
        //text.text += "Audio" + Time.time +"\r\n";
    }

    public void Stop()
    {
        Microphone.End(null);
        startRecordingTime = new float();
    }

    public void SpeechRecognition()
    {
        WwwGoogleSpeechRequest("tmpFlac", 44000);
    }

    //filepath = SavWav.Save("recording", _goAudioSource.clip); //save file from recording using SavWav lib
    const string flacName = "tmpFlac";
    string _response = "";
    //int sampleRate = SoundTools.Wav2Flac(filepath, flacName); //get sample rate from SoundTools lib

    private IEnumerator WwwGoogleSpeechRequest(string flacName, int sampleRate)
    {
        const string url = "http://www.google.com/speech-api/v1/recognize?xjerr=1&client=chromium〈=ru-RU";
        float[] postData1 = new float[myAudioClip.samples * myAudioClip.channels];
        //byte[] postData = new byte[myAudioClip.samples * myAudioClip.channels]; ;

        Debug.Log("Entering request method");
        myAudioClip.GetData(postData1, 0);
        byte[] postData = ConvertFloatToByteArray(postData1);
        // byte[] postData = File.ReadAllBytes(flacName);

        Debug.Log("Read flac file. Size: " + postData.Length + " bytes");

        //set up POST request parameters
        var form = new WWWForm();

        var headers = form.headers;

        headers["Method"] = "POST";
        headers["Content-Type"] = "audio/x-flac; rate=" + sampleRate;
        headers["Content-Length"] = postData.Length.ToString();
        headers["Accept"] = "application/json";

        form.AddBinaryData("fileUpload", postData, "flacFile", "audio/x-flac; rate=" + sampleRate);
        var httpRequest = new WWW(url, form.data, headers);

        yield return httpRequest;

        if (httpRequest.isDone && string.IsNullOrEmpty(httpRequest.error))
        {
            //_response = String.Format("Request succeeded. Request data: {0}{1}", Environment.NewLine, httpRequest.text);
            string _response = httpRequest.text.Substring(1);
            Debug.Log(_response);
           // _response = ParseResponse(_response);
            Debug.Log("Request succeeded. Request data: " + _response);
        }
        else
        {
            _response = string.Format("Request failed with Error: {0}", httpRequest.error);
            Debug.Log(string.Format("Request failed with Error: {0}", httpRequest.error));
        }
    }

    static byte[] ConvertFloatToByteArray(float[] floats)
    {
        byte[] ret = new byte[floats.Length * 4];// a single float is 4 bytes/32 bits

        for (int i = 0; i < floats.Length; i++)
        {
            // todo: stuck...I need to append the results to an offset of ret
            ret = BitConverter.GetBytes(floats[i]);

        }
        return ret;
    }
}




