using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region references
    // Vida
    [SerializeField] private GameObject _lifeUI;
    [SerializeField] private Sprite _fullHeartImg;
    [SerializeField] private Sprite _emptyHeartImg;
    // Tiempo
    [SerializeField] private GameObject _timeUI;
    private Text _timeText;
    [SerializeField] private Sprite _timer;
    // Puntos
    [SerializeField] private GameObject _pointsUI;
    private Text _pointsText;
    // Disparos
    [SerializeField] private GameObject _shotsUI;
    [SerializeField] private Sprite _gravActivated;
    [SerializeField] private Sprite _gravDeactivated;
    [SerializeField] private Sprite _neuActivated;
    [SerializeField] private Sprite _neuDeactivated;
    // Recuadros
    [SerializeField] private GameObject _cornersUI;
    [SerializeField] private Sprite _corner;
    #endregion

    #region parameters
    private Image[] _hearts= new Image[4];
    private Image[] _shots = new Image[2];
    private Image[] _corners = new Image[4];
    private int _points = 0;
    #endregion

    #region methods
    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 0) // Comprobar si está entre los límites de vida posible del jugador
        {
            UIAnimationController heart = _hearts[life].GetComponent<UIAnimationController>();

            if (powerup == false) heart.UpdateStatus(false); // Si se resta vida [ _hearts[life].sprite = _emptyHeartImg; ]
            else heart.UpdateStatus(true); // Si se suma con un powerup [ _hearts[life].sprite = _fullHeartImg; ] 
        }
    }

    // Actualizar cronómetro
    public void UpdateTime(int time)
    {
        // Mantener ceros a la izquierda según el nº de dígitos
        if (time >= 100) _timeText.text = "" + time;
        else if (time >= 10) _timeText.text = "0" + time;
        else _timeText.text = "00" + time;
    }

    // Actualizar puntos
    public void UpdatePoints(int newPoints)
    {
        _points += newPoints; // Sumar puntuación
        string zerosText = ""; // Texto vacío (contenedor de ceros)
        if (_points > 9999) _points = 9999;
        else
        {
            int maxZeros = 5; // Variable de control del nº de ceros máximo
            string pointsText = "" + _points; // Contenedeor para el nº de dígitos de _points
            for (int i = maxZeros; i > pointsText.Length + 1; i--) zerosText += "0"; // Añadir el nº de ceros correcto
        }

        // Aplicar suma en la UI
        _pointsText.text = "Puntos: " + zerosText + _points;
    }

    // Cambiar de disparo
    public void UpdateShot(int shot)
    {
        if (shot == 0)
        {
            _shots[0].sprite = _gravActivated;
            _shots[1].sprite = _neuDeactivated;
        }
        else
        {
            _shots[0].sprite = _gravDeactivated;
            _shots[1].sprite = _neuActivated;
        }
    }
    #endregion

    // Initializes own references
    void Awake()
    {
        // Inicializar recuadros
        for(int i = 0; i < _corners.Length; i++)
        {
            _corners[i] = _cornersUI.transform.GetChild(i).GetComponent<Image>();
            _corners[i].sprite = _corner;
        }

        // Inicializar vida
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i] = _lifeUI.transform.GetChild(i).GetComponent<Image>();
            _hearts[i].sprite = _fullHeartImg;
        }

        // Inicializar temporizador
        _timeText = _timeUI.transform.GetChild(1).GetComponent<Text>();
        _timeUI.transform.GetChild(0).GetComponent<Image>().sprite = _timer;

        // Inicializar puntos
        _pointsText = _pointsUI.transform.GetChild(0).GetComponent<Text>();

        // Inicializar disparos
        for (int i = 0; i < _shots.Length; i++)
        {
            _shots[i] = _shotsUI.transform.GetChild(i).GetComponent<Image>();
            if (i == 0) _shots[i].sprite = _gravActivated; // Disparo gravedad
            else if (i == 1) _shots[i].sprite = _neuDeactivated; // Disparo neutralizado
        }
    }
}
