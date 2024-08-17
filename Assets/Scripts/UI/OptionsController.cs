using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public AudioMixer audioMixer;

    private static float mainVolume = 1;
    private static float musicVolume = 1;
    private static float sfxVolume = 1;

    private const string MAIN_VOLUME_PARAM = "MainVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    void Start()
    {
        mainVolumeSlider.value = mainVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
    }

    public void ChangeMainVolume(float volume)
    {
        audioMixer.SetFloat(MAIN_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        mainVolume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        musicVolume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFX_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        sfxVolume = volume;
    }
}
