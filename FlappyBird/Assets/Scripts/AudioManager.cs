using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxAudioSource, musicAudioSource;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != null)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    public void PlayClip(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

}
