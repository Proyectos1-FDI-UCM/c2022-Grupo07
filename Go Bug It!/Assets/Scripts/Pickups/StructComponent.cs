using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PowerUpController _myPlayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myPlayer != null)
        {
            if (!_myPlayer.IsPoweredUp())
            {
                _myPlayer.StructControl(true);
                _myAnimator.SetTrigger("Picked");
                Destroy(gameObject, 1.2f);
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
