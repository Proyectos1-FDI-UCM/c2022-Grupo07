using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _range = 4;
    [SerializeField] private float _shootCooldown = 2.0f;
    [SerializeField] private float _offset;
    #endregion

    #region references
    [SerializeField]
    private GameObject _myBullet;
    private Transform _myTransform;
    private GameObject _myPlayer;
    public bool lookingRight = false;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    private float _elapsedTime;
    private float _playerDistance;
    private Vector3 _instancePosition;
    #endregion

    #region methods

    public void Shoot()
    {
        GameObject _bulletShot = GameObject.Instantiate(_myBullet, _instancePosition, Quaternion.identity);
        _bulletShot.GetComponent<McAfeeBullet>().SetDirection(SetBulletDirection());
        _elapsedTime = _shootCooldown;
    }

    public Vector3 SetBulletDirection()
    {
        if (lookingRight) return Vector3.right;
        else return Vector3.left;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = gameObject.transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        _elapsedTime = _shootCooldown;

        if (_myTransform.rotation.y == 180) lookingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (_myPlayer != null)
        {
            _playerDistance = Mathf.Abs(Vector3.Distance(_myPlayer.transform.position, _myTransform.position));

            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0 && _playerDistance <= _range && !_neutralized)
            {
                if (lookingRight) _instancePosition = _myTransform.position + new Vector3(_offset, 0, 0);
                else _instancePosition = _myTransform.position - new Vector3(_offset, 0, 0);
                Shoot();
            }
        }
    }
}

