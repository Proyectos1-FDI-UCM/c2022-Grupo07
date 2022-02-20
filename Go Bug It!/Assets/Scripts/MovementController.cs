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
    private float _maxspeed; //velocidad m�xima alcanzable
    [SerializeField]
    private float _airSpeed;
    private float _speed;
    [SerializeField]
    private float _dashduration;
    [SerializeField]
    private float _dashcooldown;
    float direction;
    #endregion

    #region properties
    private float _elapsedtime;
    private Vector3 _movementDirection;
    private bool _dash;
    #endregion

    #region methods
    // Asigna la direcci�n del movimiento seleccionada a trav�s del input del jugador
    public void SetMovementDirection(float horizontal)
    {
        _movementDirection = horizontal * _myTransform.right;
    }
    public void Dash() //Probar con addforce en vez translate
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
    }
    public void Switch()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("hola");
            direction = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("hola");
            direction = -1;

        }
    }
   
    public float GetDirection()
    {
        return direction;
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
        // Guardar tiempo transcurrido para los c�lculos
        if (_movementDirection.magnitude != 0) _elapsedtime += Time.deltaTime;
        else  _elapsedtime = 0;

        // Cancelar velocidad si no se detecta movimiento
        

        // C�lculos del movimiento
        _speed += _acceleration * _elapsedtime; //F�rmula del movimiento uniformemente acelerado
        _speed = Mathf.Clamp(_speed, 0f, _maxspeed); // Limita la velocidad a la velocidad m�xima alcanzable
        if (!_myInput._isGrounded)
        {
            _speed *= _airSpeed;
        }
        if (_movementDirection.magnitude==0) _speed = 0;
      
       

        // Aplicar movimiento
       if(!_dash)
        {
          _myTransform.Translate(_movementDirection * _speed * Time.deltaTime);
        }


        Switch();
        Debug.Log(_rigidbody2D.velocity);
    }
   
}
