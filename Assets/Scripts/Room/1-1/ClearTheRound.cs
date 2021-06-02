using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheRound : MonoBehaviour
{
    //플레이어가 여길 지나간 횟수
    int count = 0;

    //지나간 횟수이후 변화
    public int Count_Limit = 3;

    public Camera ToChangeCamera;
    public Transform nowPortal;
    public Transform nowOtherPortal;

    public GameObject OpenPortal;
    public GameObject InactivePortal;

    // Update is called once per frame
    void Update()
    {
        if(count >= Count_Limit)
        {
            ToChangeCamera.GetComponent<PortalCamera>().portal = nowPortal;
            ToChangeCamera.GetComponent<PortalCamera>().otherPortal = nowOtherPortal;

            OpenPortal.SetActive(true);
            InactivePortal.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            count++;
        }
    }
}
