using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _puntuation;
    [SerializeField] private float _animationDuration = 0.3f;
    #endregion

    #region references
    [SerializeField] private Animator _myAnimator;
    #endregion

    #region methods
    // Devuelve los puntos que da el enemigo
    public int GetPoints()
    {
        return _puntuation;
    }

    // Muerte del enemigo
    public void Dies()
    {
        GameManager.Instance.OnEnemyDies(GetPoints());
        StartCoroutine(DyingAnimation());
    }

    // Animaci�n de muerte y destrucci�n del enemigo
    IEnumerator DyingAnimation()
    {
        _myAnimator.SetBool("Death", true);
        yield return new WaitForSecondsRealtime(_animationDuration);
        Destroy(gameObject);
    }
    #endregion
}
