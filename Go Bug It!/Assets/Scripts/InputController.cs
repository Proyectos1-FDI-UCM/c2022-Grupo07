using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    #region references
    private MovementController _movController;
    private GravityComponent _myGravityComponent;
    private GunpointController _myGunpoint;
    private Animator _myAnimator;
    [SerializeField] private Collider2D _myGroundDetector;
    #endregion

    #region parameters
    // Gravedad
    private bool _changeGravity = false;
    [HideInInspector] public bool _isGrounded = false;
    #endregion

    #region properties
    private float _localScale;
    private bool _isPaused;
    // Dash
    private float _elapseDash;
    [SerializeField] private float _dashCooldown;
    // Disparo
    private float _elapsedShoot;
    [SerializeField] private float _shootCooldown;
    private float _elapsedSelect;
    [SerializeField] private float _shotSelectCooldown;
    [SerializeField] private bool _thirdBullet = false;
    // Axis
    private float _horizontal;
    private float _jump;
    private float _dash;
    private float _selectShot;
    private float _shoot;
    private float _pause;
    private int _typeShoot = 0;
    #endregion

    #region methods
    // Saber si el jugador esta tocando una superficie o no
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger) _isGrounded = true;
    }

    // Marcar que el jugador no está tocando el suelo
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger) _isGrounded = false;
    }

    // Devuelve la dirección
    public bool GetGravity()
    {
        return _changeGravity;
    }

    public void SetGravity(bool grav)
    {
        _changeGravity = grav;
    }

    // Devuelve la variable booleana que determina si está o no tocando el suelo
    public bool GetGrounded()
    {
        return _isGrounded;
    }

    public void SetNewTypeShoot(int newType)
    {
        _typeShoot = newType;
    }

    // Actualiza el valor de si está o no disponible el tercer disparo
    public void SetThirdShoot(bool set)
    {
        _thirdBullet = set;
    }
    public void SetElapsedDash(float fullCoolDown)
    {
         _elapseDash=fullCoolDown;
    }
    // Actualiza el valor del cooldown al recoger un desencriptar.

    IEnumerator changeGrav()
    {
        yield return new WaitForSeconds(0.2f);

        if (_myAnimator.GetBool("OnGravityChange") == true)
        {
            _myAnimator.SetBool("OnGravityChange", false);
        }
    }

    IEnumerator changeDash()
    {
        yield return new WaitForSeconds(0.65f);

        if (_myAnimator.GetBool("Dash") == true)
        {
            _myAnimator.SetBool("Dash", false);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        _movController = GetComponent<MovementController>();            // Accedemos al script de movimiento del jugador
        _myGravityComponent = GetComponent<GravityComponent>();         // Accedemos al script de gravedad del jugador
        _myGunpoint = transform.GetChild(0).GetComponent<GunpointController>(); // Accedemos al script de la pistola
        _myAnimator = GetComponent<Animator>();
        _elapsedShoot = _shootCooldown;
        _elapsedSelect = _shotSelectCooldown;
        _isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Ejes de inputs
        _horizontal = Input.GetAxis("Horizontal");
        _jump = Input.GetAxis("Jump");
        _dash = Input.GetAxis("Dash");
        _selectShot = Input.GetAxis("SelectShot");
        _shoot = Input.GetAxis("Shoot");
        _pause = Input.GetAxis("Pause");

        // Movimiento del personaje
        _movController.SetMovementDirection(_horizontal);

        // Cambio de gravedad
        if (_isGrounded && _jump > 0 &&!_movController._dash) //Si presiono espacio, estoy tocando una superficie y no estoy en mitad de un dash...
        {
            _changeGravity = !_changeGravity;                       //Negamos el booleano gravedad para q ahora sea lo contrario
            _myGravityComponent.ChangeGravity(_changeGravity);      //Llamamos al metodo ChangeGravity del script de gravedad
            StartCoroutine(changeGrav());
        }

        // Dash
        if (_elapseDash > _dashCooldown && _dash > 0)
        {
            _myAnimator.SetBool("Dash", true);
            _movController.SetDash();
            _elapseDash = 0;
            _myAnimator.SetBool("Dash", true);
            StartCoroutine(changeDash());            
        }
        else if(_isGrounded) _elapseDash += Time.deltaTime;

        // Selección de disparo
        if (_elapsedSelect > _shotSelectCooldown && _selectShot > 0)
        {
             _myGunpoint.ChangeShoot();
            _elapsedSelect = 0;
        }
        else _elapsedSelect += Time.deltaTime;

        // Disparo y orientación de la bala
        if (_elapsedShoot > _shootCooldown && _isGrounded && _shoot > 0)
        {
            _localScale = gameObject.transform.localScale.x;

            switch (_typeShoot)
            {
                case 0 : _myGunpoint.RegularShoot(_thirdBullet); break;
                case 1 : _myGunpoint.TripleShoot(_thirdBullet); break;
                case 2: _myGunpoint.RaycastShoot(); break;
            }
            _elapsedShoot = 0;
        }
        else _elapsedShoot += Time.deltaTime;

        // Pausa (se detecta si ya estaba pausado o no)
        if ((Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape)) && _isPaused == false)
        {
            _isPaused = true;
            GameManager.Instance.Pause(_isPaused);
        }
        else if ((Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape)) && _isPaused == true)
        {
            _isPaused = false;
            GameManager.Instance.Pause(_isPaused);
        }
    }

}
