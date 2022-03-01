using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpointController : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    [SerializeField] private GameObject _gravShot; // Bala gravitatoria
    [SerializeField] private GameObject _neuShot; // Bala neutralizadora
    [SerializeField] private Transform Gun; // Posición del label Gun
    #endregion

    #region properties
    public enum ShootType {Gravity, Neutralize}
    [SerializeField, Range(0, 1)] ShootType _shot = 0;
    #endregion

    #region methods
    // Instanciación de la bala
    public void Shoot()
    {
        if (_shot == ShootType.Gravity) GameObject.Instantiate(_gravShot, _myTransform.position, _myTransform.rotation);
        else GameObject.Instantiate(_neuShot, _myTransform.position, _myTransform.rotation);
    }

    // Cambio de disparo
    public void ChangeShoot()
    {
        if (_shot == ShootType.Gravity)
        {
            _shot = ShootType.Neutralize;
            GameManager.Instance.OnChangingShoot(1);
        }
        else
        {
            _shot = ShootType.Gravity;
            GameManager.Instance.OnChangingShoot(0);
        }
        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }
}
