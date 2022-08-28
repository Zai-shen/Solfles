using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PauseMenu PauseMenu;
    public PauseMenu WinScreen;
    private bool _gamePaused;

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
            PauseMenu?.Pause();
        }
        else
        {
            _gamePaused = false;
            PauseMenu?.Continue();
        }
    }

    public void WinGame()
    {
        WinScreen.Pause();
    }
}
