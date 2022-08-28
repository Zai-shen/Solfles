using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        PlayContinuous(AudioClips[0]);
    }

    public void PlayContinuous(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }

    public void PlayOnce(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
