using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PauseMenu _pauseMenu;
    private bool _gamePaused;

    private void Awake()
    {
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (!_gamePaused)
        {
            _gamePaused = true;
            _pauseMenu?.Pause();
        }
        else
        {
            _gamePaused = false;
            _pauseMenu?.Continue();
        }
    }
}
