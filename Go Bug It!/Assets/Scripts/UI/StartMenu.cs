using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _startFirtsButton;
    private GameObject _initialFlash;
    private GameObject _�intendo;
    private GameObject _authors;
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

    IEnumerator StartScene()
    {
        // Animaci�n del logo de �intendo
        yield return new WaitForSeconds(0.2f);
        _�intendo.GetComponent<Animator>().SetBool("Go�intendo", true);

        // Animaci�n del r�tulo de autores
        yield return new WaitForSeconds(6.5f);
        _authors.GetComponent<Animator>().SetBool("GoAuthors", true);

        // Animaci�n de fundido a blanco
        yield return new WaitForSeconds(5.3f);
        _initialFlash.GetComponent<Animator>().SetBool("GoFlash", true);

        // Asignar el bot�n inicial del men�
        EventSystem.current.SetSelectedGameObject(_startFirtsButton);
    }
    #endregion

    void Start()
    {
        // Obtener GameObjects del men�
        _initialFlash = transform.GetChild(3).gameObject;
        _�intendo = transform.GetChild(4).gameObject;
        _authors = transform.GetChild(5).gameObject;

        // Asegurarse de que no se selecciona ning�n bot�n de base
        EventSystem.current.SetSelectedGameObject(null);

        // Iniciar animaci�n de splashscreen
        StartCoroutine(StartScene());
    }
}
