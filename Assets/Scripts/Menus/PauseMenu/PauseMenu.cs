using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;

    private void PlayClickSound()
    {
        AudioManager.Instance.PlayUIClick();
    }

    public void Pause()
    {
        PlayClickSound();
        Time.timeScale = 0f;
        Menu.SetActive(true);
    }

    public void Continue ()
    {
        GameManager.Instance?.ResetPause.Invoke();
        
        PlayClickSound();
        Time.timeScale = 1f;
        Menu.SetActive(false);
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