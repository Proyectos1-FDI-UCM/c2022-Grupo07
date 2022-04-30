using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private GameObject _myText;
    [SerializeField] GameObject _boss;
    #endregion

    #region properties
    private float _duration = 1.01f;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GunpointController _playerGun = collision.gameObject.transform.GetChild(0).GetComponent<GunpointController>();

        if (_playerGun != null)
        {
            _boss.SetActive(true);
            _playerGun.SetDmgShoot();
            GameManager.Instance.OnDmgShootActivate();
            _myAnimator.SetBool("Picked", true);
            _myText.SetActive(true);
            _myText.GetComponent<FloatingTextComponent>().Appear(_duration);
            Destroy(gameObject, 1.01f);
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
