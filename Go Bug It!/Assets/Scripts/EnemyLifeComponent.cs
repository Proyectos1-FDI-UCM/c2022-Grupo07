using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    int _puntuation;
    #endregion
    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==7)// Si el enemigo toca una deathzone, muere, se destruye.
        {
            GameManager.Instance.Puntuation(_puntuation);
            Destroy(this.gameObject);
            //Habr� que llamar al game manager para contabilizar los puntos
            Debug.Log("Muerte");
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
