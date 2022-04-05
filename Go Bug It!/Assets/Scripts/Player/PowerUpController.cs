using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _shieldDuration = 1.0f;
    [SerializeField] private float _structDuration = 1.0f;
    [SerializeField] private float _spDuration = 1.0f;
    [SerializeField] private float _spamDuration = 1.0f;
    public float _slowValue = 0.5f;
    #endregion

    #region references
    [SerializeField] private GameObject _myShield;
    [SerializeField] private BulletMovementController _myBulletMovController;
    private InputController _myInputController;
    //private PlayerLifeComponent _myPlayerLifeComponent;
    #endregion

    #region properties
    private bool _isPoweredUp;
    private bool _shieldPowerUp;
    private bool _spPowerUp;
    private bool _structPowerUp;
    private bool _spamPowerUp;
    private float _durationTime = 0.0f;
    #endregion

    #region methods
    //Devuelve si el jugador esta o no con un powerup
    public bool IsPoweredUp()
    {
        return _isPoweredUp;
    }
    // Devuelve si el escudo está activado o no
    public bool IsShieldActive()
    {
        return _shieldPowerUp;
    }

    // Gestión del escudo
    public void ShieldControl(bool state)
    {
        _shieldPowerUp = state;
        _isPoweredUp = state;
        _myShield.SetActive(state);
        //_myPlayerLifeComponent.enabled = !state;

        if (state)
        {
            _durationTime = _shieldDuration;
            GameManager.Instance.OnPowerUpActivate(_shieldDuration, true, 0);
        }
        else
        {
            _durationTime = 0;
            GameManager.Instance.OnPowerUpActivate(0.0f, false, 0);
        }
    }

    // Gestión del struct
    public void StructControl(bool state)
    {
        _structPowerUp = state;
        _isPoweredUp = state;

        if (state)
        {
            _myInputController.SetNewTypeShoot(1);
            _durationTime = _structDuration;
            GameManager.Instance.OnPowerUpActivate(_structDuration, true, 1);
        }
        else
        {
            _myInputController.SetNewTypeShoot(0);
            _durationTime = 0;
            GameManager.Instance.OnPowerUpActivate(0.0f, false, 1);
        }
    }

    // Gestión del stackpointer
    public void StackPointerControl(bool state)
    {
        _spPowerUp = state;
        _isPoweredUp = state;

        if (state)
        {
            _myInputController.SetNewTypeShoot(2);
            _durationTime = _spDuration;
            GameManager.Instance.OnPowerUpActivate(_spDuration, true, 2);
        }
        else
        {
            _myInputController.SetNewTypeShoot(0);
            _durationTime = 0;
            GameManager.Instance.OnPowerUpActivate(0.0f, false, 2);
        }
    }

    // Gestión del spam
    public void SpamControl(bool state)
    {
        _spamPowerUp = state;
        _isPoweredUp = state;

        if (state)
        {
            GameManager.Instance._spam = true;
            _durationTime = _spamDuration;
            GameManager.Instance.OnPowerUpActivate(_spamDuration, true, 3);
        }
        else
        {
            GameManager.Instance._spam = false;
            _durationTime = 0;
            GameManager.Instance.OnPowerUpActivate(0.0f, false, 3);
        }
    }
    #endregion

    private void Awake()
    {
        _myShield.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _myInputController = GetComponent<InputController>();
        //_myPlayerLifeComponent = GetComponent<PlayerLifeComponent>();
        _isPoweredUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance._slowtimeFactor = _slowValue;
        if (_isPoweredUp)
        {
            _durationTime -= Time.deltaTime;
            GameManager.Instance.WhilePowerUpActive(_durationTime);
            if (_durationTime <= 0)
            {
                if (_shieldPowerUp) ShieldControl(false);
                else if (_structPowerUp) StructControl(false);
                else if (_spPowerUp) StackPointerControl(false);
                else if (_spamPowerUp) SpamControl(false);
            }
        }
    }
}
