using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    #region references
    private InputController _myInput;
    private Transform _myTransform;
    private Rigidbody2D _rigidbody2D;
    #endregion

    #region parameters
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxspeed; //velocidad máxima alcanzable
    [SerializeField, Range(0.1f, 1.0f)] private float _airSpeed;
    [SerializeField] private float _dashDuration;//Duración del dash.
    [SerializeField] private float _dashSpeed;// velocidad del dash.
    #endregion

    #region properties
    private float _elapsedtime;
    private float _movementDirection;
    private float _speed;
    private bool _dash;
    private float _elapsedDash;
    #endregion

    #region methods
    // Asigna la dirección del movimiento seleccionada a través del input del jugador
    public void SetMovementDirection(float horizontal)
    {
        if (!_dash)_movementDirection = horizontal;//Da un vector en la dirección de movimiento para aplicar la fuerza.
    }

    // Cálculo de la velocidad del jugador
    public float Speed(float _elapsedtime, float _acceleration)
    {
        float _speed = 0;

        // Cálculos del movimiento
        _speed += _acceleration * _elapsedtime; //Fórmula del movimiento uniformemente acelerado
        _speed = Mathf.Clamp(_speed, 0f, _maxspeed); // Limita la velocidad a la velocidad máxima alcanzable
        
        // Comprobar si está en el aire
        if (!_myInput._isGrounded) _speed *= _airSpeed;

        // Comprobar si no se mueve
        if (_movementDirection == 0) _speed = 0;

        return _speed;
    }

    // Impulso
    public void Dash()
    {
            _dash = true;
            _speed = _dashSpeed;
            _rigidbody2D.velocity = new Vector2(_movementDirection * _speed, _rigidbody2D.velocity.y);   
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        MovingPlatformController _plataform = collision.gameObject.GetComponent<MovingPlatformController>();
        if (_plataform!=null) _myTransform.parent = collision.gameObject.transform;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        MovingPlatformController _plataform = collision.gameObject.GetComponent<MovingPlatformController>();
        if (_plataform != null) _myTransform.parent = null;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myInput = GetComponent<InputController>();
        _myTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
         // Guardar tiempo transcurrido para los cálculos
         if (_movementDirection!= 0) _elapsedtime += Time.deltaTime;
         else  _elapsedtime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_dash) _speed = Speed(_elapsedtime, _acceleration);
        // Aplicar movimiento
        if(_myInput._doDash)
        {
            _elapsedDash += Time.fixedDeltaTime;
            if (_movementDirection > 0) _movementDirection = 1;
            else if (_movementDirection < 0) _movementDirection = -1;
            Dash();
            if(_elapsedDash>=_dashDuration)
            {
                _myInput._doDash = false;
                _dash = false;
                _elapsedDash = 0;
            }
        }
        
        if (!_dash)
        {
            _myInput._doDash = false;
            _rigidbody2D.velocity = new Vector2(_movementDirection * _speed, _rigidbody2D.velocity.y);
        }
         
        
    }
   
}
