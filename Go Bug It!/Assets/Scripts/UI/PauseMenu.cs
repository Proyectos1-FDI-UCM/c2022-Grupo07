using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    #region references
    // Botones EventSystem
    [SerializeField] private GameObject _firstPauseButton, _backOptionsButton, _backControllsButton, _optionsButton, _controllsButton;
    // Pausa
    [SerializeField] private GameObject _pauseUI, _controllsUI, _optionsUI;
    private Transform _controllsTransform, _optionsTransform;
    private AudioSource _pauseSFX;
    #endregion

    #region parameters
    private Image[] _collectiblesImg = new Image[4];
    [SerializeField] private Sprite[] _activated = new Sprite[4];
    [SerializeField] private Sprite[] _deactivated = new Sprite[4];
    [SerializeField] private AudioClip _pauseSounds;
    #endregion

    #region methods
    // Volver al men�
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.MainMenu();
        SceneManager.LoadScene("MainMenu");
    }

    // Mostrar controles
    public void ControllsMenu()
    {
        _pauseSFX.PlayOneShot(_pauseSounds);
        _pauseUI.SetActive(false);
        _controllsUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_backControllsButton);
        _controllsTransform.localPosition = new Vector3(0, 0, 0);
    }

    // Mostrar las opciones
    public void OptionsMenu()
    {
        _pauseSFX.PlayOneShot(_pauseSounds);
        _pauseUI.SetActive(false);
        _optionsUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_backOptionsButton);
        _optionsTransform.localPosition = new Vector3(0, 0, 0);
    }

    // Volver del men� de opciones y/o controles al de principal
    public void Back(string option)
    {
        // Activar men� de pausa
        _pauseSFX.PlayOneShot(_pauseSounds);
        _pauseUI.SetActive(true);

        if (option == "Options") // Si estaba en opciones
        { 
            _optionsUI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(_optionsButton);
            _optionsTransform.localPosition = new Vector3(-1920, 0, 0);
        }
        else // Si estaba en controles
        {
            _controllsUI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(_controllsButton);
            _controllsTransform.localPosition = new Vector3(1920, 0, 0);
        }
    }

    // Seleccionar bot�n al entrar a la pausa
    public void EnterPause()
    {
        EventSystem.current.SetSelectedGameObject(_firstPauseButton);
    }

    // Comandos que ejecutar al salir del men� de pausa
    public void ExitingPause()
    {
        Back("Options");
        Back("");
    }

    // Asigna la imagen del coleccionable obtenido a los indicadores de estos
    public void ActivateCollectibles(int[] collectibles)
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (collectibles[i] == 1) _collectiblesImg[i].sprite = _activated[i];
        }
    }
    #endregion

    // Initializes own references
    void Awake()
    {
        // Desactivar UI de controles y opciones
        _controllsUI.SetActive(false);
        _optionsUI.SetActive(false);

        // Inicializar los indicadores de coleccionables a vac�os
        for (int i = 0; i < _collectiblesImg.Length; i++)
        {
            _collectiblesImg[i] = gameObject.transform.GetChild(3).GetChild(i).GetComponent<Image>();
            _collectiblesImg[i].sprite = _deactivated[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PauseRegistration(gameObject);

        // Inicializar variable de posici�n
        _optionsTransform = _optionsUI.transform;
        _controllsTransform = _controllsUI.transform;

        // Asegurarse de que no se selecciona ning�n bot�n de base
        EventSystem.current.SetSelectedGameObject(null);

        _pauseSFX = GetComponent<AudioSource>();
    }
}
