using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _myGravityScale = 1;
    #endregion

    #region references 
    private Rigidbody2D _myRigidbody;
    private Animator _myAnimator;
    #endregion

    #region methods
    // Cambio de gravedad, metodo llamado desde el input controller
    public void ChangeGravity(bool grav)
    {
        _myAnimator.SetBool("OnGravityChange", true);
        //Dependiendo del booleano grav se crea una gravedad positiva o negativa
        if (grav) _myRigidbody.gravityScale = -_myGravityScale;
        else _myRigidbody.gravityScale = _myGravityScale;
        gameObject.transform.Rotate(0, 180, 180);
    }

    // Asigna un nuevo valor a la gravedad
    public void SetNewGravValue(float multiplier)
    {
        _myGravityScale /= multiplier;
        // Debug.Log("Valor grav " + _myGravityScale);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
    }
}
