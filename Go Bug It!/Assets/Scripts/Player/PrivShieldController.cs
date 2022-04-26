using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivShieldController : MonoBehaviour
{

    #region references
    private PowerUpController _myPowerUpController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && !collision.isTrigger) _myPowerUpController.ShieldControl(false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPowerUpController = GetComponentInParent<PowerUpController>();
    }
}
