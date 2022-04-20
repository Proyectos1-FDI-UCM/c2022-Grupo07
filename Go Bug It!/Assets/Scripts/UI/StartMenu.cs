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
    private GameObject _message;
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

    // Animaci�n secuencial de splashscreen
    IEnumerator StartScene()
    {
        // Animaci�n del logo de �intendo
        _�intendo.GetComponent<Animator>().SetBool("Go�intendo", true);

        // Animaci�n del r�tulo de autores
        yield return new WaitForSeconds(3.5f);
        _authors.GetComponent<Animator>().SetBool("GoAuthors", true);

        // Animaci�n de fundido a blanco
        yield return new WaitForSeconds(3.2f);
        _initialFlash.GetComponent<Animator>().SetBool("GoFlash", true);

        yield return new WaitForSeconds(0.5f);
        _authors.SetActive(false);
        _�intendo.SetActive(false);
        _initialFlash.SetActive(false);
        // Asignar el bot�n inicial del men�
        EventSystem.current.SetSelectedGameObject(_startFirtsButton);
    }
    #endregion

    void Start()
    {
        // Obtener GameObjects del men�
        _message = transform.GetChild(3).gameObject;
        _initialFlash = transform.GetChild(4).gameObject;
        _�intendo = transform.GetChild(5).gameObject;
        _authors = transform.GetChild(6).gameObject;

        // Asegurarse de que no se selecciona ning�n bot�n de base
        EventSystem.current.SetSelectedGameObject(null);

        // Iniciar animaci�n de splashscreen
        StartCoroutine(StartScene());
    }

    void Update()
    {
        // if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.G)) SceneManager.LoadScene("Debug");
        if ((Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.N)) || ((Input.GetKeyDown(KeyCode.JoystickButton4) && Input.GetKeyDown(KeyCode.JoystickButton5)))) SceneManager.LoadScene("Debug");
    }
}
