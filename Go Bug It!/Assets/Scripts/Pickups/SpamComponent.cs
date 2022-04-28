using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PowerUpController _myplayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myplayer != null)
        {
            if (!_myplayer.IsPoweredUp())
            {
                _myplayer.SpamControl(true);
                _myAnimator.SetTrigger("Picked");
                Destroy(gameObject, 0.59f);
            }
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }
}
