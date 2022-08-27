using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue ()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


    public void MainMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}