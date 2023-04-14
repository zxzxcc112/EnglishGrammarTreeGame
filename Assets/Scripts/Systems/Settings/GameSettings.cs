using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObj/Game Settings", fileName = "New GameSettings")]
public class GameSettings : ScriptableObject
{
    public Resolution currentResolution = default;
    public bool isFullScreen = default;
    public float SoundEffect = default;
    public float BackGroundMusic = default;
}
