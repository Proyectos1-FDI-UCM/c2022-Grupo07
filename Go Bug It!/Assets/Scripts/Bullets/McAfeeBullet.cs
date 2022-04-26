using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeBullet : MonoBehaviour
{

    #region parameters
    [SerializeField] private float _shotSpeed = 2.0f;
    [SerializeField] private bool left;
    #endregion

    #region properties
    private Vector3 _direction;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Al chocarme con el player le hago daño
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        NortonComponent _myNorton = collision.gameObject.GetComponentInParent<NortonComponent>();
        PrivShieldController playerShield = collision.gameObject.GetComponent<PrivShieldController>();
        LifeTimeController _otherBullet = collision.gameObject.GetComponent<LifeTimeController>();

        if (!collision.isTrigger)
        {
            //Al chocarme con un norton le activo y contra el player le mato
            if (_myPlayer != null) _myPlayer.CallForDamage();
            else if (_myNorton != null) _myNorton.Activated();
            Destroy(gameObject);
        }
        if (collision.isTrigger)
        {
            if (playerShield != null)
            {
                playerShield.GetComponentInParent<PowerUpController>().ShieldControl(false);
                Destroy(gameObject);
            }

            else if (_otherBullet != null)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    // Aplica una orientación a la bala
    public void SetDirection(Vector3 _orientation)
    {
        //La orientacion que reciba sera la que aplique en mi movimiento
        _direction = _orientation;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Asignar velocidad según la dirección
        transform.Translate(_shotSpeed * _direction * Time.deltaTime*GameManager.Instance._speedmod);
    }
}

