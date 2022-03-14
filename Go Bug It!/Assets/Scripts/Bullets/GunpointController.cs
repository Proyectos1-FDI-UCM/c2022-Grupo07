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
    [SerializeField] private Transform _playerTransform; // Posición del label Gun
    private InputController _myinput;
    private Animator _playerAnimator;
    #endregion

    #region properties
    public enum ShootType {Gravity, Neutralize}
    [SerializeField, Range(0, 1)] private ShootType _shot = 0;
    private Vector3 _direction;
    #endregion

    #region parameters
    [SerializeField] private float _offset;
    [SerializeField] private float _tripleShotAngle = 45;
    #endregion

    #region methods
    // Instanciación de la bala
    public void RegularShoot()
    {
        int _sign = BulletOrientation();
        if (_shot == ShootType.Gravity)
        {
            GameObject _grav = GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
            _grav.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
        }
        else
        {
            GameObject _neu = GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
            _neu.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
        }
    }

    public void TripleShoot()
    {
        int _sign = BulletOrientation();
        if (_shot == ShootType.Gravity)
        {
            //Bala 1
            GameObject _grav = GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
            _grav.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            //Bala 2
            GameObject _grav1 = GameObject.Instantiate(_gravShot, _direction, Quaternion.AngleAxis(_tripleShotAngle, transform.forward));
            _grav1.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            //Bala 3
            GameObject _grav2 = GameObject.Instantiate(_gravShot, _direction, Quaternion.AngleAxis(-_tripleShotAngle, transform.forward));
            _grav2.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
        }
        else
        {
            //Bala 1
            GameObject _neu = GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
            _neu.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            //Bala 2
            GameObject _neu1 = GameObject.Instantiate(_neuShot, _direction, Quaternion.AngleAxis(_tripleShotAngle, transform.forward));
            _neu1.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            //Bala 3
            GameObject _neu2 = GameObject.Instantiate(_neuShot, _direction, Quaternion.AngleAxis(-_tripleShotAngle, transform.forward));
            _neu2.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
        }
    }

    // Asignar la orientación de la bala según la del jugador
    public int  BulletOrientation()
    {
        int sign = 0;

        if (_playerTransform.localScale.x > 0)
        {
            _direction = new Vector3(_myTransform.position.x + _offset, _myTransform.position.y, 0.0f);
            sign = 1;
        }
        else
        {
            _direction = new Vector3(_myTransform.position.x - _offset, _myTransform.position.y, 0.0f);
            sign = -1;
        }

        return sign;
    }

    // Cambio de disparo
    public void ChangeShoot()
    {
        if (_shot == ShootType.Gravity)
        {
            _shot = ShootType.Neutralize;
            GameManager.Instance.OnChangingShoot(1);
            _playerAnimator.SetBool("NeuShot", true);
            _playerAnimator.SetBool("GravShot", false);
        }
        else
        {
            _shot = ShootType.Gravity;
            GameManager.Instance.OnChangingShoot(0);
            _playerAnimator.SetBool("NeuShot", false);
            _playerAnimator.SetBool("GravShot", true);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myinput = GetComponentInParent<InputController>();
        _playerAnimator = GetComponentInParent<Animator>();
        _playerAnimator.SetBool("GravShot", true);
    }
}
