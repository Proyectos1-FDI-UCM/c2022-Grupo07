using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuBulletComponent : MonoBehaviour
{
    #region references
    private Animator _myAnimator;
    private BulletMovementController _myMovementController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyComponent _enemy = collision.gameObject.GetComponent<EnemyComponent>();
        // _enemy = null;
        if (_enemy != null)
        {
            _myMovementController.enabled = false;
            transform.GetChild(0).localScale = new Vector3(0.5f, 0.5f, 0);
            _myAnimator.SetBool("OnEnemyCollision", true);
            // Destroy(gameObject);
        }
        else
        {
            _myMovementController.enabled = false;
            transform.GetChild(0).localScale = new Vector3(0.3f, 0.3f, 0);
            _myAnimator.SetBool("OnWallCollision", true);
            // Destroy(gameObject);
        }
    }

    public void OnEnemyOrWallCollision()
    {
        _myAnimator.SetBool("OnEnemyCollision", false);
        _myAnimator.SetBool("OnWallCollision", false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<BulletMovementController>();
        _myAnimator = transform.GetChild(0).GetComponent<Animator>();
    }
}
