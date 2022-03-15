using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamComponent : MonoBehaviour
{
    #region references
    Collider2D _myCollider;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController _myplayer;
        _myplayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myplayer != null)
        {
            _myplayer.SpamControl(true);
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
