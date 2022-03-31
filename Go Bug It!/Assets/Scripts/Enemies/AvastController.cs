using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvastController : MonoBehaviour
{

    #region references
    private Transform _mytransform;
    private Animator _myanimator;
    [SerializeField] private LineRenderer _myRay;
    #endregion

    #region parameters
    [SerializeField] private float _rayCoolDown;
    [SerializeField] private float _rayDuration;
    [SerializeField] private Vector2 _rayDirection;//Dirección del raycast
    [SerializeField] private float _rayOffset; //Para subir o bajar el rayo respecto a Avast (visual) 
    [SerializeField] private float _firstTimeOffset;//Sirve para ajustar los disparos de un avas tras otro.
    #endregion

    #region properties
    private float _elapsedCoolDown;
    private float _elapsedDuration;
    private bool _shooting;
    private bool _firstTimeShoot;//Identifica si es la primera vez que se dispara.
    private int _ignoreLayer = (1<< 2)|(1<<9)|(1<<8);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mytransform = transform;
        _elapsedCoolDown = 0;
        _elapsedDuration = 0;
        _shooting = false;
        _myanimator = GetComponent<Animator>();
        _firstTimeShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shooting) _elapsedCoolDown += Time.deltaTime*GameManager.Instance._speedmod; // Podemos dejar una parte del cooldown con la animaciónd e idle y otra con la de carga
        
        if (_shooting) _elapsedDuration += Time.deltaTime* GameManager.Instance._speedmod;
        else if (!_shooting) _elapsedDuration = 0;

        _myanimator.SetFloat("_elapsedCoolDown", _elapsedCoolDown);
        _myanimator.speed =1* GameManager.Instance._speedmod; // Adecua la animación al spam

        if (_firstTimeShoot && _elapsedCoolDown >= _rayCoolDown + _firstTimeOffset)
        {
            _shooting = true;
            _firstTimeShoot = false;
        }
        else if (!_firstTimeShoot && _elapsedCoolDown >= _rayCoolDown) _shooting = true;

        if (_shooting == true)
        {
            if (_elapsedDuration <= _rayDuration)
            {
                // Llamar a la animación de disparo
                RaycastHit2D hit2D = Physics2D.Raycast(_mytransform.position, _rayDirection.normalized, 1000, ~_ignoreLayer);
                // Debug.Log(hit2D.collider.gameObject.name);
                _myRay.enabled = true;
                _myRay.SetPosition(0, _mytransform.position+_rayOffset*_mytransform.up); //Se renderiza la linea desde la posición del avast al choque con collider.
                _myRay.SetPosition(1, hit2D.point);

                if (hit2D)
                {
                    PlayerLifeComponent _player = hit2D.collider.gameObject.GetComponent<PlayerLifeComponent>();

                    if (_player != null) _player.CallForDamage();

                    EnemyLifeComponent _myEnemy = hit2D.collider.gameObject.GetComponent<EnemyLifeComponent>();

                    if (_myEnemy != null)
                    {
                        NortonComponent _myNorton = hit2D.collider.gameObject.GetComponent<NortonComponent>();

                        if (_myNorton != null) _myNorton.Activated();
                        else _myEnemy.Dies();
                    }
                }
            }
            else
            {
                _shooting = false;
                _myRay.enabled = false; //Se deja de ver el rayo.
                _elapsedDuration = 0;
                _elapsedCoolDown = 0;
            }
        }
    }
}
