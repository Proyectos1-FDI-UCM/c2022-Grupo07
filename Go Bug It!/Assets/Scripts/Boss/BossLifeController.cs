using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeController : MonoBehaviour
{

    #region parameters
    [SerializeField] private int _lifePoints = 100;
    private int _currentLife;
    #endregion

    #region methods
    public void Damage()
    {
        _currentLife--;
        if (_currentLife <= 0)
        {
            GameManager.Instance.OnBossDies();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletMovementController bullet = collision.gameObject.GetComponent<BulletMovementController>();
        
        if (bullet != null) Damage();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentLife = _lifePoints;
    }
}
