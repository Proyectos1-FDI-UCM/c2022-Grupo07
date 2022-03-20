using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileTrue_controller : MonoBehaviour
{
    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_enemy != null)
        {
            int puntuacion = _enemy.GetPoints();
            _enemy.Dies();
            _enemy.enabled = false;
            GameManager.Instance.OnEnemyDies(puntuacion);
        }
    }
    #endregion
}
