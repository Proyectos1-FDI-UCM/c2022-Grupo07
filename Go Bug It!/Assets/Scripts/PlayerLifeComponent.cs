using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    // Starting Life of the player
    [SerializeField]
    private int _playerLife = 4;

    // Current Life of the player
    [SerializeField]
    private int _currLife;

    // Damage taken by player
    [SerializeField]
    private int _hitDamage = 1;

    //Respawn postion X
    [SerializeField]
    private float _respawnX = 0;

    //Respawn position Y
    [SerializeField]
    private float _respawnY = 0;


    private void Damage()
    {
        _currLife -= _hitDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy
        ///<summary>
        ///LifeComponent
        ///Como el checkpoint manager no existe en el primer sprint la implementación 
        ///de la vida del jugador no puede ser implementada totalmente.
        /// </summary>

        if (collision.gameObject.layer == 6||collision.gameObject.layer ==7)
        {
            Damage();
            transform.position = new Vector2(_respawnX, _respawnY);
        }
        //DeathZone 
        //if(collision.gameObject.layer == 7)
        //{
        //    _currLife = 0;
        //}

    }

    void Start()
    {
        _currLife = _playerLife;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(_currLife == 0)
        {
            Destroy(gameObject);
        }
    }
}
