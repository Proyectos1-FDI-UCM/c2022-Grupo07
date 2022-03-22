using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    // Temporizador del nivel
    [SerializeField] private int _levelDuration;
    private float _timeLeft;
    private int _actualLevel;
    #endregion

    #region references
    // Patrón singleton
    static private GameManager _instance;
    static public GameManager Instance { get { return _instance; } }
    // UI y pausa
    [SerializeField] private GameObject _myUIObject;
    private UIManager _myUIManager;
    [SerializeField] private GameObject _myPauseObject;
    // Jugador
    [SerializeField] private GameObject _player;
    private PlayerLifeComponent _myLife;
    // GameOver
    [SerializeField] private GameObject _myGameOver;
    private GameOver _gameOverScreen;
    #endregion

    #region methods
    //Espera hasta que termine la animación de muerte
    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("GameOver");
    }

    // Avance de nivel
    public void OnGoalAdvance()
    {
        _actualLevel++;
        SceneManager.LoadScene("Level 1");
    }

    // Muerte de un enemigo
    public void OnEnemyDies (int _puntuation)
    {
        _myUIManager.UpdatePoints(_puntuation);
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

    // Cuando se desactiva o activa un powerup
    public void OnPowerUpActivate(float value, bool active)
    {
        _myUIManager.SetMaxSliderValue(value);
        _myUIManager.SetSliderActive(active);
    }

    // Actualizar el valor del powerup mientras este está activo
    public void WhilePowerUpActive(float value)
    {
        _myUIManager.UpdatePowerUpSlider(value);
    }
    #endregion

    // Initializes GameManager instance.
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = _levelDuration;
        _actualLevel = 0;
        _myUIManager = _myUIObject.GetComponent<UIManager>();
        _myLife = _player.GetComponent<PlayerLifeComponent>();
        _myPauseObject.SetActive(false);
        _gameOverScreen = _myGameOver.GetComponent<GameOver>();
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
    }
}
