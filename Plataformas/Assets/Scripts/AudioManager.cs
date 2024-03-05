using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip musicCastlevania;
    [SerializeField] private AudioClip musicFinalFantasy;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        audioSource.Stop();
        //GetComponent<AudioSource>().clip = null;
    }

    public void StartAgain()
    {
        audioSource.clip = musicCastlevania;
        audioSource.Play();
    }

    //public float IsPlaying() => GetComponent<AudioSource>().;
}
