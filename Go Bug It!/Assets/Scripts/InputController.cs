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
    #endregion

    #region parameters
    // Gravedad
    private bool _changeGravity = false;
    [HideInInspector] public bool _isGrounded = false;
    #endregion

    #region properties
    private float direction;
    // Dash
    private float _elapseDash;
    [SerializeField] private float _dashCooldown;
    // Disparo
    private float _elapsedShoot;
    [SerializeField] private float _shootCooldown;
    private float _elapsedSelect;
    [SerializeField] private float _shotSelectCooldown;
    // Axis
    private float _horizontal;
    private float _jump;
    private float _dash;
    private float _selectShot;
    private float _shoot;
    #endregion

    #region methods
    // Saber si el jugador esta tocando una superficie o no
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null) _isGrounded = true;
    }

    // Marcar que el jugador no está tocando el suelo
    public void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    // Asignar la orientación de la bala según la del jugador
    public void Switch()
    {
        if (_horizontal > 0) direction = 1;
        else if (_horizontal < 0) direction = -1;
    }

    // Devuelve la dirección
    public float GetDirection()
    {
        return direction;
    }

    // Devuelve la variable booleana que determina si está o no tocando el suelo
    public bool GetGrounded()
    {
        return _isGrounded;
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

        //Movimiento del personaje
        _movController.SetMovementDirection(_horizontal);

        // Cambio de gravedad
        if (_isGrounded && _jump > 0)                               //Si presiono espacio y estoy tocando una superficie...
        {
            _changeGravity = !_changeGravity;                       //Negamos el booleano gravedad para q ahora sea lo contrario
            _myGravityComponent.ChangeGravity(_changeGravity);      //Llamamos al metodo ChangeGravity del script de gravedad
        }

        // Dash
        if (_elapseDash > _dashCooldown && _isGrounded && _dash > 0)
        {
            _movController.SetDash();
            _elapseDash = 0;
        }
        else _elapseDash += Time.deltaTime;

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
            Switch();
            _myGunpoint.Shoot();
            _elapsedShoot = 0;
        }
        else _elapsedShoot += Time.deltaTime;
    }
}
