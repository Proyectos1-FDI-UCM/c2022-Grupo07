using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _slowValue = 0.5f;
    [SerializeField]
    private float _duration;
    private float _elapsedTime = 0;
    private bool _activated = false;
    #endregion

    #region references
    Collider2D _myCollider;
    SpriteRenderer _mySpriteRenderer;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myplayer;
        _myplayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (_myplayer != null)
        {
            _mySpriteRenderer.enabled = false;
            _activated = true;
            //En la version final este objeto no reducira la velocidad del jugador
            Time.timeScale = _slowValue;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime >= _duration)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        if (_activated)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
