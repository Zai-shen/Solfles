using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance?.PlayContinuous(AudioManager.Instance.AudioClips[0]);
    }

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlayOnce(AudioManager.Instance.AudioClips[2]);
    }
    
    public void PlayGame()
    {
        if (AudioManager.Instance.IsPlayingMusic)
        {
            AudioManager.Instance.StopPlaying();
            AudioManager.Instance.PlayContinuous(AudioManager.Instance.AudioClips[1]);
            PlayClickSound();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    { 
        PlayClickSound();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}