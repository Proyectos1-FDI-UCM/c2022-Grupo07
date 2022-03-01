using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _shotSpeed = 1.0f;
    #endregion
    #region properties
    private Transform _myTransform;
    #endregion
    #region references

    #endregion
    #region methods
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = new RaycastHit2D();

        if (Physics2D.Raycast(_myTransform.position, ) ray )   
    }
}
