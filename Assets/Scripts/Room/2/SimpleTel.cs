using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTel : MonoBehaviour
{
    public float FunctionDelaytime = 0;

    public GameObject reciever;
    public GameObject Player;

    void teleportPos()
    {
        Player.transform.position = reciever.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Invoke("teleportPos", FunctionDelaytime);
        }
    }
}
