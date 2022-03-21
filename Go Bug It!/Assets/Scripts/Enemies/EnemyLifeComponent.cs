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
    [SerializeField]
    private Animator _myAnimator;
    #endregion

    #region methods
    public int GetPoints()
    {
        return _puntuation;
    }

    public void Dies()
    {
        GameManager.Instance.OnEnemyDies(GetPoints());
        StartCoroutine( dyingAnimation());
    }

    IEnumerator dyingAnimation()
    {
        _myAnimator.SetBool("Death", true);
        yield return new WaitForSecondsRealtime(_animationDuration);
        Destroy(gameObject);
    }
    #endregion
}
