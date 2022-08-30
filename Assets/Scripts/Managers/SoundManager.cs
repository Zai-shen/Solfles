using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider _globalVolumeSlider;
    [SerializeField] Slider _musicVolumeSlider;
    [SerializeField] Slider _effectVolumeSlider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("MainVolume"))
        {
            PlayerPrefs.SetFloat("MainVolume", 0);
        }
        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0);
        }
        if(!PlayerPrefs.HasKey("EffectVolume"))
        {
            PlayerPrefs.SetFloat("EffectVolume", 0);
        }

        Load();
    }
    
    public void ChangeMainVolume()
    { 
        AudioManager.Instance.GlobalMixer.SetFloat("MainVolume", _globalVolumeSlider.value);
        Save();
    }  
    
    public void ChangeMusicVolume()
    { 
        AudioManager.Instance.GlobalMixer.SetFloat("MusicVolume", _musicVolumeSlider.value);
        Save();
    }   
    
    public void ChangeEffectVolume()
    { 
        AudioManager.Instance.GlobalMixer.SetFloat("EffectVolume", _effectVolumeSlider.value);
        Save();
    }

    public void Load()
    {
        _globalVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume");
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _effectVolumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MainVolume", _globalVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", _effectVolumeSlider.value);
    }
}
