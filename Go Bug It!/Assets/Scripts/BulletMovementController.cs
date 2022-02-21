using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _shotSpeed = 1.0f;
    bool velocityRight;
    bool velocityLeft;
    #endregion

    #region references
    private InputController _myInputController;
    #endregion

    #region methods
    // Devuelve true si va hacia la derecha, false si va hacia la izquierda
    private bool SetMovementDirection()
    {
        if(_myInputController.GetDirection() > 0) return true;
        else if (_myInputController.GetDirection() < 0) return false;

        return true;
    }

    // Movimiento aplicado hacia la derecha
    private void velocityright()
    {
        transform.Translate(_shotSpeed * Vector3.right * Time.deltaTime);
    }

    // Movimiento aplicado hacia la izquierda
    private void velocityleft()
    {
        transform.Translate(_shotSpeed * Vector3.left * Time.deltaTime);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Comprobar si la bala va hacia la derecha o a la izquierda
        if(SetMovementDirection() == true)
        {
            velocityRight = true;
            velocityLeft = false;
        }
        else if (SetMovementDirection() == false)
        {
            velocityLeft = true;
            velocityRight = false;
        }
    }

    void Awake()
    {
        _myInputController = FindObjectOfType<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Asignar velocidad según la dirección
        if (velocityRight == true) velocityright();
        if (velocityLeft == true) velocityleft();
    }
}
