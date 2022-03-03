using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CamManager;

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

}
