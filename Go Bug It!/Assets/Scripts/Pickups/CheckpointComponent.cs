using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    private Transform _myTransform;
    private SpriteRenderer _mySpriteRenderer;
    private SpriteRenderer _myActivatedCheckpoint;
    #endregion

    #region properties
    private Vector3 _newRespawnPosition;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null)
        {
            _newRespawnPosition = _myTransform.position;
            _myPlayer.SetRespawnPosition(_newRespawnPosition);
            _mySpriteRenderer.enabled = false;
            _myCollider.enabled = false;
            _myActivatedCheckpoint.enabled = true;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider = GetComponent<Collider2D>();
        _myActivatedCheckpoint = GetComponentInChildren<SpriteRenderer>();
    }
}
