using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Add listeners for the sliders
        masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });


        // Optional: Initialize sliders based on current mixer settings
        float musicVol, sfxVol, masterVol;
        mainMixer.GetFloat("MasterVolume", out masterVol);
        mainMixer.GetFloat("MusicVolume", out musicVol);
        mainMixer.GetFloat("SFXVolume", out sfxVol);

        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
    }

    public void SetMasterVolume()
    {
        mainMixer.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        mainMixer.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        mainMixer.SetFloat("SFXVolume", sfxSlider.value);
    }
}
