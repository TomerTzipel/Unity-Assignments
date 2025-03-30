using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;


public enum AudioGroup
{
    Master,Music,SFX
}
public enum SFX
{
    Death, Hit, Damage, Flash, SlowTime, Heal, Invul,Press,Hover
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public AudioSourcePool sfxPool;
    [SerializeField] public AudioClip deathSFX;
    [SerializeField] public AudioClip hitSFX;
    [SerializeField] public AudioClip flashSFX;
    [SerializeField] public AudioClip slowTimeSFX;
    [SerializeField] public AudioClip healSFX;
    [SerializeField] public AudioClip invulSFX;
    [SerializeField] public AudioClip pressSFX;
    [SerializeField] public AudioClip hoverSFX;
    [SerializeField] public AudioClip damageSFX;
    private void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  
    }

    private void Start()
    {
        ApplyVolume(AudioGroup.Master, PlayerPrefs.GetFloat("MasterVolume", 1f));
        ApplyVolume(AudioGroup.Music, PlayerPrefs.GetFloat("MusicVolume", 1f));
        ApplyVolume(AudioGroup.SFX, PlayerPrefs.GetFloat("SFXVolume", 1f));
    }

    public void ApplyNewVolume(AudioGroup audioGroup, float value)
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
        ApplyVolume(audioGroup, value);
    }

    private void ApplyVolume(AudioGroup audioGroup, float value)
    {
        float dbValue = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat(audioGroup.ToString(), dbValue);
    }
    public void PlaySfx(SFX sfx)
    {
        PlaySfx(1f, sfx, 1f);

    }
    public void PlaySfx(float volume, SFX sfx, float pitch)
    {
        switch (sfx)
        {
            case SFX.Death:
                PlaySfx(volume,deathSFX, pitch);
                break;
            case SFX.Hit:
                PlaySfx(volume, hitSFX, pitch);
                break;
            case SFX.Damage:
                PlaySfx(volume, damageSFX, pitch);
                break;
            case SFX.Flash:
                PlaySfx(volume, flashSFX, pitch);
                break;
            case SFX.SlowTime:
                PlaySfx(volume, slowTimeSFX, pitch);
                break;
            case SFX.Heal:
                PlaySfx(volume, healSFX, pitch);
                break;
            case SFX.Invul:
                PlaySfx(volume, invulSFX, pitch);
                break;
            case SFX.Press:
                PlaySfx(volume, pressSFX, pitch);
                break;
            case SFX.Hover:
                PlaySfx(volume, hoverSFX, pitch);
                break;
        }

    }

    private void PlaySfx(float volume, AudioClip audio, float pitch)
    {
        sfxPool.PlaySound(volume, audio, pitch);
    }
}
