using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _startFirtsButton;
    #endregion

    #region methods
    //Carga la escena del tutorial (primer nivel)
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    //Cierra el juego
    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    void Start()
    {
        // Asegurarse de que no se selecciona ningún botón de base
        EventSystem.current.SetSelectedGameObject(null);

        // Asignar el botón inicial del menú
        EventSystem.current.SetSelectedGameObject(_startFirtsButton);
    }
}
