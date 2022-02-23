using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpointController : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    [SerializeField] private GameObject _myShot;
    [SerializeField] private Transform Gun;
    #endregion

    #region methods
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

    // Update is called once per frame
    void Update()
    {

    }
}
