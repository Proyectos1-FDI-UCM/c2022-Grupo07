using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileTrueController : MonoBehaviour
{
    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_enemy != null)
        {
            int puntuacion = _enemy.GetPoints();
            _enemy.Dies();
            GameManager.Instance.OnEnemyDies(puntuacion);

            _enemy.enabled = false;
        }
    }
    #endregion
}
