using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationAreaComponent : MonoBehaviour
{
    #region properties
    private bool _playerDetected = false;
    #endregion
    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si es el jugador
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (!_playerDetected && _myPlayer != null && !_myPlayer.gameObject.GetComponent<PowerUpController>().IsShieldActive())
        {
            _myPlayer.CallForDamage();
            _playerDetected = true;
        }
        else if (!_playerDetected && _myPlayer != null && _myPlayer.gameObject.GetComponent<PowerUpController>().IsShieldActive())
        {
            _myPlayer.GetComponent<PowerUpController>().ShieldControl(false);
            _playerDetected = true;
        }

        // Si es un enemigo
        EnemyLifeComponent _myEnemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_myEnemy != null)
        {
            // Si es un Norton
            NortonComponent _otherNorton = collision.gameObject.GetComponent<NortonComponent>();

            if (_otherNorton != null) _otherNorton.Activated();
            else _myEnemy.Dies();
        }
    }
    #endregion
}
