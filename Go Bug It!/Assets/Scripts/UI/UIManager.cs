using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region references
    // Tiempo
    private Text _timeText;
    // Puntos
    private Text _pointsText;
    // Disparos
    [SerializeField] private GameObject _shotsUI;
    [SerializeField] private Sprite _gravActivated;
    [SerializeField] private Sprite _gravDeactivated;
    [SerializeField] private Sprite _neuActivated;
    [SerializeField] private Sprite _neuDeactivated;
    // PowerUp
    private Slider _powerupSlider;
    private GameObject _powerupObject;
    // Pausa
    [SerializeField] private GameObject _pauseMenu;
    private PauseMenu _pauseFirstScreen;
    #endregion

    #region parameters
    private Image[] _hearts= new Image[4];
    private Image[] _shots = new Image[2];
    private int _points = 0;
    #endregion

    #region methods
    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 0)                                                 // Comprobar si está entre los límites de vida posible del jugador
        {
            UIAnimationController heart = _hearts[life].GetComponent<UIAnimationController>();

            if (powerup == false) heart.UpdateStatus(false);                        // Si se resta vida [ _hearts[life].sprite = _emptyHeartImg; ]
            else heart.UpdateStatus(true);                                          // Si se suma con un powerup [ _hearts[life].sprite = _fullHeartImg; ] 
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
        _points += newPoints;                                                           // Sumar puntuación
        string zerosText = "";                                                          // Texto vacío (contenedor de ceros)
        if (_points > 9999) _points = 9999;
        else
        {
            int maxZeros = 5;                                                           // Variable de control del nº de ceros máximo
            string pointsText = "" + _points;                                           // Contenedeor para el nº de dígitos de _points
            for (int i = maxZeros; i > pointsText.Length + 1; i--) zerosText += "0";    // Añadir el nº de ceros correcto
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

    // Activa o desactiva el slider
    public void SetSliderActive(bool active)
    {
        _powerupObject.SetActive(active);
    }

    // Actualizar el máximo valor del powerup
    public void SetMaxSliderValue(float value)
    {
        _powerupSlider.maxValue = value;
    }

    // Actualizar el slider 
    public void UpdatePowerUpSlider(float value)
    {
        _powerupSlider.value = value;
    }

    // Activa o desactiva el menu de pausa en función de su estado anterior
    public void Pause()
    {
        if (!_pauseMenu.activeInHierarchy) _pauseMenu.SetActive(true);
        else if (_pauseMenu.activeInHierarchy)
        {
            _pauseFirstScreen.ExitingPause();
            _pauseMenu.SetActive(false);
        }
    }
    #endregion

    // Initializes own references
    void Awake()
    {
        // Inicializar vida
        for (int i = 0; i < _hearts.Length; i++) _hearts[i] = gameObject.transform.GetChild(1).GetChild(i).GetComponent<Image>();

        // Inicializar temporizador
        _timeText = gameObject.transform.GetChild(2).GetChild(1).GetComponent<Text>();

        // Inicializar puntos
        _pointsText = gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>();

        // Inicializar disparos
        for (int i = 0; i < _shots.Length; i++) _shots[i] = gameObject.transform.GetChild(4).GetChild(i).GetComponent<Image>();

        // Inicializar slider de powerup y lo desactiva
        _powerupObject = gameObject.transform.GetChild(5).gameObject;
        _powerupObject.SetActive(false);
        _powerupSlider = _powerupObject.transform.GetChild(0).GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _pauseFirstScreen = _pauseMenu.transform.GetChild(0).GetComponent<PauseMenu>();
    }
}
