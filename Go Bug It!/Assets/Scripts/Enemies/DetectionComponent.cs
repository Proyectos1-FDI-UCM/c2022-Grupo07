using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    #region references
    private NortonComponent _myNorton;
    private McAfeeComponent _myMcAfee;
    [SerializeField] private bool _isMcAfee = false;
    #endregion
    
    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerLifeComponent _player = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_player != null && !NeuState())
        {
            if (_isMcAfee) _myMcAfee.Shoot();
            else { _myNorton.Activated(); Debug.Log("Dispara"); }
        }
    }
    #endregion

    public bool NeuState()
    {
        if (_isMcAfee) return _myMcAfee._neutralized;
        else return _myNorton._neutralized;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_isMcAfee) _myMcAfee = GetComponentInParent<McAfeeComponent>();
        else _myNorton = GetComponentInParent<NortonComponent>();
    }
}
