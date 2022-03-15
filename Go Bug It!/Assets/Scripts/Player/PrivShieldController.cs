using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivShieldController : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    private PowerUpController _myPowerUpController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) _myPowerUpController.ShieldControl(false);
    }
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<CircleCollider2D>();
        _myPowerUpController = GetComponentInParent<PowerUpController>();
    }
}
