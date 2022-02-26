using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralizeShootComponent : MonoBehaviour
{
    #region parameters
    private float _elapsedTime;
    [SerializeField] private int _neutralizeDuration = 5;
    int i = 0;
    #endregion

    #region references
    private GameObject _enemy;
    private Collider2D _enemyCollider;
    private Rigidbody2D _enemyRigidbody;
    #endregion

    #region properties
    [SerializeField] private bool _neutralized = false;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLifeComponent _bulletCollision = collision.gameObject.GetComponent<PlayerLifeComponent>();
        Debug.Log(_bulletCollision);

        if (_bulletCollision != null)
        {
            if (_neutralized == false)
            {
                Debug.Log("MÉTODO");
                _neutralized = true;
                NeutralizeEnemy();
            }
        }
    }

    // Neutraliza al enemigo
    private void NeutralizeEnemy()
    {
        _enemyRigidbody.simulated = false;
    }

    // Reactiva el enemigo
    private void DeNeutralizeEnemy()
    {
        _enemyRigidbody.simulated = true;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _enemy = gameObject;
        _enemyCollider = _enemy.GetComponent<Collider2D>();
        _enemyRigidbody = _enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_neutralized == true)
        {
            if (i == 0) { Debug.Log("NEUTRALIZED"); i++; }
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _neutralizeDuration)
            {
                _neutralized = false;
                Debug.Log("DENEUTRALIZED");
                DeNeutralizeEnemy();
            }
        }
    }
}
