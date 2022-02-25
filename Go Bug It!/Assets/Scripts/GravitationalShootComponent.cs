using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalShoot : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    private Collider2D _myCollider;
    #endregion

    #region properties
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = gameObject.GetComponent<Collider2D>();
    }
}
