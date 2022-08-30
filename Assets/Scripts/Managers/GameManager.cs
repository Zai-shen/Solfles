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
    private bool _didWin;

    public Action ResetPause;

    #region Singleton
    
    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get
        {
            if (!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!_gameManager)
                    Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
            }

            return _gameManager;
        }
        private set
        {
            
        }
    }

    #endregion
    
    private void OnEnable()
    {
        ResetPause += ResetPauseState;
    }

    private void OnDisable()
    {
        ResetPause -= ResetPauseState;
    }
    
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
        
        if (!_didWin && Globals.Friends.Count == 4 && Vector3.Distance(_player.position, Vector3.zero) <= WinRadius)
        {
            foreach (GameObject _friend in Globals.Friends)
            {
                if (!(Vector3.Distance(_friend.transform.position, Vector3.zero) <= WinRadius))
                    return;
            }

            WinGame();
        }
    }

    private void ResetPauseState()
    {
        _gamePaused = false;
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
        _didWin = true;
        WinScreen.Pause();
    }
    
    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, WinRadius);
    }
}
