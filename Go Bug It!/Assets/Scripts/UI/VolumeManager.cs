using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{

    #region references
    [SerializeField] private AudioMixer _mixer;
    #endregion

    #region methods
    // Actualiza el valor del volumen general
    public void SetGeneralVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("GeneralVolume", Mathf.Log10(sliderValue) * 20);
    }

    // Actualiza el valor del volumen de la musica
    public void SetMusicVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    // Actualiza el valor del volumen del sonido
    public void SetSoundVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
    #endregion

}
