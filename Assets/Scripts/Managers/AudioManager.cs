using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource EffectSource;
    public AudioClip[] MusicClips;
    public AudioClip[] EffectClips;
    public bool IsPlayingMusic;

    
    #region Singleton
    
    public static AudioManager Instance = null;
	
    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);
    }

    #endregion
    

    public void PlayContinuous(AudioClip audioClip)
    {
        IsPlayingMusic = true;
        MusicSource.clip = audioClip;
        MusicSource.Play();
    }

    public void StopPlaying()
    {
        MusicSource.Stop();
        IsPlayingMusic = false;
    }

    public void PlayOnce(AudioClip audioClip)
    {
        EffectSource.PlayOneShot(audioClip);
    }

    public void PlayUIClick()
    {
        PlayOnce(EffectClips[0]);
    }
}
