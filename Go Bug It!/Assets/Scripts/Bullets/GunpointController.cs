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
    InputController _myinput;
    private Transform _playerTransform;
    #endregion

    #region properties
    public enum ShootType {Gravity, Neutralize}
    [SerializeField, Range(0, 1)] ShootType _shot = 0;
    Vector2 _direction;
    #endregion
    #region parameters
    [SerializeField]
    private float _offset;
    #endregion

    #region methods
    // Instanciación de la bala
    public void Shoot()
    {
        bool _prevdirection=true;
         if (_myinput.GetDirection() > 0) _prevdirection=true;// decide cual es la dirección de disparo según la dirección anterior.
        else if (_myinput.GetDirection() < 0)_prevdirection= false;
        if (_prevdirection == false)
        {
            _direction = _myTransform.position;
            _direction.x -= (2 * _offset); 
        }
        else if (_prevdirection == true) _direction = _myTransform.position;// si es positivo, se dispara desde la posición del gunpoint (a la dcha.)
        if (_shot == ShootType.Gravity) GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
        else GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
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
        _playerTransform = GetComponentInParent<Transform>();
        _myinput = GetComponentInParent<InputController>();
    }
}
