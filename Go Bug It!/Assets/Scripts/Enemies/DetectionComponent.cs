using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    #region references
    private NortonComponent _myNorton;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerLifeComponent _player = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_player != null) { _myNorton.Activated(); Debug.Log("Ayuda"); }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myNorton = GetComponentInParent<NortonComponent>();
    }
}
