using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationAreaComponent : MonoBehaviour
{
    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si es el jugador
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (_myPlayer != null && !_myPlayer.gameObject.GetComponent<PowerUpController>().IsShieldActive()) _myPlayer.CallForDamage();

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
