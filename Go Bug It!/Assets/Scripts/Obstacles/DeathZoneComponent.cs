using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider2D;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
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

    // Start is called before the first frame update
    void Start()
    {
        _myCollider2D = GetComponent<Collider2D>();
    }
}
