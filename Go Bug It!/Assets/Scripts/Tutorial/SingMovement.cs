using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingMovement : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    #region properties
    private float x;
    private float y;
    private float _time;
    #endregion

    #region parameters
    [SerializeField] private float _xAmplitude;
    [SerializeField] private float _yAmplitude;
    [SerializeField] private float _xOmega;
    [SerializeField] private float _yOmega;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform; 
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        x = _xAmplitude * Mathf.Cos(_xOmega * _time);
        y = Mathf.Abs(_yAmplitude * Mathf.Sin(_yOmega * _time));
        _myTransform.position = new Vector3(x, y, 0);
    }
}
