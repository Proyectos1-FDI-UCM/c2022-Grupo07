using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shieldDuration = 1.0f;
    [SerializeField]
    private float _structDuration = 1.0f;
    [SerializeField]
    private float _spDuration = 1.0f;
    [SerializeField]
    private float _spamDuration = 1.0f;
    [SerializeField] private float _slowValue = 0.5f;
    #endregion

    #region references
    [SerializeField]
    private GameObject _myShield;
    [SerializeField]
    private BulletMovementController _myBulletMovController;
    private MovementController _myMovementController;
    private GravityComponent _myGravityComponent;
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
    private void Awake()
    {
        _myShield.SetActive(false);
    }

    public void ShieldControl(bool state)
    {
        _shieldPowerUp = state;
        _isPoweredUp = state;
        _myShield.SetActive(state);
        if (state) _durationTime = _shieldDuration;
        else _durationTime = 0;
    }

    public void StructControl(bool state)
    {
        _structPowerUp = state;
        _isPoweredUp = state;
        if (state) _durationTime = _structDuration;
        else _durationTime = 0;
    }

    public void StackPointerControl(bool state)
    {
        _spPowerUp = state;
        _isPoweredUp = state;
        if (state) _durationTime = _spDuration;
        else _durationTime = 0;
    }

    public void ShootSP()
    {

    }

    public void SpamControl(bool state)
    {
        _spamPowerUp = state;
        _isPoweredUp = state;
        if (state)
        {
            Time.timeScale = 1 * _slowValue;
            _myMovementController.SetNewValues(1 / _slowValue);
            _myBulletMovController.SetNewSpeed(1 / _slowValue);
            _myGravityComponent.SetNewGravValue(_slowValue);
            _durationTime = _spamDuration;
        }
        else
        {
            Time.timeScale = 1;
            _myMovementController.SetNewValues(_slowValue);
            _myBulletMovController.SetNewSpeed(_slowValue);
            _myGravityComponent.SetNewGravValue(1 / _slowValue);
            _durationTime = 0;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<MovementController>();
        _myGravityComponent = GetComponent<GravityComponent>();
        _isPoweredUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPoweredUp)
        {
            _durationTime -= Time.deltaTime;
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
