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
        ///<summary>
        ///LifeComponent
        ///Como el checkpoint manager no existe en el primer sprint la implementación 
        ///de la vida del jugador no puede ser implementada totalmente.
        /// </summary>

        if (collision.gameObject.layer == 6||collision.gameObject.layer ==7)
        {
            //if (collision.gameObject.GetComponent<NeuEnemyComponent>().GetNeutralization() == false)
            {
                Damage();
                transform.position = new Vector2(_respawnX, _respawnY);
            }    
        }
        //DeathZone 
        //if(collision.gameObject.layer == 7)
        //{
        //    _currLife = 0;
        //}
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
