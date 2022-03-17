using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonsExplosionCOntroller : MonoBehaviour
{
    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if(_myPlayer != null) _myPlayer.Damage();

        EnemyLifeComponent _myEnemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (_myEnemy != null) _myEnemy.Dies();

        NortonComponent _otherNorton = collision.gameObject.GetComponent<NortonComponent>();
        if (_otherNorton != null) _otherNorton.Activated();
    }
    #endregion
}
