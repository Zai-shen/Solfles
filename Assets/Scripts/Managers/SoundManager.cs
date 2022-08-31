using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _globalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectVolumeSlider;

    private void Start()
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
        PlayerPrefs.SetFloat("MainVolume", _globalVolumeSlider.value);
    }  
    
    public void ChangeMusicVolume()
    { 
        AudioManager.Instance.GlobalMixer.SetFloat("MusicVolume", _musicVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
    }   
    
    public void ChangeEffectVolume()
    { 
        AudioManager.Instance.GlobalMixer.SetFloat("EffectVolume", _effectVolumeSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", _effectVolumeSlider.value);
    }

    public void Load()
    {
        float _mainVolume = PlayerPrefs.GetFloat("MainVolume");
        float _musicVolume= PlayerPrefs.GetFloat("MusicVolume");
        float _effectVolume = PlayerPrefs.GetFloat("EffectVolume");
        
        LoadMixers(_mainVolume, _musicVolume, _effectVolume);
        LoadUI(_mainVolume, _musicVolume, _effectVolume);
    }

    private void LoadMixers(float mainVol, float musicVol, float effectVol)
    {
        AudioManager.Instance.GlobalMixer.SetFloat("MainVolume", mainVol);
        AudioManager.Instance.GlobalMixer.SetFloat("MusicVolume", musicVol);
        AudioManager.Instance.GlobalMixer.SetFloat("EffectVolume", effectVol);
    }
    
    private void LoadUI(float mainVol, float musicVol, float effectVol)
    {
        if (!_globalVolumeSlider && _musicVolumeSlider && _effectVolumeSlider)
            return;
        
        _globalVolumeSlider.value = mainVol;
        _musicVolumeSlider.value = musicVol;
        _effectVolumeSlider.value = effectVol;
    }
    
    public float VolToAttenuation(float vol)
    {
        return (1f - Mathf.Sqrt(vol)) * -80f;
    }
    
    public float AttenuationToVol(float attenuation)
    {
        return Mathf.Pow(-((attenuation / -80f) - 1f), 2f);
    }
}
