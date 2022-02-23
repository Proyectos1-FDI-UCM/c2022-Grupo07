using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region parameters
    // Temporizador del nivel
    [SerializeField] private int _levelDuration;
    private float _timeLeft;
    #endregion

    #region references
    // Patrón singleton
    static private GameManager _instance;
    static public GameManager Instance { get { return _instance; } }
    // UI
    [SerializeField] private GameObject _myUIObject;
    private UIManager _myUIManager;
    #endregion

    #region methods
    // Daño al jugador
    public void OnPlayerDamage(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints, false);
    }
    #endregion

    // Initializes GameManager instance and list of enemies.
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = _levelDuration;
        _myUIManager = _myUIObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeLeft <= 0) Debug.Log("Perdiste");
        else
        {
            _timeLeft -= Time.deltaTime;
            _myUIManager.UpdateTime((int)_timeLeft);
        }

        _myUIManager.UpdatePoints(1);
    }
}
