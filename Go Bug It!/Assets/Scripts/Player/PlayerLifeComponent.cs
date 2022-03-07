using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{

    #region parameters
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
    #endregion

    #region methods
    private void Damage()
    {
        _currLife -= _hitDamage;
        GameManager.Instance.OnPlayerDamage(_currLife);
        transform.position = new Vector2(_respawnX, _respawnY);
    }

    public void Heal()
    {
        if (_currLife < _playerLife)
        {
            _currLife++;
            GameManager.Instance.OnPlayerHeals(_currLife);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con un enemigo
        EnemyComponent _enemy = collision.gameObject.GetComponent<EnemyComponent>();
        
        if (_enemy != null)
        {
            NeuEnemyComponent _neuEnemy = _enemy.GetComponent<NeuEnemyComponent>();

            if (_neuEnemy != null)
            {
                if (_neuEnemy.GetNeutralization() != true) Damage();
            } 
        }

        // Colisión con la zona de muerte
        DeathZoneComponent _deathZone = collision.gameObject.GetComponent<DeathZoneComponent>();
        
        if (_deathZone != null)
        {
            Damage();
            transform.position = new Vector2(_respawnX, _respawnY);
        }
    }

    public void SetRespawnPosition(Vector3 _newRespawnPosition)
    {
        _respawnX = _newRespawnPosition.x;
        _respawnY = _newRespawnPosition.y;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currLife = _playerLife;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_currLife == 0) Destroy(gameObject);
    }
}
