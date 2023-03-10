using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private void Start()
    {
        SetupResolutions();
        SetFullScreen(fullScreenToggle.isOn);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    private void SetupResolutions()
    {
        int resolutionIndex = 0;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for(int i = 0;i < Screen.resolutions.Length;i++)
        {
            Resolution resolution = Screen.resolutions[i];
            options.Add(resolution.width + " x " + resolution.height);

            if (resolution.width == Screen.currentResolution.width &&
               resolution.height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void LoadGame(GameData data)
    {
        fullScreenToggle.isOn = data.isFullScreen;
    }

    public void SaveGame(ref GameData data)
    {
        data.isFullScreen = fullScreenToggle.isOn;
    }
}
