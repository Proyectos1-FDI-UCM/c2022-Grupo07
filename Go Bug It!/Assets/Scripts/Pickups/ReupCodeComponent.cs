using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReupCodeComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private GameObject _myFloatingText;
    #endregion

    #region properties
    private float _duration = 1.67f;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if(_myPlayer != null)
        {
            _myPlayer.Heal();
            _myAnimator.SetTrigger("Picked");
            _myFloatingText.SetActive(true);
            _myFloatingText.GetComponent<FloatingTextComponent>().Appear(_duration);
            Destroy(gameObject, _duration);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myFloatingText = transform.GetChild(0).gameObject;
        _myFloatingText.SetActive(false);
    }
}
