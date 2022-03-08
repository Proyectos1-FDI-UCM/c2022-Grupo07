using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGoal : MonoBehaviour
{
    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeComponent _player;
        _player = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if(_player!=null)
        {
            GameManager.Instance.OnGoalAdvance();
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
