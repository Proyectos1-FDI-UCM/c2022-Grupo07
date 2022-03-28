using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeBullet : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame


    #region parameters
    [SerializeField]
    private float _shotSpeed = 2.0f;
    [SerializeField]
    private bool left;
    private Collider2D _myCollider;
    #endregion

    #region properties
    private Vector3 _direction;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null)
        {
            StartCoroutine(_myPlayer.hurted());
        }

        NortonComponent _myNorton = collision.gameObject.GetComponent<NortonComponent>();
        if (_myNorton != null)
        {
            _myNorton.Activated();
        }

        Destroy(gameObject);
    }

    public void SetDirection(Vector3 _orientation)
    {
        _direction = _orientation;
    }
    #endregion

    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Asignar velocidad según la dirección
        transform.Translate(_shotSpeed * _direction * Time.deltaTime*GameManager.Instance._speedmod);
    }
}

