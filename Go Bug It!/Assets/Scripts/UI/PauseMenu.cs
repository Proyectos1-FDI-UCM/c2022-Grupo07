using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    #region references
    // Botones EventSystem
    [SerializeField] private GameObject _firstPauseButton;
    // Pausa
    [SerializeField] private GameObject _pauseUI, _controllsUI, _optionsUI;
    private Transform _controllsTransform, _optionsTransform;
    #endregion

    #region methods
    // Volver al menú
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

    public void Back(string option)
    {
        if (option == "Options")
        {
            _optionsUI.SetActive(false);
            _optionsTransform.localPosition = new Vector3(-1920, 0, 0);
        }
        else
        {
            _controllsUI.SetActive(false);
            _controllsTransform.localPosition = new Vector3(1920, 0, 0);
        }

        _pauseUI.SetActive(true);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _optionsTransform = _optionsUI.transform;
        _controllsTransform = _controllsUI.transform;

        // Asegurarse de que no se selecciona ningún botón de base
        EventSystem.current.SetSelectedGameObject(null);

        // Asignar el botón inicial del menú
        EventSystem.current.SetSelectedGameObject(_firstPauseButton);
    }
}
