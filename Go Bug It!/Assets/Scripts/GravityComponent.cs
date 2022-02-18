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
    #endregion

    #region methods
    // Cambio de gravedad
    public void ChangeGravity(bool grav)
    {
        if (grav) _myRigidbody.gravityScale = -_myGravityScale;
        else _myRigidbody.gravityScale = _myGravityScale;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }
}
