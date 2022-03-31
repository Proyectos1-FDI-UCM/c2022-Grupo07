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
    private Vector3 _myMovement = new Vector3(0.1f, 0, 0);
    #endregion

    #region methods
    // Multiplica al vector de movimiento según si va a la izquierda (-1) o a la derecha (1)
    public void SetMovementDirection(int sign)
    {
        _myMovement *= sign;
    }

    // Rota la bala a su orientación contraria si va hacia la izquierda
    public void BulletRotation(int sign)
    {
        if (sign == -1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // Asigna una nueva velocidad 
    public void SetNewSpeed(float multiplier)
    {
        _shotSpeed *= multiplier;
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
