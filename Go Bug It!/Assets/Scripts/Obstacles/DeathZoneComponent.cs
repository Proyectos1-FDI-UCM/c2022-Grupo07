using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneComponent : MonoBehaviour
{

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_enemy != null)
        {
            int puntuacion = _enemy.GetPoints();
            Destroy(_enemy.gameObject);
            GameManager.Instance.OnEnemyDies(puntuacion);
        }
    }
    #endregion

}
