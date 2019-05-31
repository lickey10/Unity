using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float sensitivity = 100;
	public float loudness = 0;
    private string audioSource = "";
	
	void Start() {
		GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, 44100);
		GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
		GetComponent<AudioSource>().mute = true; // Mute the sound, we don't want the player to hear it
		//while (!(Microphone.GetPosition(GetComponent<AudioSource>().name) > 0)){} // Wait until the recording has started
        while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        GetComponent<AudioSource>().Play(); // Play the audio source!

        audioSource = GetComponent<AudioSource>().name;
    }
	
	void Update(){
		loudness = GetAveragedVolume() * sensitivity;

        if(loudness != 0)
        {
            print("yeah");
        }
	}

    void OnGUI()
    {
        var style = new GUIStyle("label");
        style.fontSize = 80;

        GUI.Label(new Rect(175, 0, 600, 400), "soundLevel:" + loudness.ToString(), style);
        GUI.Label(new Rect(175, 400, 600, 400), "AudioSource:" + audioSource, style);
    }

    float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		GetComponent<AudioSource>().GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}
}
