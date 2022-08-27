using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
    }

    public void Continue ()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
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