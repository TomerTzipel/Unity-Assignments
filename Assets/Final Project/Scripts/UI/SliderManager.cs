using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.onValueChanged.AddListener(val =>
        {
            AudioManager.Instance.ApplyNewVolume(AudioGroup.Master, val);
        });

        musicSlider.onValueChanged.AddListener(val =>
        {
            AudioManager.Instance.ApplyNewVolume(AudioGroup.Music, val);
        });

        sfxSlider.onValueChanged.AddListener(val =>
        {
            AudioManager.Instance.ApplyNewVolume(AudioGroup.SFX, val);
        });
    }
}
