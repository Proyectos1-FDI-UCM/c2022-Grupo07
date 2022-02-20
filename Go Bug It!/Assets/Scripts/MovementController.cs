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
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxspeed; //velocidad máxima alcanzable
    [SerializeField]
    private float _airSpeed;
    [SerializeField]
    float _dashforce;
    #endregion

    #region properties
    private float _elapsedtime;
    private Vector3 _movementDirection;
    private bool _dash;
    private float _elapsedash;
    #endregion

    #region methods
    // Asigna la dirección del movimiento seleccionada a través del input del jugador
    public void SetMovementDirection(float horizontal)
    {
        _movementDirection = horizontal * _myTransform.right;
    }
    public float Speed(float _elapsedtime, float _acceleration)
    {
        float _speed = 0;
        // Cálculos del movimiento
        _speed += _acceleration * _elapsedtime; //Fórmula del movimiento uniformemente acelerado
        _speed = Mathf.Clamp(_speed, 0f, _maxspeed); // Limita la velocidad a la velocidad máxima alcanzable
        if (!_myInput._isGrounded)
        {
            _speed *= _airSpeed;
        }
        if (_movementDirection.magnitude == 0) _speed = 0;
        return _speed;
    }
    /*public void Dash() //Teleport dash, por si acaso
    {
        float _previusGravity=_rigidbody2D.gravityScale;
        float _elapseduration = 0;
        // float _elapsedcooldown = 0;
        // if(_elapsedcooldown<=_dashcooldown)
        _dash = true;
        
           while(_elapseduration<=_dashduration)
           {
            _rigidbody2D.gravityScale = 0;
            _myTransform.Translate(_movementDirection * 1 * Time.deltaTime);
            _elapseduration += Time.deltaTime;
            //_elapsedcooldown += Time.deltaTime;

           }
        _dash = false;
        
        //_elapsedcooldown = 0;
        _elapseduration = 0;
        _rigidbody2D.gravityScale = _previusGravity;
    }*/
    public void Dash()
    {
        float _previusGravity = _rigidbody2D.gravityScale; //guarda la gravedad anterior
        _rigidbody2D.AddForce(_movementDirection * _dashforce);//aplica una fuerza en la direccion del movimiento
    }
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myInput = GetComponent<InputController>();
        _myTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Guardar tiempo transcurrido para los cálculos
        if (_movementDirection.magnitude != 0) _elapsedtime += Time.deltaTime;
        else  _elapsedtime = 0;

        // Aplicar movimiento
       
          _myTransform.Translate(_movementDirection * Speed(_elapsedtime,_acceleration)* Time.deltaTime);
        
       
    }
}
