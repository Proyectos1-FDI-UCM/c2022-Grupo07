using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnManager : MonoBehaviour
{

    #region parameters
    [SerializeField] private GameObject _enemyPrefab;
    private Transform _myTransform;
    [SerializeField] private float _respawnTimer = 1.5f;
    private int _counter = 1;
    #endregion

    #region references
    [SerializeField] private GameObject _enemy;
    private GameObject InstantiatedEnemy;
    private float _gravity;

    
    #endregion

    #region methods
    private IEnumerator RespawnEnemy()
    {
        _counter = 0; // Para hacer que se instancie solo una vez el if antes de instanciar el enemigo
        yield return new WaitForSeconds(_respawnTimer);
        InstantiatedEnemy = Instantiate(_enemyPrefab, _myTransform);
        if(_enemyPrefab.GetComponent<GravEnemyComponent>() != null)
        {
            _enemyPrefab.GetComponent<GravEnemyComponent>().SetGravity(_gravity);
        }
            

        _enemy = InstantiatedEnemy;
        _counter = 1; //Restablece la posibilidad de respawnear al enemigo
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        if(_enemy.GetComponent<GravEnemyComponent>() != null)
        {
            _gravity =_enemy.GetComponent<GravEnemyComponent>().ReturnGravity(); 
        }

        if (_enemy.transform.GetChild(2).GetComponent<GravEnemyComponent>() != null)
        {
            _gravity = _enemy.transform.GetChild(2).GetComponent<GravEnemyComponent>().ReturnGravity();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si el enemigo existe
        if(_enemy == null&& _counter == 1)
        {
            StartCoroutine(RespawnEnemy());
        }
    }
}
