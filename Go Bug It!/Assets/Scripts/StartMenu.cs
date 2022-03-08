using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    #region methods
    public void StartGame()//Carga la escena del tutorial (primer nivel)
    {
        SceneManager.LoadScene("Sprint 2");// poner el nombre de la escena tutorial cuando la tengamos hecha
    }
    public void Quit()//Cierra la aplicación
    {
        Application.Quit();
    }
    #endregion
}
