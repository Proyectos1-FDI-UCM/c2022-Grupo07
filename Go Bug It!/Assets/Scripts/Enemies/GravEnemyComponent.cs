using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravEnemyComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _gravity = 1.0f;
    #endregion

    #region references
    private Collider2D _myCollider;
    private Rigidbody2D _myRigidbody;
    private Transform _myTransform;
    [SerializeField]
    private Animator _myAnimator;
    #endregion
    
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravBulletComponent _myBullet = collision.gameObject.GetComponent<GravBulletComponent>();
        
        if (_myBullet != null) ChangeGravity();
    }

    public void ChangeGravity()
    {
        //_myTransform.Rotate(180, 0, 0);
        _gravity *= -1;
        _myRigidbody.gravityScale = _gravity;

        _myAnimator.SetBool("changingGrav", true);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = gameObject.GetComponent<Collider2D>();
        _myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _myTransform = gameObject.transform;
    }
}
