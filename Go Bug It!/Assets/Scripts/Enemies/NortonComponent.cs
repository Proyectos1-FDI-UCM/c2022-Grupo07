using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonComponent : MonoBehaviour
{
    #region references
    /*private Transform _myTransform;
    private GameObject _myPlayer;*/
    private CircleCollider2D _myRango;
    private Animator _myAnimator;
    private Animator _rangeAnim;
    private AudioSource _nortonSFX;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    [SerializeField] private AudioClip audioClip;
    //private float _targetDistance;
    #endregion

    #region methods
    // Activar la animación anterior a la explosión
    public void Activated()
    {
        //se activa la animacion
        _myAnimator.SetBool("Activated", true);
    }

    // Explosión (evento en animación)
    public void Explode()
    {
        //Activo la animacion de explosion, mi rango de daño y destruyo todo lo que encuentre en él
        _myAnimator.SetBool("Explosion", true);
        _rangeAnim.SetTrigger("Explosion");
        _nortonSFX.PlayOneShot(audioClip);
        _myRango.enabled = true;
        Destroy(gameObject, 0.5f/GameManager.Instance._speedmod);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myRango = transform.GetChild(0).GetComponent<CircleCollider2D>();
        _rangeAnim = transform.GetChild(0).GetComponent<Animator>();
        _myRango.enabled = false;
        _nortonSFX = GetComponent<AudioSource>();
    }
}