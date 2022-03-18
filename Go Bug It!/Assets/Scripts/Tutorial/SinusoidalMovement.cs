using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{

    #region properties
    private float _originalYPosition;
    #endregion

    #region parameters
    [SerializeField] private float _yAmplitude;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _originalYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, _originalYPosition + Mathf.Sin(Time.time) * _yAmplitude, transform.position.z);
    }
}
