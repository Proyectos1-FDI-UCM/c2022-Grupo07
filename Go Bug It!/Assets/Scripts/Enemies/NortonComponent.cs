using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _range = 4;
    //private bool _activated = false;
    #endregion

    #region references
    private Transform _myTransform;
    private GameObject _myPlayer;
    [SerializeField] private GameObject _myRango;
    private Animator anim;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    private float _targetDistance;
    #endregion

    #region methods
    public void Activated()
    {
        anim.SetBool("Activated", true);
        //_activated = true;
    }
    
    public void Explode()
    {
        anim.SetBool("Explosion", true);
        _myRango.SetActive(true);
        Destroy(gameObject, 1.0f);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Warning())
        {
               //se activaría la animacion de explotar del NOrton
        }*/

        if (_myPlayer != null)
        {
            _targetDistance = Mathf.Abs(Vector2.Distance(_myPlayer.transform.position, _myTransform.position));
            if (_targetDistance <= _range)
            {
                Activated();
            }
        }
    }
}
