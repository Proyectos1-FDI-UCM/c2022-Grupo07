using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement_Controller : MonoBehaviour
{
    #region parameters
    [SerializeField]private float _speed = 2;
    #endregion
    

    // Update is called once per frame
    void Update()
    {
        //Moves boss position horizontally
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
