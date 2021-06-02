using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoom : MonoBehaviour
{
    bool the_LastRoom_Enter = false;

    public CharOnFloor theLast_Floor;
    string theMatname;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //클리어 박스를 전부 확인해서 전부 클리어가 된 상태일 경우
            //마지막 방을 갈수 있게 열어둔다
            for(int i = 0; i < GameManager.Instance.ClearBox.Count - 1; i++)
            {
                theMatname = GameManager.Instance.ClearBox[i].GetComponent<Renderer>().material.name;

                if (!(theMatname.Substring(0,7) == GameManager.Instance.M_Clear.name))
                {
                    the_LastRoom_Enter = false;
                    break;
                }

                else
                {
                    the_LastRoom_Enter = true;
                }
            }


            if (the_LastRoom_Enter)
            {
                theLast_Floor.the_Trigger = true;
            }
        }
    }
}
