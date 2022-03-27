using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    #region references
    [SerializeField]
    private GameObject CamManager;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& !collision.isTrigger)
        {
            CamManager.SetActive(true);
        }
    } 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& !collision.isTrigger)
        {
            CamManager.SetActive(false);
        }
    }
    #endregion

}
