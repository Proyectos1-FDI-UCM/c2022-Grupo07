using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeController : MonoBehaviour
{
    #region references
    private Animator _myAnimator;
    private BossMovementController _myBossMovement;
    [SerializeField]private GameObject _bossSpawn;
    #endregion
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
            _myAnimator.SetBool("Death", true);
            Destroy(_bossSpawn);
            _myBossMovement.SetSpeedZero();
            GameManager.Instance.EndGame(1.1f);
            Destroy(gameObject, 1.1f);
            
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
        _myBossMovement = GetComponentInParent<BossMovementController>();
        _myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_currentLife <= (3f / 4f) * _lifePoints) brokens[0].SetActive(true);
        if (_currentLife <= (2f / 4f) * _lifePoints) brokens[1].SetActive(true);
        if (_currentLife <= (1f / 4f) * _lifePoints) brokens[2].SetActive(true);
    }
}
