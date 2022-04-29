using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    int _points = 0;
    // Temporizador del nivel
    private float _timeLeft;
    public float _slowtimeFactor;
    #endregion

    #region references
    // Patrón singleton
    static private GameManager _instance;
    static public GameManager Instance { get { return _instance; } }
    // UI
    private GameObject _myUIObject;
    private UIManager _myUIManager;
    // Pausa
    private GameObject _myPauseObject;
    private PauseMenu _myPause;
    // Jugador
    private GameObject _player;
    private PlayerLifeComponent _myPlayerLife;
    // Boss
    private BossLifeController _boss;
    // GameOver
    // [SerializeField] private GameObject ;
    // private GameOver _gameOverScreen;
    #endregion

    #region properties
    private int[] _collectibles = new int[4]; // 0 = no obtenido, 1 = obtenido
    [HideInInspector] public bool _spam;
    [HideInInspector] public float _speedmod;
    private string _scene = "";
    #endregion

    #region methods
    // Devuelve los puntos
    public int GetPoints()
    {
        return _points;
    }

    // Devuelve la última escena registrada
    public string GetScene()
    {
        return _scene;
    }

    public void Spammed()
    {
        if (_spam) _speedmod = _slowtimeFactor;//_speedmod es una variable que cogen todos los objetos afectados, si esta spameado, coge el valor realentizado
        else _speedmod = 1;// Cuando no está spameado, vale 1
    }
    IEnumerator EndGameCo(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("VictoryScene");
    }
    public void EndGame(float seconds)
    {
        StartCoroutine(EndGameCo(seconds));
    }

    // Espera hasta que termine la animación de muerte
    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(1.1f);
        _myUIObject.SetActive(false);
        _myPauseObject.SetActive(false);
        SceneManager.LoadScene("GameOver");
    }

    // Espera hasta que termine la animación de fin de nivel
    IEnumerator LevelTransition(string scene)
    {
        // Animación del jugador de fin del nivel
        _player.GetComponent<Rigidbody2D>().simulated = false;
        _player.GetComponent<Animator>().SetBool("End", true);
        yield return new WaitForSeconds(0.5f);

        // Transición
        _myUIManager.SetTransition(true);
        yield return new WaitForSeconds(1.5f);

        // Cargar siguiente escena
        _scene = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(scene);
        _myUIManager.SetTransition(false);
    }

    // Lo llama el Game Over
    IEnumerator RetryTransition(string scene)
    {
        // Activar UI y transición
        _myUIManager.SetTransition(true);
        _myUIObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        // if (scene != "Tutorial") DestroyOnLoad(); // Si es el tutorial, destruir DontDestroyOnLoad
        // else _myUIManager.SetTransition(false); // Si no, transición normal

        // Transición y cargar escena
        _myUIManager.SetTransition(false);
        SceneManager.LoadScene(scene);
    }

    // Avance de nivel
    public void OnGoalAdvance(string scene, float newTime)
    {
        _myUIManager.UpdatePoints(Mathf.RoundToInt(_timeLeft) * 2);
        _timeLeft = newTime;
        _myPlayerLife.FullyHealing();
        StartCoroutine(LevelTransition(scene));
    }

    // Reintentar el nivel
    public void OnRetry(string scene, float newTime)
    {
        _myUIManager.SetTransition(false);
        _myUIObject.SetActive(true);
        _timeLeft = newTime;
        StartCoroutine(RetryTransition(scene));
    }

    /* Registro del jugador en el GameManager
    public void PlayerRegistration(PlayerLifeComponent player)
    {
        _myPlayerLife = player;
    } */

    // Registrar a la instancia del nivel del player
    public void PLayerRegistrationTrue(GameObject player)
    {
        _player = player;
        _myPlayerLife = _player.GetComponent<PlayerLifeComponent>();
    }

    // Registrar a la instancia del nivel de la UI
    public void UIRegistration(GameObject UI)
    {
        _myUIObject = UI;
        _myUIManager = _myUIObject.GetComponent<UIManager>();
    }

    // Registrar a la instancia del nivel de la UI
    public void PauseRegistration(GameObject pause)
    {
        _myPauseObject = pause;
        _myPause = _myPauseObject.GetComponent<PauseMenu>();
        _myPauseObject.SetActive(false);
    }

    // Actualiza la puntuación y llama la UI
    public void UpdatePoints(int newPoints)
    {
        _points += newPoints;               // Sumar puntuación
        if (_points > 9999) _points = 9999; // Limitar puntuación
        _myUIManager.UpdatePoints(_points);
    }

    // Muerte de un enemigo
    public void OnEnemyDies (int puntuation)
    {
        UpdatePoints(puntuation);
    }

    // Muerte del jefe
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

    // Activar disparo de daño
    public void OnDmgShootActivate()
    {
        _myUIManager.DmgShootActivate();
    }

    // Pausa el juego y abre el menu de pausa
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
        _scene = SceneManager.GetActiveScene().name;
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

    // Actualiza el array de coleccionables y lo aplica en el menú de pausa
    public void OnCollectiblePicked(int posicion)
    {
        Debug.Log("Picked");
        _collectibles[posicion] = 1;
        _myPause.ActivateCollectibles(_collectibles);
    }

    // Devuelve el array de coleccionables
    public int[] GetCollectibles()
    {
        return _collectibles;
    }

    // Destruye la UI, el menú de pausa y a sí mismo para volver al menú principal
    public void MainMenu()
    {
        Destroy(gameObject);
    }
    #endregion

    // Initializes GameManager instance
    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < _collectibles.Length; i++) _collectibles[i] = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _spam = false;
        _timeLeft = 300.0f;
        _boss = gameObject.GetComponent<BossLifeController>();
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_timeLeft <= 0) OnPlayerDies();
        else
        {
            _timeLeft -= Time.deltaTime;
            _myUIManager.UpdateTime((int)_timeLeft);
        }
        */
        Spammed();//Comprueba constantemente si el spam está activado
    }
}
