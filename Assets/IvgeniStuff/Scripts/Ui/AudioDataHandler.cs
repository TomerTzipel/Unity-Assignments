using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioDataHandler : MonoBehaviour
{
    [SerializeField] public AudioMixer audioMixer;
    public SliderManager volumeUI;

    void Start()
    {
        ApplyVolume("Master", PlayerPrefs.GetFloat("MasterVolume", 1f));
        ApplyVolume("Music", PlayerPrefs.GetFloat("MusicVolume", 1f));
        ApplyVolume("SFX", PlayerPrefs.GetFloat("SFXVolume", 1f));

        volumeUI.OnMasterVolumeChanged += val => ApplyVolume("Master", val);
        volumeUI.OnMusicVolumeChanged += val => ApplyVolume("Music", val);
        volumeUI.OnSFXVolumeChanged += val => ApplyVolume("SFX", val);
    }

    private void ApplyVolume(string parameterName, float value)
    {
        float dbValue = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat(parameterName, dbValue);
    }
}
