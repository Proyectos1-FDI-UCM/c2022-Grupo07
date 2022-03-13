using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvastController : MonoBehaviour
{

    #region references
    private Transform _mytransform;
    private Animator _myanimator;
    #endregion

    #region parameters
    [SerializeField] private float _rayCoolDown;
    [SerializeField] private float _rayDuration;
    [SerializeField] private Vector2 _rayDirection;//Direcci�n del raycast
    #endregion

    #region properties
    private float _elapsedCoolDown;
    private float _elapsedDuration;
    private bool _shooting;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mytransform = transform;
        _elapsedCoolDown = 0;
        _elapsedDuration = 0;
        _shooting = false;
        _myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shooting) _elapsedCoolDown += Time.deltaTime; //Podemos dejar una parte del cooldown con la animaci�nd e idle y otra con la de carga
        if (_shooting) _elapsedDuration += Time.deltaTime;
        else if (!_shooting) _elapsedDuration = 0;
        _myanimator.SetFloat("_elapsedCoolDown", _elapsedCoolDown);
        _myanimator.SetBool("_shooting", _shooting);
        if (_elapsedCoolDown >= _rayCoolDown)
        {
            _shooting = true;
        }
        if (_shooting == true)
        {
            if (_elapsedDuration <= _rayDuration)
            {
                //Llamar a la animaci�n de disparo
                RaycastHit2D hit2D = Physics2D.Raycast(_mytransform.position, _rayDirection.normalized, 1000,3);
                if(hit2D)
                {
                  PlayerLifeComponent _player = hit2D.collider.gameObject.GetComponent<PlayerLifeComponent>();
                  if (_player != null)
                  {
                    _player.Damage();
                    Debug.Log("Pillado");
                  }
                }
            }
            else
            {
                _shooting = false;
                _elapsedDuration = 0;
                _elapsedCoolDown = 0;
            }
        }
    }
}
