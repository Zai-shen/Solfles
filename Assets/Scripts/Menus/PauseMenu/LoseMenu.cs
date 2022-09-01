using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : PauseMenu
{
    public new void Pause()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
        AudioManager.Instance.StopPlaying();
        AudioManager.Instance.PlayContinuous(AudioManager.Instance.MusicClips[3]);
    }
    
    public new void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        AudioManager.Instance.StopPlaying();
        AudioManager.Instance.PlayContinuous(AudioManager.Instance.MusicClips[1]);
    }
}