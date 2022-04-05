using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivShieldComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController _myPlayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myPlayer != null)
        {
            if (!_myPlayer.IsPoweredUp())
            {
                _myPlayer.ShieldControl(true);
                _myAnimator.SetTrigger("Picked");
                Destroy(gameObject, 0.75f);
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
