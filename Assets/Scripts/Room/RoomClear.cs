using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClear : MonoBehaviour
{
    public int RoomCount;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ClearRoom_ChangeBox(RoomCount);
        }
    }
}
