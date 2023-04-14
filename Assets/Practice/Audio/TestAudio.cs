using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSO audioSO;

    public bool isPause;

    private void Awake()
    {
    }

    private void Update()
    {
        if(isPause)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void SetConfig()
    {
        audioSource.outputAudioMixerGroup = audioSO.mixerGroup;
        
    }
}
