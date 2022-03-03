using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuEnemyComponent : MonoBehaviour
{
    #region parameters
    private float _elapsedTime;
    [SerializeField] private int _neutralizeDuration = 5;
    #endregion

    #region properties
    [SerializeField] private bool _neutralized = false;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NeuBulletComponent _bulletCollision = collision.gameObject.GetComponent<NeuBulletComponent>();

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
