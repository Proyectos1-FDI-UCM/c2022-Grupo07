using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravBulletComponent : MonoBehaviour
{
    #region references
    private Animator _myAnimator;
    private BulletMovementController _myMovementController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravEnemyComponent _enemy = collision.gameObject.GetComponent<GravEnemyComponent>();
        if (_enemy != null)
        {
            _myMovementController.enabled = false;
            transform.GetChild(0).localScale = new Vector3 (0.5f, 0.5f, 0);
            _myAnimator.SetBool("OnEnemyCollision", true);
            Destroy(gameObject, 0.8f);
        }

        else if (!collision.isTrigger)
        {
            PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
            if (_myPlayer == null)
            {
                _myMovementController.enabled = false;
                transform.GetChild(0).localScale = new Vector3(0.3f, 0.3f, 0);
                _myAnimator.SetBool("OnWallCollision", true);
                Destroy(gameObject, 0.25f);
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<BulletMovementController>();
        _myAnimator = transform.GetChild(0).GetComponent<Animator>();
    }
}
