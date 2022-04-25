using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    [SerializeField] GameObject _boss;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GunpointController _playerGun = collision.gameObject.transform.GetChild(0).GetComponent<GunpointController>();

        if (_playerGun != null)
        {
            //_boss.SetActive(true);
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
        _myAnimator = GetComponent<Animator>();
    }
}
