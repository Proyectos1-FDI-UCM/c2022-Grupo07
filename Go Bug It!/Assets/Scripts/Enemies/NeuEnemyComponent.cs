using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuEnemyComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private McAfeeComponent _enemyM;
    private NortonComponent _enemyN;
    #endregion

    #region parameters
    private float _elapsedTime;
    [SerializeField] private float _neutralizeDuration = 5;
    public bool _enemyNorton;
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
            if (_neutralized == false)
            {
                //Si me colisiona una bala neuralizadora y no estoy neutralizado ya activo la animacion y desactivo los componentes
                _myAnimator.SetBool("Neutralize", true);
                if (_enemyNorton) { _enemyN._neutralized = true; _enemyN.enabled = false; }
                else { _enemyM._neutralized = true; _enemyM.enabled = false; }
                _neutralized = true;
            }
            else if (_neutralized && _elapsedTime >= -7.5)
            {
                if (_enemyNorton) _enemyN._neutralized = true;
                else _enemyM._neutralized = true;
                _elapsedTime -= _neutralizeDuration;
            }
        }
    }

    // Devuelve la variable de neutralizado
    public bool GetNeutralization()
    {
        //Devuelve el estado de neutralizacion del enemigo
        return _neutralized;
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //Dependiendo de que tipo de enemigo es se le asignan unas variables u otras
        if (_enemyNorton)
        {
            _enemyN = GetComponentInParent<NortonComponent>();
            _myAnimator = GetComponentInParent<Animator>();
        }
        else
        {
            _enemyM = GetComponentInParent<McAfeeComponent>();
            _myAnimator = GetComponentInParent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_neutralized == true)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _neutralizeDuration)
            {
                _neutralized = false;

                if (_enemyNorton) _enemyN.enabled = true;
                else { _enemyM._neutralized = false; _enemyM.enabled = true; }
                _myAnimator.SetBool("Neutralize", false);
                _elapsedTime = 0;
            }
        }
    }
}
