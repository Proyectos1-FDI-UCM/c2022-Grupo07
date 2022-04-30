using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecryptComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private GameObject _myText;
    #endregion

    #region properties
    private float _duration = 0.67f;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        InputController _myPlayer = collision.gameObject.GetComponent<InputController>();

        if (_myPlayer != null)
        {
            _myPlayer.SetElapsedDash(10); //Setea el cooldown para que se disponga del dash de nuevo.
            _myAnimator.SetTrigger("Picked");
            _myText.SetActive(true);
            _myText.GetComponent<FloatingTextComponent>().Appear(_duration);
            Destroy(gameObject, _duration);
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
