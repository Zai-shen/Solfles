using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayContinuous(AudioManager.Instance.AudioClips[0]);
    }

    public void PlayGame()
    {
        if (AudioManager.Instance.IsPlayingMusic)
        {
            AudioManager.Instance.StopPlaying();
            AudioManager.Instance.PlayContinuous(AudioManager.Instance.AudioClips[1]);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    { 
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}