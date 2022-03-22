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
    private GameObject _ñintendo;
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
        // Animación del logo de Ñintendo
        yield return new WaitForSeconds(0.2f);
        _ñintendo.GetComponent<Animator>().SetBool("GoÑintendo", true);

        // Animación del rótulo de autores
        yield return new WaitForSeconds(6.5f);
        _authors.GetComponent<Animator>().SetBool("GoAuthors", true);

        // Animación de fundido a blanco
        yield return new WaitForSeconds(5.3f);
        _initialFlash.GetComponent<Animator>().SetBool("GoFlash", true);

        // Asignar el botón inicial del menú
        EventSystem.current.SetSelectedGameObject(_startFirtsButton);
    }
    #endregion

    void Start()
    {
        // Obtener GameObjects del menú
        _initialFlash = transform.GetChild(3).gameObject;
        _ñintendo = transform.GetChild(4).gameObject;
        _authors = transform.GetChild(5).gameObject;

        // Asegurarse de que no se selecciona ningún botón de base
        EventSystem.current.SetSelectedGameObject(null);

        // Iniciar animación de splashscreen
        StartCoroutine(StartScene());
    }
}
