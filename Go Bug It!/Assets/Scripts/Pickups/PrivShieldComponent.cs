using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivShieldComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    #endregion
    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController _myPlayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myPlayer != null)
        {
            _myPlayer.ShieldControl(true);
            Destroy(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
    }
}
