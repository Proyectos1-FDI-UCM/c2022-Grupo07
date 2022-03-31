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
    // Actualiza el valor del volumen de la musica
    public void SetVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    #endregion

}
