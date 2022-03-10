using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shieldDuration;
    [SerializeField]
    private float _structDuration;
    [SerializeField]
    private float _spDuration;
    #endregion

    #region references
    [SerializeField]
    private GameObject _myShield;
    #endregion

    #region properties
    private bool _isPoweredUp;
    private bool _shieldPowerUp;
    private bool _spPowerUp;
    private bool _structPowerUp;
    private float _durationTime = 0.0f;
    #endregion

    #region methods
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }
}
