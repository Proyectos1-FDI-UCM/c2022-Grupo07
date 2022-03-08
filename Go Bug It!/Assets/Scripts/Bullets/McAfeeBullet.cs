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
    #endregion
    
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null)
        {
            _myPlayer.Damage();
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Asignar velocidad seg�n la direcci�n

        if (left)
        {
            transform.Translate(_shotSpeed * Vector2.left * Time.deltaTime);
        }
        else
        {
            transform.Translate(_shotSpeed * Vector3.right * Time.deltaTime);
        }
    }
}

