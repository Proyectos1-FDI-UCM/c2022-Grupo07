using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _range = 1;
    //private bool _activated = false;
    #endregion

    #region references
    private Transform _myTransform;
    private GameObject _myPlayer;
    private CircleCollider2D _myRango;
    private Animator _myAnimator;
    private Animator _rangeAnim;
    [SerializeField] private GameObject _sfx;
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

        if (_myPlayer != null && !_myPlayer.gameObject.GetComponent<PowerUpController>().IsShieldActive()) _myPlayer.CallForDamage();

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
        _myAnimator.SetBool("Activated", true);
    }

    // Explosión (evento en animación)
    public void Explode()
    {
        _sfx.GetComponent<SoundEffectController>().PlaySound("explosion");
        _myAnimator.SetBool("Explosion", true);
        _rangeAnim.SetTrigger("Explosion");
        _myRango.enabled = true;
        Destroy(gameObject, 0.5f/GameManager.Instance._speedmod);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        _myAnimator = GetComponent<Animator>();
        _myRango = transform.GetChild(0).GetComponent<CircleCollider2D>();
        _rangeAnim = transform.GetChild(0).GetComponent<Animator>();
        _myRango.enabled = false;

        //_range = _myRango.gameObject.transform.localScale.x * _myRango.radius;
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