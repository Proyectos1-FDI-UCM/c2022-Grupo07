using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileTrueController : MonoBehaviour
{

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con un enemigo
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_enemy != null)
        {
            GameManager.Instance.OnEnemyDies(_enemy.GetPoints());
            _enemy.Dies();
            _enemy.enabled = false;
        }

        // Colisión con el jugador
        PlayerLifeComponent _player = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (_player != null) _player.CallForDamage();
    }
    #endregion

}
