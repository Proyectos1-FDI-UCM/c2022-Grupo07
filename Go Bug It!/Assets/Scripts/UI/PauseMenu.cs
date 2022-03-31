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
    [SerializeField] private GameObject _firstPauseButton;
    // Pausa
    [SerializeField] private GameObject _pauseUI, _controllsUI, _optionsUI;
    private Transform _controllsTransform, _optionsTransform;
    #endregion

    #region parameters
    private Image[] _collectiblesImg = new Image[4];
    [SerializeField] private Sprite[] _activated = new Sprite[4];
    [SerializeField] private Sprite[] _deactivated = new Sprite[4];
    #endregion

    #region methods
    // Volver al men�
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Mostrar controles
    public void ControllsMenu()
    {
        _pauseUI.SetActive(false);
        _controllsUI.SetActive(true);
        _controllsTransform.localPosition = new Vector3(0, 0, 0);
    }

    // Mostrar las opciones
    public void OptionsMenu()
    {
        _pauseUI.SetActive(false);
        _optionsUI.SetActive(true);
        _optionsTransform.localPosition = new Vector3(0, 0, 0);
    }

    // Volver del men� de opciones y/o controles al de principal
    public void Back(string option)
    {
        if (option == "Options") // Si estaba en opciones
        {
            _optionsUI.SetActive(false);
            _optionsTransform.localPosition = new Vector3(-1920, 0, 0);
        }
        else // Si estaba en controles
        {
            _controllsUI.SetActive(false);
            _controllsTransform.localPosition = new Vector3(1920, 0, 0);
        }

        // Activar pausa y seleccionar bot�n
        _pauseUI.SetActive(true);
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
        // Inicializar variable de posici�n
        _optionsTransform = _optionsUI.transform;
        _controllsTransform = _controllsUI.transform;

        // Asegurarse de que no se selecciona ning�n bot�n de base
        EventSystem.current.SetSelectedGameObject(null);

        // Asignar el bot�n inicial del men�
        EventSystem.current.SetSelectedGameObject(_firstPauseButton);
    }
}
