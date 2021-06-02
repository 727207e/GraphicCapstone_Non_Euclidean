using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSetSize : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
