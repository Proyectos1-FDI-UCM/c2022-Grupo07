using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterComponent : MonoBehaviour
{
    #region parameters
    //public bool _isExit;
    #endregion

    #region References
    [SerializeField] private Vector3 _myDestination;
    private Collider2D _myCollider;
    #endregion

    #region properties
    private GameObject _playerObj;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null)
        {
            _playerObj = _myPlayer.gameObject;
            StartCoroutine(DeactivatesInput());
            _playerObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _myPlayer.transform.position = _myDestination;
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
}
