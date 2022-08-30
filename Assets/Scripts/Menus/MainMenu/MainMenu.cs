using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainlyMenu;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;
    
    private void Start()
    {
        AudioManager.Instance?.PlayContinuous(AudioManager.Instance.MusicClips[0]);
    }

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlayUIClick();
    }
    
    public void PlayGame()
    {
        if (AudioManager.Instance.IsPlayingMusic)
        {
            AudioManager.Instance.StopPlaying();
            AudioManager.Instance.PlayContinuous(AudioManager.Instance.MusicClips[1]);
            PlayClickSound();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayOptionsMenu()
    {
        PlayClickSound();
        MainlyMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }
    
    public void DisplayMainlyMenu()
    {
        PlayClickSound();
        MainlyMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }
    
    public void DisplayCreditsMenu()
    {
        PlayClickSound();
        MainlyMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
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