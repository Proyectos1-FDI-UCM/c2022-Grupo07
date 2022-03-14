using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpointController : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    [SerializeField] private GameObject _gravShot; // Bala gravitatoria
    [SerializeField] private GameObject _neuShot; // Bala neutralizadora
    [SerializeField] private Transform Gun; // Posici�n del label Gun
    [SerializeField] private Transform _playerTransform; // Posici�n del label Gun
    private InputController _myinput;
    private Animator _playerAnimator;
    [SerializeField]private LineRenderer _myRay;
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
    // Instanciaci�n de la bala
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

    }

    IEnumerator LineDrawer()
    {
        _myRay.enabled = true;

        //Esperar un frame
        yield return new WaitForSeconds(0.02f);

        _myRay.enabled = false;
    }
    public void RaycastShoot()
    {
        int layermask = 1 << 3;
        layermask = ~layermask;
        int sign = BulletOrientation();

        RaycastHit2D _objectHit =  Physics2D.Raycast(Gun.position, Gun.right*sign, 1000, layermask);

        if (_objectHit)
        {
            Debug.Log(_objectHit.transform.name);
            EnemyLifeComponent _enemy = _objectHit.transform.GetComponent<EnemyLifeComponent>();
            if (_enemy != null)
            {
                _enemy.Dies();
            }
            
            _myRay.SetPosition(0, Gun.position);
            _myRay.SetPosition(1, _objectHit.point);
        }

        else
        {
            _myRay.SetPosition(0, Gun.position);
            _myRay.SetPosition(1, Gun.position + Gun.right  *sign * 100);
        }

        StartCoroutine(LineDrawer());
    }

    // Asignar la orientaci�n de la bala seg�n la del jugador
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
