using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonComponent : MonoBehaviour
{
    #region parameters
    private float _range;
    //private bool _activated = false;
    #endregion

    #region references
    private Transform _myTransform;
    private GameObject _myPlayer;
    private CircleCollider2D _myRango;
    private Animator anim;
    private Animator _rangeAnim;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    private float _targetDistance;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si es el jugador
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null) _myPlayer.Damage();

        // Si es un enemigo
        EnemyLifeComponent _myEnemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (_myEnemy != null)
        {
            // Si es un Norton
            NortonComponent _otherNorton = collision.gameObject.GetComponent<NortonComponent>();
            if (_otherNorton != null) _otherNorton.Activated();
            else _myEnemy.Dies();
        }
    }

    // Activar la animación anterior a la explosión
    public void Activated()
    {
        anim.SetBool("Activated", true);
    }

    // Explosión (evento en animación)
    public void Explode()
    {
        anim.SetBool("Explosion", true);
        _rangeAnim.SetTrigger("Explosion");
        _myRango.enabled = true;
        Destroy(gameObject, 0.5f);
    }

    // 
    public void AlreadyExploded()
    {
        _myRango.enabled = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        _myRango = transform.GetChild(0).GetComponent<CircleCollider2D>();
        _rangeAnim = transform.GetChild(0).GetComponent<Animator>();
        _myRango.enabled = false;

        _range = _myRango.gameObject.transform.localScale.x * Mathf.Pow(0.54f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (_myPlayer != null)
        {
            _targetDistance = Mathf.Abs(Vector2.Distance(_myPlayer.transform.position, _myTransform.position));
            if (_targetDistance <= _range) Activated();
        }
    }
}