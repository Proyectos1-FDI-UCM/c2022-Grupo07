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
    [SerializeField] private float _dashForce;
    #endregion

    #region properties
    private float _elapsedtime;
    private float _movementDirection;
    private Vector3 _dashDirection;
    #endregion

    #region methods
    // Asigna la dirección del movimiento seleccionada a través del input del jugador
    public void SetDashDirection(float horizontal)
    {
        _dashDirection = horizontal * _myTransform.right;//Da un vector en la dirección de movimiento para aplicar la fuerza.
    }
    public void SetMovementDirection(float horizontal)
    {
        _movementDirection = horizontal;
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
        _rigidbody2D.AddForce(_dashDirection*_dashForce);
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
        // Aplicar movimiento
        _myTransform.Translate(_dashDirection * Speed(_elapsedtime,_acceleration)* Time.fixedDeltaTime);
        
        /* _rigidbody2D.velocity = new Vector2(_movementDirection*Speed(_elapsedtime,_acceleration), _rigidbody2D.velocity.y);*/
    }
   
}
