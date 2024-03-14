using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{

    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer Mixer;

    private const float DefaultVolume = 0.5f;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            MusicVolume();
            SFXVolume();
        }
    }

    void PutDefaultVolume()
    {
        MusicSlider.value = DefaultVolume;
        SFXSlider.value = DefaultVolume;
        MusicVolume();
        SFXVolume();
    }


    public void MusicVolume()
    {
        float volume = MusicSlider.value;
        Mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SFXVolume()
    {
        float volume = SFXSlider.value;
        Mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", DefaultVolume);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", DefaultVolume);

        MusicVolume();
        SFXVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.Save();
    }
}

