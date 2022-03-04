using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuBulletComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider2D;
    private Animator _myAnimator;
    [SerializeField] private RuntimeAnimatorController _bulletAnimation;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyComponent _enemy = collision.gameObject.GetComponent<EnemyComponent>();

        if (_enemy != null)
        {
            _myAnimator.Play("NeuParticles");
            // Destroy(gameObject);
        }
        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider2D = gameObject.GetComponent<Collider2D>();
        _myAnimator = gameObject.GetComponent<Animator>();
    }
}
