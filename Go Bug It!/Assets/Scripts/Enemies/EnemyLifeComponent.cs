using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _puntuation;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==7)// Si el enemigo toca una deathzone, muere, se destruye.
        {
            GameManager.Instance.OnEnemyDies(_puntuation);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
