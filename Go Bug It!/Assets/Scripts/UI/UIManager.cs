using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    #region references
    // Esquina
    private GameObject _shotsCorner;
    // Vida
    private Image[] _hearts = new Image[4];
    // Tiempo
    private Text _timeText;
    private Animator _timeAnim;
    // Puntos
    private Text _pointsText;
    // Disparos
    private Image[] _shotsImg = new Image[3];
    private GameObject[] _shotsObj = new GameObject[3];
    [SerializeField] private Sprite[] _shotsInds = new Sprite[4]; // [0, 1] grav actv/deactv - [2, 3] neu actv/deactv
    // Dash
    private GameObject _dashInd;
    [SerializeField] private Sprite[] _dashIndImg = new Sprite[2]; // [0] activado - [1] desactivado
    // PowerUp
    [SerializeField] private Sprite[] _powerupImg = new Sprite[4]; // [0] private shield -  [1] x3 - [2] stackpointer - [3] spam
    private GameObject _powerupObject;
    private Slider _powerupSlider;
    private Image _powerupIcon;
    // Pausa
    [SerializeField] private GameObject _pauseMenu;
    private PauseMenu _pauseFirstScreen;
    // TIME!
    private GameObject _TIME;
    // Transitions
    private GameObject _damageTransition;
    private GameObject _endTransition;    
    #endregion

    #region parameters
    private int _points = 0;
    #endregion

    #region methods
    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 0)                                                 // Comprobar si est� entre los l�mites de vida posible del jugador
        {
            Animator _heartAnim = _hearts[life].GetComponent<Animator>();

            if (powerup == false) // Si se resta vida [ _hearts[life].sprite = _emptyHeartImg; ]
            {
                _heartAnim.SetBool("Hurted", true);
                if (life > 0) StartCoroutine(DamageAnimation());
            }
            else _heartAnim.SetBool("Hurted", false); ;                             // Si se suma con un powerup [ _hearts[life].sprite = _fullHeartImg; ] 
        }
    }

    // Reproducci�n de la animaci�n
    IEnumerator DamageAnimation()
    {
        _damageTransition.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        _damageTransition.SetActive(false);
    }

    // Actualizar cron�metro
    public void UpdateTime(int time)
    {
        // Mantener ceros a la izquierda seg�n el n� de d�gitos
        if (time >= 100) _timeText.text = "" + time;
        else if (time >= 10)
        {
            _timeText.text = "0" + time;
            if (time < 30) _timeAnim.SetBool("Bellow", true);
        }
        else _timeText.text = "00" + time;
    }

    // Actualizar puntos
    public void UpdatePoints(int newPoints)
    {
        _points += newPoints;                                                           // Sumar puntuaci�n
        string zerosText = "";                                                          // Texto vac�o (contenedor de ceros)
        if (_points > 9999) _points = 9999;
        else
        {
            int maxZeros = 5;                                                           // Variable de control del n� de ceros m�ximo
            string pointsText = "" + _points;                                           // Contenedeor para el n� de d�gitos de _points
            for (int i = maxZeros; i > pointsText.Length + 1; i--) zerosText += "0";    // A�adir el n� de ceros correcto
        }

        // Aplicar suma en la UI
        _pointsText.text = "Puntos: " + zerosText + _points;
    }

    // Actualizar puntos cuando muere el jefe
    public void UpdatePointsBoss()
    {
        _pointsText.text = "&/$(%�&/o:";
    }

    // Cambiar de disparo
    public void UpdateShot(int shot)
    {
        if (shot == 0) // Si ten�a el disparo nuetralizador activado
        {
            _shotsImg[0].sprite = _shotsInds[0]; // Disparo gravedad activado
            _shotsImg[1].sprite = _shotsInds[3]; // Disparo neutralizador desactivado
        }
        else // Si ten�a el disparo gravitatorio activado
        {
            _shotsImg[0].sprite = _shotsInds[1]; // Disparo gravedad desactivado
            _shotsImg[1].sprite = _shotsInds[2]; // Disparo neutralizador activado
        }
    }

    // Activa o desactiva el slider
    public void SetSliderActive(bool active)
    {
        _powerupObject.SetActive(active);
    }

    // Actualizar el m�ximo valor del powerup
    public void SetMaxSliderValue(float value)
    {
        _powerupSlider.maxValue = value;
    }

    // Actualizar el valor del slider 
    public void UpdatePowerUpSlider(float value)
    {
        _powerupSlider.value = value;
    }

    // Activa o desactiva el menu de pausa en funci�n de si estaba o no activado previamente
    public void Pause()
    {
        if (!_pauseMenu.activeInHierarchy) _pauseMenu.SetActive(true);
        else if (_pauseMenu.activeInHierarchy)
        {
            _pauseFirstScreen.ExitingPause();
            _pauseMenu.SetActive(false);
        }
    }

    // Llama a la corrutina del rotulo TIME!
    public void Time()
    {
        StartCoroutine(TimeCoroutine());
    }

    // Activa el rotulo TIME!, espera y carga la escena de GameOver
    IEnumerator TimeCoroutine()
    {
        _TIME.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }

    // Cambia el sprite del indicador del dash seg�n si se puede o no hacer
    public void UpdateDashIndicator(bool canDo)
    {
        if (canDo) _dashInd.GetComponent<Image>().sprite = _dashIndImg[0];
        else _dashInd.GetComponent<Image>().sprite = _dashIndImg[1];
    }
    
    // Activa el indicador del disparo de da�o y desactiva el resto
    public void DmgShootActivate()
    {
        // Desactivar disparos de gravedad y neutralizar
        _shotsObj[0].SetActive(false);
        _shotsObj[1].SetActive(false);

        // Activar disparo de da�o
        _shotsObj[2].SetActive(true);

        // Reposicionar la esquina y el indicador de dash y aplicar animaciones
        _shotsCorner.transform.localPosition = new Vector3(1040, -490, 0);
        _shotsCorner.GetComponent<Animator>().SetTrigger("DmgShoot");
        _dashInd.transform.localPosition = new Vector3(813, 46, 0);
    }

    // Cambia el sprite del icono del powerup seg�n el valor recibido
    public void UpdatePowerUpIcon(int powerup)
    {
        _powerupIcon.sprite = _powerupImg[powerup];
    }

    // Aplica la animaci�n de fin de nivel
    public void SetTransition(bool fin)
    {
        _endTransition.GetComponent<Animator>().SetBool("End", fin);
    }
    #endregion

    // Initializes own references
    void Awake()
    {
        // Inicializar esquina derecha para su posterior manipulaci�n
        _shotsCorner = gameObject.transform.GetChild(0).GetChild(3).gameObject;

        // Inicializar vida
        for (int i = 0; i < _hearts.Length; i++) _hearts[i] = gameObject.transform.GetChild(1).GetChild(i).GetComponent<Image>();

        // Inicializar temporizador
        _timeText = gameObject.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        _timeAnim = gameObject.transform.GetChild(2).GetChild(1).GetComponent<Animator>();

        // Inicializar puntos
        _pointsText = gameObject.transform.GetChild(3).GetComponent<Text>();

        // Inicializar disparos
        for (int i = 0; i < _shotsImg.Length; i++)
        {
            _shotsObj[i] = gameObject.transform.GetChild(4).GetChild(i).gameObject; // Objeto
            _shotsImg[i] = _shotsObj[i].GetComponent<Image>();                      // Imagen del disparo
        }
        _shotsObj[2].SetActive(false); // Desactivar disparo damage

        // Inicializar dash
        _dashInd = gameObject.transform.GetChild(4).GetChild(3).gameObject;

        // Inicializar slider de powerup, imagen de powerup y desactivarlos
        _powerupObject = gameObject.transform.GetChild(5).gameObject;
        _powerupObject.SetActive(false);
        _powerupSlider = _powerupObject.transform.GetChild(0).GetComponent<Slider>();
        _powerupIcon = _powerupObject.transform.GetChild(1).GetComponent<Image>();

        // Inicializar rotulo TIME! y desactivarlo
        _TIME = gameObject.transform.GetChild(6).gameObject;
        _TIME.SetActive(false);

        // Inicializar transiciones de da�o y fin
        _damageTransition = transform.GetChild(7).gameObject;
        _damageTransition.SetActive(false);
        _endTransition = transform.GetChild(8).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar el men� de pausa
        _pauseFirstScreen = _pauseMenu.transform.GetChild(0).GetComponent<PauseMenu>();
    }
}
