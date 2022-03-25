using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private GameObject _Prefab;
    private Transform _myTransform;
    [SerializeField] private float _respawnTimer = 1.5f;
    private int _counter = 1;
    #endregion
    #region references
    [SerializeField] private GameObject _InitialObject;
    private GameObject _InstantiatedObject;
    #endregion
    #region methods
    private IEnumerator RespawnEnemy()
    {
        _counter = 0; // Para hacer que se instancie solo una vez el if antes de instanciar el objeto
        yield return new WaitForSeconds(_respawnTimer);
        _InstantiatedObject = Instantiate(_Prefab, _myTransform);
        _InitialObject = _InstantiatedObject;
        _counter = 1; //Restablece la posibilidad de respawnear al objeto
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el enemigo existe
        if(_InitialObject == null&& _counter == 1)
        {
            StartCoroutine(RespawnEnemy());
        }
    }
}
