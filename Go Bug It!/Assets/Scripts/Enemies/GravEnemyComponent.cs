using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravEnemyComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _gravity = 1.0f;
    #endregion

    #region references
    [SerializeField]
    private Rigidbody2D _myRigidbody;
    [SerializeField]
    private Animator _myAnimator;
    #endregion

    #region properties
    [SerializeField] private bool _isWD = false;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravBulletComponent _gravBullet = collision.gameObject.GetComponent<GravBulletComponent>();
        NeuBulletComponent _neuBullet = collision.gameObject.GetComponent<NeuBulletComponent>();
        //Si me colisiona una bala de grav cambio de gravedad
        if (_gravBullet != null)
        {
            ChangeGravity();
            StartCoroutine(ChanGrav());
        }
        //Si me colisiona una bala de neu y soy WD muestro que soy inmune
        else if (_neuBullet != null && _isWD)
        {
            _myAnimator.SetBool("NeuBullet", true);
            StartCoroutine(WDInmune());
        }
    }

    // Cambio de gravedad
    public void ChangeGravity()
    {

        //Le cambio el signo a mi gravedad
        _gravity *= -1;
        //Dependiendo del signo de la gravedad ejecuto una animacion u otra
        if (_gravity < 0) _myAnimator.SetBool("ChangingGrav", true);
        else _myAnimator.SetBool("ChangingGrav+", true);
    }

    // Animación de inmunidad del Windows Defender que retrasa la asignación de NeuBullet
    IEnumerator WDInmune()
    {
        //Activo la animacion de inmunidad
        yield return new WaitForSeconds(0.84f);
        _myAnimator.SetBool("NeuBullet", false);
    }

    IEnumerator ChanGrav()
    {
        yield return new WaitForSeconds(1);
        _myAnimator.SetBool("ChangingGrav", false);
        _myAnimator.SetBool("ChangingGrav+", false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody.gravityScale = _gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //Si esta activado el powerUp de spam cambio mis parametros de gravedad
        if (GameManager.Instance._spam) _myRigidbody.gravityScale = _gravity * GameManager.Instance._speedmod;
        else _myRigidbody.gravityScale = _gravity;
    }
}
