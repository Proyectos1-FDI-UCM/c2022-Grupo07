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
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    private float _targetDistance;
    #endregion

    #region methods
    // Activar la animación anterior a la explosión
    public void Activated()
    {
        _myAnimator.SetBool("Activated", true);
    }

    // Explosión (evento en animación)
    public void Explode()
    {
        _myAnimator.SetBool("Explosion", true);
        _rangeAnim.SetTrigger("Explosion");
        Debug.Log("Rango exp activado");
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
    }
}