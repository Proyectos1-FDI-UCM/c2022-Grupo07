using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPointerComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private GameObject _myText;
    #endregion

    #region properties
    private float _duration = 0.92f;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PowerUpController _myPlayer = collision.gameObject.GetComponent<PowerUpController>();

        if (_myPlayer != null)
        {
            if (!_myPlayer.IsPoweredUp())
            {
                _myPlayer.StackPointerControl(true);
                _myAnimator.SetTrigger("Picked");
                _myText.SetActive(true);
                _myText.GetComponent<FloatingTextComponent>().Appear(_duration);
                Destroy(gameObject, _duration);
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myText = transform.GetChild(0).gameObject;
        _myText.SetActive(false);
    }
}
