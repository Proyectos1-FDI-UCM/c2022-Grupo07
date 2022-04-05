using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    #region references
    private GameObject _creditsTextObject;
    private Transform _creditsTextTransform;
    private GameObject _collectiblesObject;
    private Text _collectiblesMessage;
    private GameObject _thanks4playing;
    private GameObject _altF4;
    private AudioSource _audioSrc;
    #endregion

    #region parameters
    [SerializeField] private float _creditsSpeed;
    private Vector3 _yVector = new Vector3(0, 10, 0);
    #endregion

    #region methods
    // Según los coleccionables obtenidos cambia el texto de recompensa de los mismos
    public void ShowCollectibles()
    {
        int[] _collectibles = GameManager.Instance.GetCollectibles();
        int _nCollect = 0;

        for (int i = 0; i < _collectibles.Length; i++)
        {
            if (_collectibles[i] == 1) _nCollect++;
        }

        switch(_nCollect)
        {
            case 0: _collectiblesMessage.text = "You are a begginner programmer"; break;
            case 1: _collectiblesMessage.text = "You are an Amateur programmer"; break;
            case 2: _collectiblesMessage.text = "You are a Junior programmer"; break;
            case 3: _collectiblesMessage.text = "You are a Senior programmer"; break;
            case 4: _collectiblesMessage.text = "You are a MASTER programmer"; break;
        }
    }

    // Gestión del tiempo al enseñar la pantalla de créditos para secuenciar comportamientos
    IEnumerator ShowVictoryScreen()
    {
        //Esperamos un tiempo a que acaben los créditos
        yield return new WaitForSeconds(40);
        _creditsTextObject.SetActive(false);

        // Activamos los siguientes objetos secuencialmente
        _thanks4playing.SetActive(true);
        yield return new WaitForSeconds(1);
        ShowCollectibles();
        _collectiblesObject.SetActive(true);
        _altF4.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _altF4.GetComponent<Animator>().SetTrigger("Idle");

        // Desactivamos objetos y activamos la pantalla de error de Windows
        yield return new WaitForSeconds(2);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        Camera.current.GetComponent<AudioSource>().Stop();
        _audioSrc.Play();
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        // Esperamos un tiempo hasta después cerrar la aplicación
        yield return new WaitForSeconds(30);
        Application.Quit();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar créditos
        _creditsTextObject = transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        _creditsTextTransform = _creditsTextObject.transform;

        // Inicializar collecionables
        _collectiblesObject = transform.GetChild(0).GetChild(0).GetChild(5).gameObject;
        _collectiblesMessage = _collectiblesObject.GetComponent<Text>();
        _collectiblesObject.SetActive(false);

        // Inicializar thnaks4playing
        _thanks4playing = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        _thanks4playing.SetActive(false);

        // Inicializar imagen AltF4
        _altF4 = transform.GetChild(0).GetChild(0).GetChild(4).gameObject;
        _altF4.SetActive(false);

        // Inicializar audiosource
        _audioSrc = GetComponent<AudioSource>();

        // Iniciar corrutina
        StartCoroutine(ShowVictoryScreen());
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento vertical de los créditos
        if (_creditsTextObject.activeInHierarchy) _creditsTextTransform.Translate(_yVector * _creditsSpeed * Time.deltaTime);
    }
}
