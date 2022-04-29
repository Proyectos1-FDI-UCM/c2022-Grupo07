using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DmgBulletComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private BulletMovementController _myMovementController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();

        if (_enemy != null)
        {
                _myMovementController.enabled = false;
                transform.GetChild(0).localScale = new Vector3(0.5f, 0.5f, 0);
                _myAnimator.SetBool("OnEnemyCollision", true);
            if (_enemy.gameObject.GetComponentInParent<McAfeeComponent>() != null || _enemy.gameObject.GetComponentInParent<NortonComponent>() != null) _enemy.Dies();
            Destroy(gameObject, 0.5f);
            

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
