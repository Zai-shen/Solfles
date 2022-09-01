using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player Player;
    public LoseMenu DeathScreen;

    #region Singleton
    
    private static PlayerManager _playerManager;
    public static PlayerManager Instance
    {
        get
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;

                if (!_playerManager)
                    Debug.LogError("There needs to be one active PlayerManager script on a GameObject in your scene.");
            }

            return _playerManager;
        }
        set
        {
            
        }
    }

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void OnDeath()
    {
        DeathScreen.Pause();
        AudioManager.Instance.StopPlaying();
        AudioManager.Instance.PlayContinuous(AudioManager.Instance.MusicClips[3]);
    }

    private void Start()
    {
        if (DeathScreen)
        {
            Player.Health.OnDeath += OnDeath;
        }
    }
    
    private void OnDisable()
    {
        if (DeathScreen)
        {
            Player.Health.OnDeath -= OnDeath;
        }
    }
}
