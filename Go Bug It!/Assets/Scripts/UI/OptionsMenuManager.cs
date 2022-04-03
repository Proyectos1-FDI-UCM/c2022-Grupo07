using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{

    #region references
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Dropdown _resDropdown;
    #endregion

    #region properties
    Resolution[] _resolutions;
    #endregion

    #region methods
    // Actualiza el valor del volumen general
    public void SetGeneralVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("GeneralVolume", Mathf.Log10(sliderValue) * 20);
    }

    // Actualiza el valor del volumen de la música
    public void SetMusicVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    // Actualiza el valor del volumen del sonido
    public void SetSoundVolumeLevel(float sliderValue)
    {
        _mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

    // Activar o desactivar pantalla completa
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // Inicializar dropdown de resoluciones
    public void ResolutionStart(Resolution[] res)
    {
        List<string> options = new List<string>();

        int _currentRes = 0;
        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].height + " x " + res[i].height + " @" + res[i].refreshRate + "Hz";
            options.Add(option);

            if (res[i].width == Screen.width && res[i].height == Screen.height) _currentRes = i;
        }

        _resDropdown.AddOptions(options);
        _resDropdown.value = _currentRes;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _resolutions = Screen.resolutions;
        _resDropdown.ClearOptions();
        ResolutionStart(_resolutions);
    }
}
