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
    // UI
    [SerializeField] private GameObject _myUIObject;
    private UIManager _myUIManager;
    [SerializeField] private GameObject _player;
    private InputController _myinput;
    private PlayerLifeComponent _myLife;
    #endregion

    #region methods
    public void OnGoalAdvance()
    {
        _actualLevel++;
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(_myUIObject);
        SceneManager.LoadScene("Level 1");
    }
    public void OnEnemyDies (int _puntuation)
    {
        _myUIManager.UpdatePoints(_puntuation);
    }

    // Daño al jugador
    public void OnPlayerDamage(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints, false);
    }

    public void OnChangingShoot(int shot)
    {
        _myUIManager.UpdateShot(shot);
    }
    public void Pause(bool pause)//Pausa el juego y abre el menu de pausa
    {
        _myUIManager.Pause();
        if (pause == true)Time.timeScale = 0;      
        else Time.timeScale = 1;           
    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    public void OnPlayerHeals(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints-1, true);
    }

    // Initializes GameManager instance and list of enemies.
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
        _myinput = _player.GetComponent<InputController>();
        _myLife = _player.GetComponent<PlayerLifeComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeLeft <= 0) _myLife.Dies();
        else
        {
            _timeLeft -= Time.deltaTime;
            _myUIManager.UpdateTime((int)_timeLeft);
        }
    }
}
