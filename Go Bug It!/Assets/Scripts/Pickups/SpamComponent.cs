using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private GameObject _mytext;
    #endregion

    #region properties
    private float _duration = 0.59f;
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
                _mytext.SetActive(true);
                _mytext.GetComponent<FloatingTextComponent>().Appear(_duration);
                Destroy(gameObject, _duration);
            }
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _mytext = transform.GetChild(0).gameObject;
        _mytext.SetActive(false);
    }
}
