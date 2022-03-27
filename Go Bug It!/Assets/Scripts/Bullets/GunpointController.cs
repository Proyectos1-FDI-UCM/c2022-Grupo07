using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpointController : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    [SerializeField] private GameObject _gravShot; // Bala gravitatoria
    [SerializeField] private GameObject _neuShot; // Bala neutralizadora
    [SerializeField] private GameObject _dmgShot; // Bala neutralizadora
    [SerializeField] private Transform Gun; // Posición del label Gun
    [SerializeField] private Transform _playerTransform; // Posición del label Gun
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
    [SerializeField] private float _tripleShotAngle = 45;
    private bool _dmgShoot = false;
    #endregion

    #region methods

    // Asigna el valor true al _dmgShoot para poder hacer ese disparo
    public void SetDmgShoot()
    {
        _dmgShoot = true;
    }

    // Disparo normal. Instanciación de la bala según su tipo y si es o no la tercera bala
    public void RegularShoot()
    {
        // Dirección de la bala
        int _sign = BulletOrientation();

        // Si no es el tercer disparo
        if (_dmgShoot == false)
        {
            // Instancia de la bala
            if (_shot == ShootType.Gravity)                                 // Gravedad
            {
                GameObject _grav = GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
                _grav.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _grav.GetComponent<BulletMovementController>().BulletRotation(_sign);
            }
            else if (_shot == ShootType.Neutralize)                         // Neutralizador
            {
                GameObject _neu = GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
                _neu.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _neu.GetComponent<BulletMovementController>().BulletRotation(_sign);
            }
        }
        else                                                                // Daño
        {
            GameObject _dmg = GameObject.Instantiate(_dmgShot, _direction, _myTransform.rotation);
            _dmg.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            _dmg.GetComponent<BulletMovementController>().BulletRotation(_sign);
        }
    }

    // Disparo triple (*3). Instanciación de las bala según su tipo y si es o no la tercera bala
    public void TripleShoot()
    {
        // Dirección de la bala
        int _sign = BulletOrientation();

        // Instanciar tres balas (1: 0º, 2: 45º, 3: -45º)
        if (_dmgShoot == false)
        {
            if (_shot == ShootType.Gravity)                                 // Gravedad
            {
                //Bala 1
                GameObject _grav = GameObject.Instantiate(_gravShot, _direction, _myTransform.rotation);
                _grav.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _grav.GetComponent<BulletMovementController>().BulletRotation(_sign);
                //Bala 2
                GameObject _grav1 = GameObject.Instantiate(_gravShot, _direction, Quaternion.AngleAxis(_tripleShotAngle, transform.forward));
                _grav1.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _grav1.GetComponent<BulletMovementController>().BulletRotation(_sign);
                //Bala 3
                GameObject _grav2 = GameObject.Instantiate(_gravShot, _direction, Quaternion.AngleAxis(-_tripleShotAngle, transform.forward));
                _grav2.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _grav2.GetComponent<BulletMovementController>().BulletRotation(_sign);
            }
            else                                                            // Neutralizardor
            {
                //Bala 1
                GameObject _neu = GameObject.Instantiate(_neuShot, _direction, _myTransform.rotation);
                _neu.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _neu.GetComponent<BulletMovementController>().BulletRotation(_sign);
                //Bala 2
                GameObject _neu1 = GameObject.Instantiate(_neuShot, _direction, Quaternion.AngleAxis(_tripleShotAngle, transform.forward));
                _neu1.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _neu1.GetComponent<BulletMovementController>().BulletRotation(_sign);
                //Bala 3
                GameObject _neu2 = GameObject.Instantiate(_neuShot, _direction, Quaternion.AngleAxis(-_tripleShotAngle, transform.forward));
                _neu2.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
                _neu2.GetComponent<BulletMovementController>().BulletRotation(_sign);
            }
        }
        else                                                                // Daño
        {
            //Bala 1
            GameObject _dmg = GameObject.Instantiate(_dmgShot, _direction, _myTransform.rotation);
            _dmg.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            _dmg.GetComponent<BulletMovementController>().BulletRotation(_sign);
            //Bala 2
            GameObject _dmg1 = GameObject.Instantiate(_dmgShot, _direction, Quaternion.AngleAxis(_tripleShotAngle, transform.forward));
            _dmg1.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            _dmg1.GetComponent<BulletMovementController>().BulletRotation(_sign);
            //Bala 3
            GameObject _dmg2 = GameObject.Instantiate(_dmgShot, _direction, Quaternion.AngleAxis(-_tripleShotAngle, transform.forward));
            _dmg2.GetComponent<BulletMovementController>().SetMovementDirection(_sign);
            _dmg2.GetComponent<BulletMovementController>().BulletRotation(_sign);
        }
    }

    IEnumerator LineDrawer()
    {
        _myRay.enabled = true;

        //Esperar un frame
        yield return new WaitForSeconds(0.02f);

        _myRay.enabled = false;
    }

    // Disparo laser (Stackpointer)
    public void RaycastShoot()
    {
        _myRay.gameObject.SetActive(true);
        int layermask = 3 << 1;
        layermask = ~layermask;
        Debug.Log("mascara : " + layermask);
        int sign = BulletOrientation();

        RaycastHit2D _objectHit =  Physics2D.Raycast(Gun.position + new Vector3(_offset,0,0) * sign, Gun.right*sign, 1000, layermask);

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
