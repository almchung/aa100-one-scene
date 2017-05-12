using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    private AudioSource myAudio;
    public string selectDevice { get; private set; }
    void Start()
    {
        selectDevice = Microphone.devices[0].ToString();
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = null;
        myAudio.loop = true; // Set the AudioClip to loop
        myAudio.mute = false; // Mute the sound, we don't want the player to hear it
        myAudio.clip = Microphone.Start(selectDevice, true, 10, 44100);
        while (!(Microphone.GetPosition(selectDevice) > 0)) { } // Wait until the recording has started
        myAudio.Play(); // Play the audio source!
    }

    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        myAudio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}