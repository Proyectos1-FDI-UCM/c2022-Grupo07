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
    // Volver al menú
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
        // Asegurarse de que no se selecciona ningún botón de base
        EventSystem.current.SetSelectedGameObject(null);

        // Asignar el botón inicial del menú
        EventSystem.current.SetSelectedGameObject(_firstPauseButton);
    }
}
