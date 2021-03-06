using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{

    #region references
    private Transform _myTransform;
    private Collider2D _myCollider;
    private Animator _myAnimator;
    private GameObject _myFloatingText;
    #endregion

    #region properties
    private Vector3 _newRespawnPosition;
    #endregion

    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_myPlayer != null)
        {
            _newRespawnPosition = _myTransform.position;
            _myPlayer.SetRespawnPosition(_newRespawnPosition);
            _myCollider.enabled = false;
            _myAnimator.SetBool("activated", true);
            _myFloatingText.SetActive(true);
            _myFloatingText.GetComponent<FloatingTextComponent>().Appear(1.5f);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myAnimator = GetComponent<Animator>();
        _myCollider = GetComponent<Collider2D>();
        _myFloatingText = transform.GetChild(0).gameObject;
        _myFloatingText.SetActive(false);
    }
}
