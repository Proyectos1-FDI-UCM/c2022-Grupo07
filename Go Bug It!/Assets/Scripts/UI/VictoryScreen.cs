using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    #region references
    private Text _collectiblesMessage;
    private Transform _creditsTextTransform;
    private GameObject _creditsTextObject;
    #endregion

    #region parameters
    [SerializeField] private float _speed;
    private float _elapsedTime;
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
        //Desactivamos los objetos
        Debug.Log(1);
        yield return new WaitForSeconds(45);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        Debug.Log(2);

        //Activamos la imagen de Windows error
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(30);
        
        // Cerrar juego
        Debug.Log(3);
        Application.Quit();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _collectiblesMessage = transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>();
        ShowCollectibles();
        StartCoroutine(ShowVictoryScreen());
        _creditsTextObject = transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        _creditsTextTransform = _creditsTextObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _creditsTextTransform.Translate(new Vector3(0, 10, 0) * _speed * Time.deltaTime);
        _elapsedTime += Time.deltaTime;
        Debug.Log(_elapsedTime);
    }
}
