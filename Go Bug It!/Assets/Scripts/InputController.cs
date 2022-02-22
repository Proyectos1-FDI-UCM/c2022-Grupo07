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

    #region parameters
    private float direction;
    #endregion

    #region methods
    //Metodo que nos informa sobre si el jugador esta tocando una superficie o no
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
        _movController = GetComponent<MovementController>();            //Accedemos al script de movimiento del jugador
        _myGravityComponent = GetComponent<GravityComponent>();         //Accedemos al script de gravedad del jugador
        _myCollider = GetComponent<Collider2D>();                       //Accedemos al collider de nuestro jugador
        _changeGravity = false;                                         //Inicializamos el booleano de la gravedad a negativo para que la gravedad sea normal
        _isGrounded = false;                                            //Inicializamos el booleano de tocar una superficie a false
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)             //Si presiono espacio y estoy tocando una superficie...
        {
            _changeGravity = !_changeGravity;                           //Negamos el booleano gravedad para q ahora sea lo contrario
            _myGravityComponent.ChangeGravity(_changeGravity);          //Llamamos al metodo ChangeGravity del script de gravedad
            _isGrounded = false;                                        //Cambiamos a false el booleano de superficie para que se cambie a true cuando detecte una colisión
        }
        
        if(Input.GetKeyDown(KeyCode.LeftShift)&&_dashcooldown_ok)
        {
            _movController.Dash();
            _elapsedash = 0;
            _dashcooldown_ok = false;
        }
        
        // Asignar orientación bala
        Switch();
        
        if(_elapsedash>=_dashcooldown&&_isGrounded)//calcula el cooldown de los dashes (Nota Rafa Malo: Se podria hacer un scrpit que lleve los cooldowns en su update, ya que parece el unico sitio donde funciona)
        {
            _dashcooldown_ok = true;
        }
        else
        {
          _elapsedash += Time.deltaTime;
        }

        Debug.Log(_elapsedash);
    }
}
