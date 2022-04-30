using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour
{

    #region references
    [SerializeField] private GameObject _gameOverFirstButton;
    private GameObject _retryButton;
    private GameObject _retryText;
    #endregion

    #region parameters
    private string _retryScene = "Tutorial";
    #endregion

    #region methods
    // Carga el men�
    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Asigna el nombre de las escena en la que mueres para despu�s cargar la correcta
    public void SetRetryScene(string name)
    {
        if (name != "") _retryScene = name;
        _retryText.transform.GetChild(0).GetComponent<Text>().text += " (" + _retryScene + ")";
        _retryText.transform.GetChild(1).GetComponent<Text>().text += " (" + _retryScene + ")";
    }

    // Carga la escena correspondiente
    public void Retry()
    {
        GameManager.Instance.OnRetry(_retryScene);
    }

    // Cierra la aplicaci�n
    public void Quit() 
    {
        Application.Quit();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Asegurarse de que no se selecciona ning�n bot�n de base y despu�s asignar el deseado
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_gameOverFirstButton);

        // Inicializar bot�n de reintentar
        _retryButton = gameObject.transform.GetChild(1).GetChild(1).gameObject;

        // Inicializar y desactivar explicaci�n
        _retryText = gameObject.transform.GetChild(2).gameObject;
        _retryText.SetActive(false);

        // Actualizar el valor de la escena a recargar
        SetRetryScene(GameManager.Instance.GetScene());
    }

    // Update is called once per frame
    void Update()
    {
        // Si el bot�n seleccionado es el bot�n de reintentar, mostrar mensaje
        if (EventSystem.current.currentSelectedGameObject == _retryButton) _retryText.SetActive(true);
        else
        {
            if (_retryText.activeInHierarchy) _retryText.SetActive(false); // Desactivar en caso contrario
        }
    }
}
