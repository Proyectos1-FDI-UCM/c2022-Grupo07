using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecryptComponent : MonoBehaviour
{

    #region references
    private Collider2D _myCollider;
    private Animator _myAnimator;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        InputController _myPlayer;
        _myPlayer = collision.gameObject.GetComponent<InputController>();
        if (_myPlayer != null)
        {
            _myPlayer.SetElapsedDash(10); //Setea el cooldown para que se disponga del dash de nuevo.
            _myAnimator.SetTrigger("Picked");
            Destroy(gameObject, 0.67f);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
        _myAnimator = GetComponent<Animator>();
    }
}
