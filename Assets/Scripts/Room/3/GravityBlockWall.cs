using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlockWall : MonoBehaviour
{
    public GameObject BlockWall;

    public GameObject Portal;

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.Find("Main Camera").GetComponent<CameraGravityInfo>().CameraIsInGravity)
            {
                BlockWall.SetActive(true);
                Portal.SetActive(false);
            }
        }
    }
}
