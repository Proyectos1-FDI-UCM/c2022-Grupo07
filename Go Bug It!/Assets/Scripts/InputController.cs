using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    #region references
    private MovementController _movController;
    private GravityComponent _myGravityComponent;
    private Collider2D _myCollider;
    #endregion

    #region properties
    private float _horizontal;
    private bool _changeGravity;
    [HideInInspector] public bool _isGrounded;
    #endregion

    #region parameters
    private float direction;
    #endregion

    #region methods
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) _isGrounded = true;
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _movController = GetComponent<MovementController>();
        _myGravityComponent = GetComponent<GravityComponent>();
        _myCollider = GetComponent<Collider2D>();
        _changeGravity = false;
        _isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");  

        // Movimiento del personaje
        _movController.SetMovementDirection(_horizontal);

        //Cambio de gravedad
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _changeGravity = !_changeGravity;
            _myGravityComponent.ChangeGravity(_changeGravity);
            _isGrounded = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _movController.Dash();
        }

        // Asignar orientación bala
        Switch();
    }
}
