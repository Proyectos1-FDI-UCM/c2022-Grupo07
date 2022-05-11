using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeComponent : MonoBehaviour
{

    #region references
    private Animator _myAnimator;
    private Rigidbody2D _myRigidbody;
    private BoxCollider2D _myCollider;
    private InputController _myInput;
    private BoxCollider2D _respawnBox1;
    private BoxCollider2D _respawnBox2;
    private AudioSource _playerLifeSFX;
    #endregion

    #region parameters
    bool _isDying;
    // Boss Gameobject
    [SerializeField] private GameObject _boss;
    // Respawn point
    [SerializeField] private GameObject _respawn;
    // Respawn aid timer
    [SerializeField] private float _respawnTimerAid = 1;
    // Starting Life of the player
    [SerializeField] private int _playerLife = 4;
    // Current Life of the player
    [SerializeField] private int _currLife;
    // Damage taken by player
    [SerializeField] private int _hitDamage = 1;
    //Respawn postion X
    [SerializeField] private float _respawnX = 0;
    //Respawn position Y
    [SerializeField] private float _respawnY = 0;
    [SerializeField] AudioClip[] _audioClips;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con un enemigo
        EnemyLifeComponent _enemy = collision.gameObject.GetComponentInChildren<EnemyLifeComponent>();
        BossLifeController _boss = collision.gameObject.GetComponent<BossLifeController>();
        DeathZoneComponent _deathZone = collision.gameObject.GetComponent<DeathZoneComponent>();

        //Si encuentro un enemigo y no esta neutralizado me hago daño
        if (_enemy != null)
        {
            NeuEnemyComponent _neuEnemy = _enemy.GetComponentInChildren<NeuEnemyComponent>();

            if (_neuEnemy != null)
            {
                if (_neuEnemy.GetNeutralization() != true) StartCoroutine(hurted(1.65f));
            }
        }
        else if (_boss != null) StartCoroutine(hurted(1.65f));
        else if (_deathZone != null) StartCoroutine(hurted(0));
    }

    // Corrutina al reaparecer generando ayudas
    IEnumerator respawn_aid()
    {
        //Timescale is reduced to give player time to react
        Time.timeScale = 0.2f;

        //Activates Respawn aid colliders and platforms.
        //Colliders are activated due to the moving platform script needing them activated to drag the player with them
        _respawn.transform.GetChild(0).gameObject.SetActive(true);
        _respawn.transform.GetChild(1).gameObject.SetActive(true);
        _respawnBox1.enabled = true;
        _respawnBox2.enabled = true;
        yield return new WaitForSecondsRealtime(_respawnTimerAid);

        //Timescale reverted
        Time.timeScale = 1f;

        //Platforms last a bit longer than the slowdown timer
        yield return new WaitForSecondsRealtime(_respawnTimerAid);

        //Deactivates colliders in order to avoid errors when disabling gameobject
        _respawnBox1.enabled = false;
        _respawnBox2.enabled = false;

        //Desactivates aiding platforms completely
        _respawn.transform.GetChild(0).gameObject.SetActive(false);
        _respawn.transform.GetChild(1).gameObject.SetActive(false);
    }

    // Corrutina al ser dañado
    IEnumerator hurted(float _firstWaitTime)
    {
        // Activar animación de daño si no está muriendo
        if(!_isDying) _myAnimator.SetBool("Hurted", true);
        // Deshabilitar movimiento y físicas
        _myInput.enabled = false;
        _myRigidbody.simulated = false;
        _myCollider.enabled = false;
        
        // Esperar al fin de la animación llamando entremedias al GameManager
        yield return new WaitForSeconds(_firstWaitTime);
        GameManager.Instance.OnPlayerDamage(_currLife - _hitDamage);
        yield return new WaitForSeconds(0.25f);

        // Llamada al método de daño
        Damage();
        if (_currLife >= 1) _playerLifeSFX.PlayOneShot(_audioClips[0]);//Hit

        // Activar animación de idle, movimiento y físicas
        _myAnimator.SetBool("Hurted", false);
        _myInput.enabled = true;
        _myRigidbody.simulated = true;
        _myCollider.enabled = true;
    }

    // Método accedido por los enemigos para hacer daño
    public void CallForDamage()
    {
        StartCoroutine(hurted(1.65f));
    }

    // Daño. Actualiza la vida y decide si el jugador a muerto o no
    private void Damage()
    {
        // Actualizar vida
        _currLife -= _hitDamage;

        if (_currLife <= 0) Dies(); // Si muere, llamada al método de muerte
        else                        // Si no muere
        {
            if (_myRigidbody.gravityScale < 0)  // Si tiene una gravedad negativa
            {
                gameObject.transform.Rotate(0, 180, 180);
                _myRigidbody.gravityScale = 2;
                _myRigidbody.velocity = new Vector2(0, 0);
                _myInput.SetGravity(!_myInput.GetGravity());
            }

            if (_boss != null) // Si existe el boss
            {
                transform.position = new Vector2(_respawn.transform.position.x, _respawn.transform.position.y);
                StartCoroutine(respawn_aid());
            }
            else transform.position = new Vector2(_respawnX, _respawnY);
        }
        
    }

    // Curación
    public void Heal()
    {
        if (_currLife < _playerLife)
        {
            _currLife++;
            GameManager.Instance.OnPlayerHeals(_currLife);
        }
        else GameManager.Instance.UpdatePoints(200);
        _playerLifeSFX.PlayOneShot(_audioClips[1]);//Heal
    }

    // Muerte del jugador
    public void Dies()
    {
        if (!_isDying)
        {
            _myAnimator.SetBool("Dies", true);
            _playerLifeSFX.PlayOneShot(_audioClips[2]);//gameover
            _isDying = true;
            Destroy(gameObject, 1.1f);
            GameManager.Instance.OnPlayerDies();
        }
    }

    // Actualiza la posición de reaparición del jugador al tocar un checkpoint
    public void SetRespawnPosition(Vector3 _newRespawnPosition)
    {
        _respawnX = _newRespawnPosition.x;
        _respawnY = _newRespawnPosition.y;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlayerRegistrationTrue(gameObject);
        
        // Inicialización de variables
        _currLife = _playerLife;
        _myAnimator = GetComponent<Animator>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myInput = GetComponent<InputController>();
        _myCollider = GetComponent<BoxCollider2D>();
        _isDying = false;

        if (_boss != null)
        {
            _respawnBox1 = _respawn.transform.GetChild(0).gameObject.transform.GetComponent<BoxCollider2D>();
            _respawnBox2 = _respawn.transform.GetChild(1).gameObject.transform.GetComponent<BoxCollider2D>();
            StartCoroutine(respawn_aid());
        }
        _playerLifeSFX = GetComponent<AudioSource>();
    }
}
