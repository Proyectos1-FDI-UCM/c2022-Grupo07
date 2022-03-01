using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==7)// Si el enemigo toca una deathzone, muere, se destruye.
        {
            Destroy(this.gameObject);
            //Habrá que llamar al game manager para contabilizar los puntos
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
