using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;

    public bool IsPlayingMusic;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayContinuous(AudioClip audioClip)
    {
        IsPlayingMusic = true;
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }

    public void StopPlaying()
    {
        AudioSource.Stop();
        IsPlayingMusic = false;
    }

    public void PlayOnce(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }

    public void PlayUIClick()
    {
        AudioSource.PlayOneShot(AudioClips[2]);
    }
}
