using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_life_controller : MonoBehaviour
{
    #region parameters
    [SerializeField] private int life_points = 100;
     private int current_life;
    #endregion
    #region references
    private BulletMovementController _bulletMovementController;
    #endregion
    #region methods
    public void Damage()
    {
        current_life = current_life-1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletMovementController bullet = collision.gameObject.GetComponent<BulletMovementController>();
        if (bullet != null)
        {
            Damage();
        }
        if(current_life <= 0)
        {
            GameManager.Instance.OnBossDies();
            Destroy(gameObject);
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        current_life = life_points;
        _bulletMovementController = GetComponent<BulletMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
