using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PauseMenu PauseMenu;
    public PauseMenu WinScreen;
    private bool _gamePaused;
    private Transform _player;
    public float WinRadius = 5f;
    private int _activeScene = 0;
    private bool _audioStarted;
    
    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene().buildIndex;
        if (_activeScene == 1)
        {
            _player = PlayerManager.Instance.Player.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Globals.Friends.Count == 4 && Vector3.Distance(_player.position, Vector3.zero) <= WinRadius)
        {
            foreach (GameObject _friend in Globals.Friends)
            {
                if (!(Vector3.Distance(_friend.transform.position, Vector3.zero) <= WinRadius))
                    return;
            }

            WinGame();
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
