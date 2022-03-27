using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPointerComponent : MonoBehaviour
{

    #region references
    private Collider2D _myCollider;
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController _myPlayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myPlayer != null)
        {
            _myPlayer.StackPointerControl(true);
            _myAnimator.SetTrigger("Picked");
            Destroy(gameObject, 0.92f);
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
