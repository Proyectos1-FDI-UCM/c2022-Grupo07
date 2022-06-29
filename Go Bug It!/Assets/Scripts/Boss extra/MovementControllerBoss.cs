using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerBoss : MonoBehaviour
{

    #region references
    private BossInputController _myInput;
    private Rigidbody2D _rigidbody2D;
    private Animator _myAnimator;
    #endregion

    #region parameters
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxspeed; //velocidad m�xima alcanzable
    [SerializeField, Range(0.1f, 1.0f)] private float _airSpeed;
    [SerializeField] private float _dashDuration;//Duraci�n del dash.
    [SerializeField] private float _dashSpeed;// velocidad del dash.
    #endregion

    #region properties
    private float _elapsedtime;
    private float _movementDirection;
    private float _currentSpeed;
    private float _prevgrav; //Guarda la gravedad previa
    public bool _dash = false;
    private float _elapsedDash;
    private bool m_FacingRight = false;  // For determining which way the player is currently facing.
    #endregion

    #region methods
    // Asigna la direcci�n del movimiento seleccionada a trav�s del input del jugador
    public void SetMovementDirection(float horizontal)
    {
        if (!_dash) _movementDirection = horizontal; // Da un vector en la direcci�n de movimiento para aplicar la fuerza.
    }

    // C�lculo de la velocidad del jugador
    public float Speed(float _elapsedtime, float _acceleration)
    {
        float _speed = 0;

        // C�lculos del movimiento
        _speed += _acceleration * _elapsedtime; //F�rmula del movimiento uniformemente acelerado
        _speed = Mathf.Clamp(_speed, 0f, _maxspeed); // Limita la velocidad a la velocidad m�xima alcanzable

        // Comprobar si est� en el aire
        if (!_myInput._isGrounded) _speed *= _airSpeed;

        // Comprobar si no se mueve
        if (_movementDirection == 0) _speed = 0;

        return _speed;
    }

    // Activar el dash
    public void SetDash()
    {
        _dash = true;
    }

    // Impulso
    public void Dash()
    {

        // Si se est� haciendo el dash
        if (_elapsedDash <= _dashDuration)
        {
            if (_movementDirection > 0) _movementDirection = 1;
            else if (_movementDirection < 0) _movementDirection = -1;
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = new Vector2(_movementDirection * _currentSpeed, 0);
        }
        else // Si se termina de hacer
        {
            _rigidbody2D.gravityScale = _prevgrav;
            _dash = false;
            _elapsedDash = 0;
        }
    }

    // Giro hacia la direcci�n contraria
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void SetNewValues(float _multiplier)
    {
        _acceleration *= _multiplier;
        _airSpeed *= _multiplier;
        //_maxspeed *= _multiplier;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myInput = GetComponent<BossInputController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_dash) _elapsedDash += Time.deltaTime;

        // Guardar tiempo transcurrido para los c�lculos
        if (_movementDirection != 0) _elapsedtime += Time.deltaTime;
        else _elapsedtime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calcular velocidad
        if (!_dash)
        {
            _prevgrav = _rigidbody2D.gravityScale;
            _currentSpeed = Speed(_elapsedtime, _acceleration);
        }
        else if (_dash) _currentSpeed = _dashSpeed;

        // Aplicar giro
        if (_movementDirection > 0 && !m_FacingRight) Flip(); // Si no mira hacia la derecha
        else if (_movementDirection < 0 && m_FacingRight) Flip(); // Si mira hacia la derecha

        if (_dash) Dash();

        // Aplicar movimiento
        _rigidbody2D.velocity = new Vector2(-_movementDirection * _currentSpeed, _rigidbody2D.velocity.y);
    }

    private void LateUpdate()
    {
        // Animaci�n
        if (_myInput.GetGrounded()) _myAnimator.SetFloat("Speed", _currentSpeed);
        else _myAnimator.SetFloat("Speed", 0);
    }

}
