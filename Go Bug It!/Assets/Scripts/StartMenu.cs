using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    #region methods
    public void StartGame() //Carga la escena del tutorial (primer nivel)
    {
        SceneManager.LoadScene("Tutorial");// Al darle al start, te mete en el tutorial.
    }
    public void Quit() //Cierra la aplicación
    {
        Application.Quit();
    }
    #endregion
}
