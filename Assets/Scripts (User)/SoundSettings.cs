using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private AudioMixerGroup _masterAudioGroup;

    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private AudioMixerGroup _SFXAudioGroup;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private AudioMixerGroup _musicAudioGroup;




    private void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        SetMasterVolumeLevel();
        SetSFXVolumeLevel();
        SetMusicVolumeLevel();
    }

    public void SetMasterVolumeLevel()
    {
        _mixer.SetFloat(_masterAudioGroup.name, Mathf.Log10(_masterSlider.value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
        PlayerPrefs.Save();
    }
    public void SetSFXVolumeLevel()
    {
        _mixer.SetFloat(_SFXAudioGroup.name, Mathf.Log10(_SFXSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", _SFXSlider.value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolumeLevel()
    {
        _mixer.SetFloat(_musicAudioGroup.name, Mathf.Log10(_musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.Save();
    }
}

