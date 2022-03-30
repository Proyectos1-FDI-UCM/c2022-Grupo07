using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectorComponent : MonoBehaviour
{
    #region references
    private InputController _myInput;
    #endregion

    #region properties
    private bool _isGrounded = false;
    #endregion

    #region methods
    // Saber si el jugador esta tocando una superficie o no
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            _isGrounded = true;
            _myInput.SetGrounded(_isGrounded);
        }
        
    }

    // Marcar que el jugador no está tocando el suelo
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            _isGrounded = false;
            _myInput.SetGrounded(_isGrounded);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myInput = GetComponentInParent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
