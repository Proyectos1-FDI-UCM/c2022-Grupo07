using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterComponent : MonoBehaviour
{
    #region parameters
    public bool _isExit;
    #endregion

    #region References
    [SerializeField] private Transform _myDestination;
    private Collider2D _myCollider;
    #endregion

    #region properties
    private Vector3 _destination;
    private GameObject _playerObj;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null && !_isExit)
        {
            _playerObj = _myPlayer.gameObject;
            _myPlayer.transform.position = _destination;
            StartCoroutine(DeactivatesInput());
        }
    }

    IEnumerator DeactivatesInput()
    {
        _playerObj.GetComponent<InputController>().enabled = false;
        _playerObj.GetComponent<MovementController>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        _playerObj.GetComponent<InputController>().enabled = true;
        _playerObj.GetComponent<MovementController>().enabled = true;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
        _destination = _myDestination.position;
    }
}
