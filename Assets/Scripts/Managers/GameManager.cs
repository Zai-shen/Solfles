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
    private AudioManager _audioManager;
    private bool _audioStarted;
    
    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene().buildIndex;
        if (_activeScene == 1)
        {
            _player = PlayerManager.Instance.Player.transform;
        }
    }

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (_activeScene == 1)
        {
            if (!_audioStarted && _audioManager.IsPlayingMusic)
            {
                _audioManager.PlayContinuous(_audioManager.AudioClips[0]);
                _audioStarted = true;
            }
            
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
        else if (!_audioStarted)
        {
            _audioManager.PlayContinuous(_audioManager.AudioClips[1]);
            _audioManager.IsPlayingMusic = false;
            _audioStarted = true;
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
