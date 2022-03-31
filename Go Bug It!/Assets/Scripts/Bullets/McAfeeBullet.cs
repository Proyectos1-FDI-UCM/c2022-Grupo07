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
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null) _myPlayer.CallForDamage();

        NortonComponent _myNorton = collision.gameObject.GetComponent<NortonComponent>();
        if (_myNorton != null) _myNorton.Activated();

        Destroy(gameObject);
    }

    // Aplica una orientación a la bala
    public void SetDirection(Vector3 _orientation)
    {
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

