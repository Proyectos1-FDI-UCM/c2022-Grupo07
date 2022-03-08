using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravEnemyComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField]private float _animationCooldown = 1.0f;
    #endregion

    #region references
    private Collider2D _myCollider;
    private Rigidbody2D _myRigidbody;
    private Transform _myTransform;
    [SerializeField]
    private Animator _myAnimator;
    #endregion

    #region properties
    private bool _gravityChanged = true;
    private float _elapsedTime;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravBulletComponent _gravBullet = collision.gameObject.GetComponent<GravBulletComponent>();
        NeuBulletComponent _neuBullet = collision.gameObject.GetComponent<NeuBulletComponent>();

        if (_gravBullet != null) ChangeGravity();
        else if (_neuBullet != null) _myAnimator.SetTrigger("NeuBullet");
    }

    public void ChangeGravity()
    {
        _gravity *= -1;
        _myRigidbody.gravityScale = _gravity;
        _gravityChanged = true;

        if (_gravity < 0) _myAnimator.SetBool("changingGrav", true);
        else _myAnimator.SetBool("changingGrav+", true);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = gameObject.GetComponent<Collider2D>();
        _myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _myTransform = gameObject.transform;

        _elapsedTime = _animationCooldown;
    }

    void Update()
    {
        if (_gravityChanged)
        {
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0 && _gravityChanged)
            {
                _myAnimator.SetBool("changingGrav", false);
                _myAnimator.SetBool("changingGrav+", false);
                _gravityChanged = false;
                _elapsedTime = _animationCooldown;
            }
        }
    }
}
