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
        Debug.Log(_currLife);
        GameManager.Instance.OnPlayerDamage(_currLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con un enemigo
        EnemyComponent _enemy = collision.gameObject.GetComponent<EnemyComponent>();
        
        if (_enemy != null)
        {
            NeuBulletComponent _bullet = _enemy.GetComponent<NeuBulletComponent>();
            Debug.Log(_bullet);

            if (_bullet == null) // Si el enemigo no está neutralizado
            {
                Damage();
                transform.position = new Vector2(_respawnX, _respawnY);
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
