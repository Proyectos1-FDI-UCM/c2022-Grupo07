using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileTrueController : MonoBehaviour
{

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con un enemigo
        EnemyLifeComponent _enemyWD = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (_enemyWD != null)
        {
            Debug.Log("Matar enemigo WD");
            _enemyWD.Dies();
            _enemyWD.enabled = false;
        }

        EnemyLifeComponent _enemyNM = collision.gameObject.GetComponentInChildren<EnemyLifeComponent>();
        if (_enemyNM != null)
        {
            Debug.Log("Matar enemigo N-M");
            _enemyNM.Dies();
            _enemyNM.enabled = false;
        }

        // Colisión con el jugador
        PlayerLifeComponent _player = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_player != null) _player.CallForDamage();
    }
    #endregion

}
