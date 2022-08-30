using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player Player;
    public PauseMenu DeathScreen;

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
    }

    #endregion
    
    private void OnEnable()
    {
        if (DeathScreen)
        {
            Player.Health.OnDeath += DeathScreen.Pause;
        }
    }
    
    private void OnDisable()
    {
        if (DeathScreen)
        {
            Player.Health.OnDeath -= DeathScreen.Pause;
        }
    }
}
