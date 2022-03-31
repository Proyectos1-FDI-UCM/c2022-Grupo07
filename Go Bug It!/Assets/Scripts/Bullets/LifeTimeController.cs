using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeController : MonoBehaviour
{

    #region parameters
    [SerializeField] private int _lifeTime = 2;
    private float _elapsedTime;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Contenedor de tiempo
        _elapsedTime += Time.deltaTime;

        // Destruir bala
        if (_elapsedTime > _lifeTime) Destroy(gameObject);
    }
}
