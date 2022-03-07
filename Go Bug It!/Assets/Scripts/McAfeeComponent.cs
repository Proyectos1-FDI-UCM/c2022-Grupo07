using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _range = 4;
    #endregion

    #region references
    [SerializeField]
    private GameObject _myBullet;
    private Transform _myTransform;
    private GameObject _myPlayer;
    private double _lastShot;
    [SerializeField] private GameObject _myOffsetDisparo;
    [SerializeField] private bool lookingRight = false;
    #endregion

    #region properties
    private Vector2 _targetPosition;
    #endregion

    #region methods
    
    public void Shoot()
    {  
        if (Vector2.Distance(_myPlayer.transform.position, transform.position) < _range && Time.time > _lastShot + 1)
        {
            Vector3 posBullet = _myOffsetDisparo.transform.position;
            Instantiate(_myBullet, posBullet, Quaternion.identity);
            _lastShot = Time.time;
        }
        Debug.Log("distancia entre el enemigo y el jugador: " + Vector2.Distance(_myPlayer.transform.position, transform.position));   
        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
       
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
 
    }

    // Update is called once per frame
    void Update()
    {    
       
        if (_myPlayer.transform.position.x - transform.position.x > 0 && lookingRight)
        {
            Shoot();
           
        }
        else if (_myPlayer.transform.position.x - transform.position.x <= 0 && !lookingRight)  
        {
            Shoot();
            
        } 
    }
}
