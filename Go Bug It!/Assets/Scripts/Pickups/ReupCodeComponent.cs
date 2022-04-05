using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReupCodeComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if(_myPlayer != null)
        {
            _myPlayer.Heal();
            _myAnimator.SetTrigger("Picked");
            Destroy(gameObject, 0.59f);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }
}
