using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimationComponent : MonoBehaviour
{

    #region references
    [SerializeField] private Animation _bulletAnimation;
    [SerializeField] private Animation _wallAnimation;
    private Transform _myTransform;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyComponent _enemy = collision.gameObject.GetComponent<EnemyComponent>();

        if (_enemy != null)
        {

        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }
}
