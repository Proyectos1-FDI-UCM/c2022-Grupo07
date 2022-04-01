using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _shootCooldown = 2.0f;
    [SerializeField] private float _offset;
    #endregion

    #region references
    [SerializeField] private GameObject _myBullet;
    private Transform _myTransform;
    private BoxCollider2D _myDetector;
    private bool lookingRight = false;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    private float _elapsedTime;
    private bool canShoot = false;
    private Vector3 _instancePosition;
    #endregion

    #region methods
    // Disparo de McAfee
    public void Shoot()
    {
        _myDetector.enabled = false;
        if (canShoot && !_neutralized)
        {
            if (lookingRight) _instancePosition = _myTransform.position + new Vector3(_offset, 0, 0);
            else _instancePosition = _myTransform.position - new Vector3(_offset, 0, 0);

            GameObject _bulletShot = GameObject.Instantiate(_myBullet, _instancePosition, Quaternion.identity);
            _bulletShot.GetComponent<McAfeeBullet>().SetDirection(SetBulletDirection());
            canShoot = false;
            _elapsedTime = _shootCooldown;
        }
    }

    // Asignar la dirección de la bala
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
        _elapsedTime = _shootCooldown;

        if (_myTransform.rotation.y == 180) lookingRight = true;
        if (!lookingRight) _myDetector = transform.GetChild(0).GetComponent<BoxCollider2D>();
        _myDetector.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            _elapsedTime -= Time.deltaTime * GameManager.Instance._speedmod;
            if (_elapsedTime <= 0)
            {
                canShoot = true;
                _myDetector.enabled = true;
            }
        }
    }
}

