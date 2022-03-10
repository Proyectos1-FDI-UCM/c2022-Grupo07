using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    #region methods
    public void MainMenu() //Carga la escena del tutorial (primer nivel)
    {
        SceneManager.LoadScene("MainMenu");// Al darle al start, te mete en el tutorial.
    }
    public void Quit() //Cierra la aplicación
    {
        Application.Quit();
    }
    #endregion
}
