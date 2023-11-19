using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource sfxAudioSource, musicAudioSource;

    public void PlayClip(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
        //GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
