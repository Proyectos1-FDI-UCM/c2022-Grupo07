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

    #region methods
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) _isGrounded = true;
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
       
    }
}
