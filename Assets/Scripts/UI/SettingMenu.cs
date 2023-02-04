using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public Dropdown resolutionsDropdown;


    private void Start()
    {
        resolutions= Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i =0; i< resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x"+ resolutions[i].height;
            options.Add(option);
        }

        resolutionsDropdown.AddOptions(options);
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
    public void setFullScreen (bool isFullScreened)
    {
        Screen.fullScreen = isFullScreened;
    }
}
