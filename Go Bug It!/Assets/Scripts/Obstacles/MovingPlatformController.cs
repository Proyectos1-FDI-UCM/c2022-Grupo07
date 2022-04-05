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
    private Vector2 _movementRight = new Vector2(0.1f, 0);
    private Vector2 _movementLeft = new Vector2(-0.1f, 0);
    #endregion

    #region parameters
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;
    [SerializeField] private float limPlatformMax_X;
    [SerializeField] private float limPlatformMin_X;
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

    // Comprueba la dirección de la plataforma y aplica el movimiento correspondiente
    public void Movement()
    {
        if (transform.position.x > limPlatformMax_X) _direction = -1;          

        else if (transform.position.x < limPlatformMin_X) _direction = 1;

        gameObject.transform.Translate(transform.right * Time.deltaTime * _speed * _direction);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mytransform = transform;
        _startposition = _mytransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}

