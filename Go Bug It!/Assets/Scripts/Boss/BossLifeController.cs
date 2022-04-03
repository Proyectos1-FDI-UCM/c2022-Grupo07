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
    #region references
    [SerializeField] GameObject[] brokens;
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
    private void Update()
    {
        if (_currentLife <= (3f / 4f) * _lifePoints) brokens[0].SetActive(true);
        if (_currentLife <= (2f / 4f) * _lifePoints) brokens[1].SetActive(true);
        if (_currentLife <= (1f / 4f) * _lifePoints) brokens[2].SetActive(true);
    }
}
