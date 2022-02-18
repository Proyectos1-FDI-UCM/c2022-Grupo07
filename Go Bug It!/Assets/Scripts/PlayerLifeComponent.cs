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

    private void Damage()
    {
        _currLife -= _hitDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy
        if (collision.gameObject.layer == 6)
        {
            Damage();
        }
        //DeathZone 
        if(collision.gameObject.layer == 7)
        {
            _currLife = 0;
        }

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
