using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltF4MovingImage : MonoBehaviour
{
    #region references
    private Transform _altf4Transform;
    #endregion

    #region parameters
    [SerializeField] private float _altf4Speed;
    private Vector3 _xVector = new Vector3(10, 0, 0);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _altf4Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _altf4Transform.Translate(_xVector * _altf4Speed * Time.deltaTime);
    }
}
