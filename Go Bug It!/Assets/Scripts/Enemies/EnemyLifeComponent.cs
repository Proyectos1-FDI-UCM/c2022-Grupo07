using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _puntuation;
    [SerializeField] private float _animationDuration = 1;
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

    // Animación de muerte y destrucción del enemigo
    IEnumerator DyingAnimation()
    {
        _myAnimator.SetBool("Death",true);
        yield return new WaitForSecondsRealtime(_animationDuration);
        Destroy(gameObject);
    }
    #endregion

    public void Update()
    {
        _myAnimator.speed = 1 * GameManager.Instance._speedmod; //Adecua la velocidad de la animación al spam
    }
}
