using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravBulletComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
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
