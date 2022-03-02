using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeComponent : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    [SerializeField]
    private GameObject _myBullet;
    private Transform _myTransform;
    private Collider2D _myDetectionZone;
    #endregion

    #region properties
    private Vector2 _targetPosition;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer;
        _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        RaycastHit2D _hitInfo = new RaycastHit2D();
        if (_myPlayer != null)
        {
            int _layerMask = _myPlayer.gameObject.layer;
            _targetPosition = (_myTransform.position - _myPlayer.transform.position);
            
            //Physics2D.Raycast(_myTransform.position, _targetPosition, _hitInfo, _layerMask, 10000);
            //Physics2D.Raycast(_myTransform.position, _targetPosition, 1000, )
        }
    }

    public void Shoot()
    {

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myDetectionZone = GetComponentInChildren<Collider2D>();
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
