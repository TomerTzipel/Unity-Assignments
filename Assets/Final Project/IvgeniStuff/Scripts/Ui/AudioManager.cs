using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public enum AudioGroup
{
    Master,Music,SFX
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] public AudioMixer audioMixer;
    private void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ApplyVolume(AudioGroup.Master, PlayerPrefs.GetFloat("MasterVolume", 1f));
        ApplyVolume(AudioGroup.Music, PlayerPrefs.GetFloat("MusicVolume", 1f));
        ApplyVolume(AudioGroup.SFX, PlayerPrefs.GetFloat("SFXVolume", 1f));
    }

    public void ApplyVolume(AudioGroup audioGroup, float value)
    {
        switch (audioGroup)
        {
            case AudioGroup.Master:
                PlayerPrefs.SetFloat("MasterVolume", value);
                break;
            case AudioGroup.Music:
                PlayerPrefs.SetFloat("MusicVolume", value);
                break;
            case AudioGroup.SFX:
                PlayerPrefs.SetFloat("SFXVolume", value);  
                break;
        }
        PlayerPrefs.Save();

        float dbValue = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat(audioGroup.ToString(), dbValue);
    }
}
