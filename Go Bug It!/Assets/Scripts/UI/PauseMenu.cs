using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _firstPauseButton;
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

    }

    // Mostrar las opciones
    public void OptionsMenu()
    {

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Asegurarse de que no se selecciona ning�n bot�n de base
        EventSystem.current.SetSelectedGameObject(null);

        // Asignar el bot�n inicial del men�
        EventSystem.current.SetSelectedGameObject(_firstPauseButton);
    }
}
