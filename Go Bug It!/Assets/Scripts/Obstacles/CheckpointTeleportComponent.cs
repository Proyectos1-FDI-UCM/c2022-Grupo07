using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTeleportComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float X_teleport;
    [SerializeField] private float Y_teleport;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _player;
        _player = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (_player != null)
        {
            _player.transform.position = new Vector2(X_teleport, Y_teleport);
        }
    }
    #endregion

}
