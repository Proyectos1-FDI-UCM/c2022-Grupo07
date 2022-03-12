using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _puntuation;
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
    #endregion
}
