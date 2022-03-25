using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] string _nextSecene = "";
    [SerializeField] float _newLevelDuration = 60;
    #endregion

    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _myPlayer = collision.gameObject.GetComponent<PlayerLifeComponent>();

        if (_myPlayer != null)
        {
            GameManager.Instance.OnGoalAdvance(_nextSecene, _newLevelDuration);
            Destroy(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }
}
