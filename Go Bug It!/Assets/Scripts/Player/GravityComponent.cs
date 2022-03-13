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
        //Dependiendo del booleano grav se crea una gravedad positiva o negativa
        if (grav) _myRigidbody.gravityScale = -_myGravityScale;
        else _myRigidbody.gravityScale = _myGravityScale;
        gameObject.transform.Rotate(0, 180, 180);

    }

    public void SetNewGravValue(float multiplier)
    {
        _myGravityScale /= multiplier;
        Debug.Log("Valor grav " + _myGravityScale);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();     //Accedo al rigidbody de mi jugador
        _myAnimator = GetComponent<Animator>();
    }
}
