using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAfeeBullet : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame


    #region parameters
    [SerializeField]
    private float _shotSpeed = 2.0f;
    [SerializeField]
    private bool left;

    #endregion

    #region references
    private InputController _myInputController;
    private Rigidbody2D _myRigidBody;

    #endregion

    #region methods

    #endregion

    // Start is called before the first frame update
    void Start()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        // Asignar velocidad según la dirección
       
        if (left)
        {
            transform.Translate(_shotSpeed * Vector2.left * Time.deltaTime);
        }
        else
        {
            transform.Translate(_shotSpeed * Vector3.right * Time.deltaTime);
        }


    }
}
