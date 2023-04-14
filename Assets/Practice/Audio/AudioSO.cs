using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptableobj/AudioSO")]
public class AudioSO : ScriptableObject
{
    public List<AudioClip> clipList;
    public AudioMixerGroup mixerGroup;

}
