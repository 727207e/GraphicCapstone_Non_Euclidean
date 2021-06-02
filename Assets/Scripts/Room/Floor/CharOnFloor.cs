using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharOnFloor : MonoBehaviour
{
    public WantToChangeCameraPos[] cameraGroups;

    public ActiveList[] actvieObject;
    public InactiveList[] inactvieObject;

    public bool the_Trigger = true;

    //구조체
    [System.Serializable]
    public class WantToChangeCameraPos
    {
        public Camera ToChangeCamera;
        public Transform nowPortal;
        public Transform nowOtherPortal;
    }
    //구조체
    [System.Serializable]
    public class ActiveList
    {
        public GameObject objects;
    }
    //구조체
    [System.Serializable]
    public class InactiveList
    {
        public GameObject objects;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Trigger_collider_do();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Trigger_collider_do();
        }
    }


    void Trigger_collider_do()
    {
        if (the_Trigger)
        {
            //변경해야 하는 포탈들 변경 업데이트
            for (int i = 0; i < cameraGroups.Length; i++)
            {
                cameraGroups[i].ToChangeCamera.GetComponent<PortalCamera>().portal
                    = cameraGroups[i].nowPortal;
                cameraGroups[i].ToChangeCamera.GetComponent<PortalCamera>().otherPortal
                    = cameraGroups[i].nowOtherPortal;
            }

            //활성화 해야하는 오브젝트 활성화
            for (int i = 0; i < actvieObject.Length; i++)
            {
                actvieObject[i].objects.SetActive(true);
            }
            //비활성화 해야하는 오브젝트 비활성화
            for (int i = 0; i < inactvieObject.Length; i++)
            {
                inactvieObject[i].objects.SetActive(false);
            }
        }
    }
}
