using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    #region references
    private Transform _mytransform;
    #endregion

    #region properties
    private Vector2 _startposition;
    #endregion

    #region parameters
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movementDirection;
    #endregion

    #region methods
    // Hacer hijo al jugador si está en la plataforma
    public void OnCollisionEnter2D(Collision2D collision)
    {
        InputController _player = collision.gameObject.GetComponent<InputController>();
        if (_player != null) collision.gameObject.transform.parent = gameObject.transform;
    }

    // Soltar al jugador si deja de estar en la plataforma
    public void OnCollisionExit2D(Collision2D collision)
    {
        InputController _player = collision.gameObject.GetComponent<InputController>();
        if (_player != null) collision.gameObject.transform.parent = null;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mytransform = transform;
        _startposition = _mytransform.position;
        _movementDirection = _movementDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (_movementDirection.x == 0)
        {
            if ((_mytransform.position.y <= _startposition.y- _distance || _mytransform.position.y  >= _startposition.y + _distance))
            {
                _movementDirection.y = -_movementDirection.y;
            }
        }
        else if (_movementDirection.y == 0)
        {
            if ((_mytransform.position.x <= _startposition.x - _distance || _mytransform.position.x >= _startposition.x + _distance))
            {
                _movementDirection.x = -_movementDirection.x;
            }
         }
        _mytransform.Translate(_movementDirection * _speed * Time.deltaTime);
    }
}


