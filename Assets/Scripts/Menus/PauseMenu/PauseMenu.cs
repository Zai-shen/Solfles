using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlayOnce(AudioManager.Instance.AudioClips[2]);
    }

    public void Pause()
    {
        PlayClickSound();
        Time.timeScale = 0f;
        pause.SetActive(true);
    }

    public void Continue ()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        pause.SetActive(false);
    }

    public void Restart()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


    public void MainMenu ()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}