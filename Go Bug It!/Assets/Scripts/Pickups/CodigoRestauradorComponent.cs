using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoRestauradorComponent : MonoBehaviour
{
    #region parameters

    #endregion

    #region references
    GameManager _myGameManager;
    Collider2D _myCollider;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer;
        _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if(_myPlayer != null)
        {
            _myPlayer.Heal();
            Destroy(gameObject);
        }
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
    }
}
