using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpoint : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    private InputController _myinputController;
    #endregion

    #region properties
    [SerializeField]
    private GameObject myShot;
    public Transform Gun;
    #endregion



    #region methods
    public void Shoot()
    {
        GameObject.Instantiate(myShot, _myTransform.position, _myTransform.rotation);
    }
    #endregion

    private void Awake()
    {
        _myinputController = GetComponent<InputController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _myinputController._isGrounded)
        {
            Shoot();
        }
    }
}
