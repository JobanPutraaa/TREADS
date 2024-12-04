using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVol();
        }
        else
        {
            setMusicVol();
        }
    }
    public void setMusicVol()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void setMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
    }

    public void setSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    private void LoadVol()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        setMusicVol();
    }
}
