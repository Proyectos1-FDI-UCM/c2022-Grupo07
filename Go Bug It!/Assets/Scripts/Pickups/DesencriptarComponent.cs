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
        InputController _myPlayer;
        _myPlayer = collision.gameObject.GetComponent<InputController>();
        if (_myPlayer != null)
        {
            _myPlayer._elapseDash = 10; //Setea el cooldown para que se disponga del dash de nuevo.
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
