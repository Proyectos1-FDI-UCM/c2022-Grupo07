using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectComponent : MonoBehaviour
{

    #region references
    private Collider2D _myCollider;
    private Animator _myAnimator;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GunpointController _playerGun = collision.gameObject.transform.GetChild(0).GetComponent<GunpointController>();

        if (_playerGun != null)
        {
            _playerGun.SetDmgShoot();
            GameManager.Instance.OnDmgShootActivate();
            _myAnimator.SetBool("Picked", true);
            Destroy(gameObject, 1.01f);
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
