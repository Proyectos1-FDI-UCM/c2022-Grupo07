using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shotSpeed = 1.0f;
    private MovementController _myMovementController;
    bool velocityRight;
    bool velocityLeft;
   
    #endregion
    private void Awake()
    {
        _myMovementController = FindObjectOfType<MovementController>();
    }
    private bool SetMovementDirection()
    {
        if(_myMovementController.GetDirection() > 0)
        {
            
            return true;
           
        }

        else if (_myMovementController.GetDirection() < 0)
        {
           
            return false;
            
        }
        return true;
    }
  
    
    private void velocityright()
    {
        transform.Translate(_shotSpeed * Vector3.right * Time.deltaTime);
    }
    private void velocityleft()
    {
        transform.Translate(_shotSpeed * Vector3.left * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
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
    // Update is called once per frame
    void Update()
    {
        if(velocityRight == true)
        {
            velocityright();
        }
        if (velocityLeft == true)
        {
            velocityleft();
        }
       
    }
}
