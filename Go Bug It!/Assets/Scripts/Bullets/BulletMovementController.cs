using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementController : MonoBehaviour
{

    #region references
    [SerializeField] private InputController _myInputController;
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField] private float _shotSpeed = 1.0f;
    #endregion

    #region properties
    private bool _direction;
    private Vector3 _myMovement = new Vector3(0.1f, 0, 0);
    #endregion

    #region methods
    // Devuelve true si va hacia la derecha, false si va hacia la izquierda
    public void SetMovementDirection(int sign)
    {
        _myMovement *= sign;
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
        _myTransform.Translate(_myMovement * _shotSpeed * Time.deltaTime);
    }
}
