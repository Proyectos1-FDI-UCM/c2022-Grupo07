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
    #endregion

    #region methods
    // Instanciación de la bala
    public void RegularShoot()
    {
        if (_shot == ShootType.Gravity)
        {
            GameObject _grav = GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
            _grav.GetComponent<BulletMovementController>().SetMovementDirection(BulletOrientation());
        }
        else
        {
            GameObject _neu = GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
            _neu.GetComponent<BulletMovementController>().SetMovementDirection(BulletOrientation());
        }
    }

    public void TripleShoot()
    {

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
