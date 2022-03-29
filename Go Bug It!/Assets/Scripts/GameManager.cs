using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    // Temporizador del nivel
    private float _timeLeft;
    public float _slowtimeFactor;
    #endregion

    #region properties
    private int[] _collectibles = { 0, 0, 0, 0 }; // 0 = no obtenido, 1 = obtenido
    [HideInInspector] public bool _spam;
    [HideInInspector] public float _speedmod;
    #endregion

    #region references
    // Patrón singleton
    static private GameManager _instance;
    static public GameManager Instance { get { return _instance; } }
    // UI
    [SerializeField] private GameObject _myUIObject;
    private UIManager _myUIManager;
    // Pausa
    [SerializeField] private GameObject _myPauseObject;
    private PauseMenu _myPause;
    // Jugador
    [SerializeField] private GameObject _player;
    private PlayerLifeComponent _myplayerLife;
    // Boss
    private Boss_life_controller _boss;
    // GameOver
    // [SerializeField] private GameObject ;
    // private GameOver _gameOverScreen;
    #endregion

    #region methods
    public void Spammed()
    {
        if (_spam)
        {
            _speedmod = _slowtimeFactor;
        }
        else _speedmod = 1;
    }

    //Espera hasta que termine la animación de muerte
    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("GameOver");
    }

    // Avance de nivel
    public void OnGoalAdvance(string escena, float newTime)
    {
        _timeLeft = newTime;
        _myplayerLife.FullyHealing();
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(_myUIManager.gameObject);
        DontDestroyOnLoad(_myPauseObject.gameObject);
        SceneManager.LoadScene(escena);
    }

    //Registro del jugador en el GameManager
    public void PlayerRegistration(PlayerLifeComponent player)
    {
        _myplayerLife = player;
    }

    // Muerte de un enemigo
    public void OnEnemyDies (int _puntuation)
    {
        _myUIManager.UpdatePoints(_puntuation);
    }

    //Muerte del jefe
    public void OnBossDies()
    {
        _myUIManager.UpdatePointsBoss();
    }
    
    // Daño al jugador
    public void OnPlayerDamage(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints, false);
    }

    // Cambio de disparo
    public void OnChangingShoot(int shot)
    {
        _myUIManager.UpdateShot(shot);
    }

    public void OnDmgShootActivate()
    {
        _myUIManager.DmgShootActivate();
    }

    //Pausa el juego y abre el menu de pausa
    public void Pause(bool pause)
    {
        _myUIManager.Pause();
        if (pause == true) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    // Cerrar juego
    public void Quit()
    {
        Application.Quit();
    }

    // Curación del jugador
    public void OnPlayerHeals(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints-1, true);
    }

    // Muerte del jugador. Inicia la animación de muerte y marca la escena actual como la de reintento
    public void OnPlayerDies()
    {
        // _gameOverScreen.SetRetryScene(SceneManager.GetActiveScene().name);
        if (_timeLeft > 0) StartCoroutine(WaitDeath()); 
        else _myUIManager.Time();
        
    }

    // Llama a la UI para actualizar el indicador del dash según si está o no disponible
    public void OnDashUpdate(bool canDo)
    {
        _myUIManager.UpdateDashIndicator(canDo);
    }

    // Cuando se desactiva o activa un powerup, ajusta todos los componentes correspondientes
    public void OnPowerUpActivate(float value, bool active, int powerup)
    {
        _myUIManager.SetMaxSliderValue(value);
        _myUIManager.SetSliderActive(active);
        if(active) _myUIManager.UpdatePowerUpIcon(powerup);
    }

    // Actualizar el valor del powerup mientras este está activo
    public void WhilePowerUpActive(float value)
    {
        _myUIManager.UpdatePowerUpSlider(value);
    }
    #endregion

    #region properties
    //[HideInInspector]public bool _spam;
    //[HideInInspector]public float _speedmod;

    // Actualiza el array de coleccionables y lo aplica en el menú de pausa
    public void OnCollectiblePicked(int posicion)
    {
        _collectibles[posicion] = 1;
        _myPause.ActivateCollectibles(_collectibles);
    }

    // Devuelve el array de coleccionables
    public int[] GetCollectibles()
    {
        return _collectibles;
    }
    #endregion

    // Initializes GameManager instance.-
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _spam = false;
        
        _timeLeft = 300.0f;
        _myUIManager = _myUIObject.GetComponent<UIManager>();
        _myPause = _myPauseObject.transform.GetChild(0).GetComponent<PauseMenu>();
        _myPauseObject.SetActive(false);
        _boss = gameObject.GetComponent<Boss_life_controller>();
        // _gameOverScreen = _myGameOver.GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeLeft <= 0) OnPlayerDies();
        else
        {
            _timeLeft -= Time.deltaTime;
            _myUIManager.UpdateTime((int)_timeLeft);
        }
        Spammed();//Comprueba constantemente si el spam está activado
    }
}
