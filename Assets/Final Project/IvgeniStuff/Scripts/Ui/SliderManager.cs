using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public event Action<float> OnMasterVolumeChanged;
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.onValueChanged.AddListener(val =>
        {
            PlayerPrefs.SetFloat("MasterVolume", val);
            PlayerPrefs.Save();
            OnMasterVolumeChanged?.Invoke(val);
        });

        musicSlider.onValueChanged.AddListener(val =>
        {
            PlayerPrefs.SetFloat("MusicVolume", val);
            PlayerPrefs.Save();
            OnMusicVolumeChanged?.Invoke(val);
        });

        sfxSlider.onValueChanged.AddListener(val =>
        {
            PlayerPrefs.SetFloat("SFXVolume", val);
            PlayerPrefs.Save();
            OnSFXVolumeChanged?.Invoke(val);
        });
    }
}
