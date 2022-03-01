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
    #endregion

    #region parameters
    private Image[] _heartsImg = new Image[4];
    private Image[] _shots = new Image[2];
    private int _points = 0;
    #endregion

    #region methods
    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 0) // Comprobar si est� entre los l�mites de vida posible del jugador
        {
            if (powerup == false) _heartsImg[life].sprite = _emptyHeartImg; // Si se resta vida
            else _heartsImg[life].sprite = _fullHeartImg; // Si se suma con un powerup
        }
    }

    // Actualizar cron�metro
    public void UpdateTime(int time)
    {
        // Mantener ceros a la izquierda seg�n el n� de d�gitos
        if (time >= 100) _timeText.text = "" + time;
        else if (time >= 10) _timeText.text = "0" + time;
        else _timeText.text = "00" + time;
    }

    // Actualizar puntos
    public void UpdatePoints(int newPoints)
    {
        _points += newPoints; // Sumar puntuaci�n
        string zerosText = ""; // Texto vac�o (contenedor de ceros)
        if (_points > 9999) _points = 9999;
        else
        {
            int maxZeros = 5; // Variable de control del n� de ceros m�ximo
            string pointsText = "" + _points; // Contenedeor para el n� de d�gitos de _points
            for (int i = maxZeros; i > pointsText.Length + 1; i--) zerosText += "0"; // A�adir el n� de ceros correcto
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
    private void Awake()
    {
        // Inicializar vida
        for (int i = 0; i < _heartsImg.Length; i++)
        {
            _heartsImg[i] = _lifeUI.transform.GetChild(i).GetComponent<Image>();
            _heartsImg[i].sprite = _fullHeartImg;
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
