using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesencriptarComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MovementController _myPlayer;
        _myPlayer = collision.gameObject.GetComponent<MovementController>();
        if (_myPlayer != null)
        {
            _myPlayer.SetDash();
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
