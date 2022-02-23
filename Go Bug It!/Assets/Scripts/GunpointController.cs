using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpointController : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    [SerializeField] private GameObject _myShot; // Prefab bala
    [SerializeField] private Transform Gun; // Posición del label Gun
    #endregion

    #region methods
    // Instanciación de la bala
    public void Shoot()
    {
        GameObject.Instantiate(_myShot, _myTransform.position, _myTransform.rotation);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }
}
