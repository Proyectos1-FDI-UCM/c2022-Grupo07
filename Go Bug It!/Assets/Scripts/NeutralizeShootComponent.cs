using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralizeShootComponent : MonoBehaviour
{
    #region parameters
    private float _elapsedTime;
    [SerializeField] private int _neutralizeDuration = 5;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NeuBullet _bulletCollision = collision.gameObject.GetComponent<NeuBullet>();

        if (_bulletCollision != null)
        {
            if (_neutralized == false) _neutralized = true;
        }
    }

    public bool GetNeutralization()
    {
        return _neutralized;
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
            Debug.Log("NEUTRALIZED");
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _neutralizeDuration)
            {
                _neutralized = false;
                Debug.Log("DENEUTRALIZED");
                _elapsedTime = 0;
            }
        }
    }
}
