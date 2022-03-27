using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController _myplayer;
        _myplayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myplayer != null)
        {
            _myplayer.SpamControl(true);
            _myAnimator.SetTrigger("Picked");
            Destroy(gameObject, 0.59f);
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
