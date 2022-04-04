using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvastController : MonoBehaviour
{

    #region references
    private Transform _mytransform;
    private Animator _myanimator;

    [SerializeField] private GameObject _myRay;

    private AudioSource _avastSFX;
    [SerializeField] private AudioClip _lasergun;

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
    private bool _isCharging;
    private float anim_speed;
    private bool _activateSound;
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
        _isCharging = false;
        anim_speed = 1;
        _avastSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shooting) _elapsedCoolDown += Time.deltaTime*GameManager.Instance._speedmod; // Podemos dejar una parte del cooldown con la animaciónd e idle y otra con la de carga
        
        if (_shooting) _elapsedDuration += Time.deltaTime* GameManager.Instance._speedmod;
        else if (!_shooting) _elapsedDuration = 0;
        if (_elapsedCoolDown >= (3f / 5f) * _rayCoolDown && !_shooting)
        {
            _isCharging = true;
            anim_speed = 1 * (5 / _rayCoolDown);

        }
        else anim_speed = 1;
        _myanimator.speed =anim_speed* GameManager.Instance._speedmod; // Adecua la animación al spam

        if (_firstTimeShoot && _elapsedCoolDown >= _rayCoolDown + _firstTimeOffset)
        {
            _shooting = true;
            _firstTimeShoot = false;
        }
        else if (!_firstTimeShoot && _elapsedCoolDown >= _rayCoolDown) _shooting = true;

        if (_shooting == true)
        {
            if(_activateSound==true)//llama al disparo una sola vez
            {
                _avastSFX.PlayOneShot(_lasergun);
                _activateSound = false;
            }
            if (_elapsedDuration <= _rayDuration)
            {
                _myRay.SetActive(true);
                // Llamar a la animación de disparo
                RaycastHit2D hit2D = Physics2D.Raycast(_mytransform.position, _rayDirection.normalized, 1000, ~_ignoreLayer);
                // Debug.Log(hit2D.collider.gameObject.name);
                _myRay.GetComponent<LineRenderer>(). enabled = true;
                _myRay.GetComponent<LineRenderer>().SetPosition(0, _mytransform.position+_rayOffset*_mytransform.up); //Se renderiza la linea desde la posición del avast al choque con collider.
                _myRay.GetComponent<LineRenderer>().SetPosition(1, hit2D.point);

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
                _isCharging = false;
                _activateSound = true;
                _shooting = false;
                _myRay.GetComponent<LineRenderer>().enabled = false; //Se deja de ver el rayo.
                _myRay.SetActive(false);
                _elapsedDuration = 0;
                _elapsedCoolDown = 0;
               
            }
        }
        _myanimator.SetBool("_isCharging", _isCharging);
    }
}
