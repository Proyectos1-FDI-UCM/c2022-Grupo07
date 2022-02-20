using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    float _dashcooldown;
    #endregion
    #region references
    private MovementController _movController;
    private GravityComponent _myGravityComponent;
    private Collider2D _myCollider;
    #endregion

    #region properties
    private float _horizontal;
    private bool _changeGravity;
    [HideInInspector] public bool _isGrounded;
    private float _elapsedash;
    private bool _dashcooldown_ok;
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
        _elapsedash = 0;
        _dashcooldown_ok = false;
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
        if(Input.GetKeyDown(KeyCode.LeftShift)&&_dashcooldown_ok)
        {
            _movController.Dash();
            _elapsedash = 0;
           
        }

       
        if(_elapsedash>=_dashcooldown)//calcula el cooldown de los dashes (Nota Rafa Malo: Se podria hacer un scrpit que lleve los cooldowns en su update, ya que parece el unico sitio donde funciona)
        {
            _dashcooldown_ok = true;
        }
        else
        {
          _dashcooldown_ok = false; 
            _elapsedash += Time.deltaTime;
        }
            
      
    }
}
