using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingNortonInteraction : MonoBehaviour
{
    #region references
    [SerializeField] NortonComponent Norton;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        Norton.Activated();
        Destroy(gameObject, 0.5f);
    }
    #endregion
}
