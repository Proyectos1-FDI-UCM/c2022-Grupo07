using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _shootCooldown = 2.0f;
    [SerializeField] private float _offset;
    [SerializeField] private AudioClip audioClip;
    #endregion

    #region references
    [SerializeField] private GameObject _myBullet;
    private Transform _myTransform;
    private AudioSource _mcAfeeSFX;
    private BoxCollider2D _myDetector;
    [SerializeField]private bool lookingRight = false;
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
        //Mientras que no este neutralizado y aun no puedo disparar rebajo el cooldown
        if (!canShoot && !_neutralized)
        {
            _elapsedTime -= Time.deltaTime * GameManager.Instance._speedmod;
            if (_elapsedTime <= 0) canShoot = true;
        }
        //Si el cooldown ya es 0 y no estoy neutralizado disparo
        else if (canShoot && !_neutralized)
        {
            //Dependiendo de donde miro instancio la bala en el offset positivo o negativo
            if (lookingRight) _instancePosition = _myTransform.position + new Vector3(_offset, 0, 0);
            else _instancePosition = _myTransform.position - new Vector3(_offset, 0, 0);
            
            //Le aplico la orientacion a la bala al instanciarla, devuelvo el cooldown a su valor inicial y no puedo disparar
            GameObject _bulletShot = GameObject.Instantiate(_myBullet, _instancePosition, Quaternion.identity);
            _bulletShot.GetComponent<McAfeeBullet>().SetDirection(SetBulletDirection());
            _mcAfeeSFX.PlayOneShot(audioClip);
            canShoot = false;
            _elapsedTime = _shootCooldown;
        }
    }

    // Asignar la dirección de la bala
    public Vector3 SetBulletDirection()
    {
        //La direccion de la bala dependera de hacia donde mire el McAfees
        if (lookingRight) return Vector3.right;
        else return Vector3.left;
    }

    // Desactiva el collider de detección de diparo cuando muere
    public void Death()
    {
        _myDetector.enabled = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos nuestro transform e iniciamos el contador con el valor de cooldown
        _myTransform = gameObject.transform;

        //Dependiendo de la rotacion del objeto sabemos si apunta a la derecha o no
        if (_myTransform.rotation.y == 180) lookingRight = true;
        
        //cogemos el detector del jugador mediante codigo
        _myDetector = transform.GetChild(0).GetComponent<BoxCollider2D>();
        _mcAfeeSFX = GetComponent<AudioSource>();
    }
}

