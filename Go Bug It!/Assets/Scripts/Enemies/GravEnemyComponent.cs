using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravEnemyComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _animationCooldown = 1.0f;
    #endregion

    #region references
    private Rigidbody2D _myRigidbody;
    [SerializeField]
    private Animator _myAnimator;
    [SerializeField] private GameObject _sfx;
    #endregion

    #region properties
    private bool _gravityChanged = true;
    private float _elapsedTime;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravBulletComponent _gravBullet = collision.gameObject.GetComponent<GravBulletComponent>();
        NeuBulletComponent _neuBullet = collision.gameObject.GetComponent<NeuBulletComponent>();

        if (_gravBullet != null) ChangeGravity();
        else if (_neuBullet != null)
        {
            _myAnimator.SetBool("NeuBullet", true);
            StartCoroutine(WDInmune());
        }
    }

    // Cambio de gravedad
    public void ChangeGravity()
    {
        _sfx.GetComponent<SoundEffectController>().PlaySound("gravity");
        _gravity *= -1;
        _gravityChanged = true;

        if (_gravity < 0) _myAnimator.SetBool("ChangingGrav", true);
        else _myAnimator.SetBool("ChangingGrav+", true);
    }

    // Animación de inmunidad del Windows Defender que retrasa la asignación de NeuBullet
    IEnumerator WDInmune()
    {
        yield return new WaitForSeconds(0.84f);
        _myAnimator.SetBool("NeuBullet", false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _myRigidbody.gravityScale = _gravity;
        _elapsedTime = _animationCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gravityChanged)
        {
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0 && _gravityChanged)
            {
                _myAnimator.SetBool("ChangingGrav", false);
                _myAnimator.SetBool("ChangingGrav+", false);
                _gravityChanged = false;
                _elapsedTime = _animationCooldown;
            }
        }

        if (GameManager.Instance._spam) _myRigidbody.gravityScale = _gravity * GameManager.Instance._speedmod;
        else _myRigidbody.gravityScale = _gravity;
    }
}
