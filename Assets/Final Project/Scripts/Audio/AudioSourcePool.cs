using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private int poolSize = 15;
    [SerializeField] private AudioMixerGroup outputMixer;

    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = outputMixer;
            audioSources.Add(source);
        }
    }

    public void PlaySound(float volume, AudioClip audio, float pitch)
    {
        AudioSource availableSource = audioSources.Find(source => !source.isPlaying);

        if (availableSource == null)
        {
            availableSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(availableSource);
        }

        availableSource.pitch = pitch;
        availableSource.clip = audio;
        availableSource.volume = volume;
        availableSource.Play();
    }
}
