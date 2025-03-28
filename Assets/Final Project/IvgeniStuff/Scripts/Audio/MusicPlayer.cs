using UnityEngine;
using UnityEngine.Audio;


public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup outputMixer;
    [SerializeField] private AudioSource outputSource;
    [SerializeField] private AudioClip music;

    private void Start()
    {

        outputSource.outputAudioMixerGroup = outputMixer;
        outputSource.clip = music;
        outputSource.loop = true;
        outputSource.Play();
    }
}
