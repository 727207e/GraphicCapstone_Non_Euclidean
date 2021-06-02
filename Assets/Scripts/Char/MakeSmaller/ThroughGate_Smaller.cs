using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughGate_Smaller : MonoBehaviour
{
    public GameObject StartGate;
    public GameObject EndGate;

    //작아지는 상태 on/off
    bool smallerTrigger = false;

    //오브젝트 스케일(기준이 될 값)
    //작아지는 정도는 절반(Start가 End의 2배이기 때문)
    float Startgate_Scale;

    //플에이어가 게이트 처음에 들어올때 당시의 스케일(기준값)
    Vector3 OldPlayerScale = Vector3.zero;

    //플레이어
    GameObject Player;

    //처음 끝 일정 부분
    float SafeOffset = 2f;






    private void Start()
    {
        Startgate_Scale = StartGate.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //SmallerGate에 들어와있느 상태일때
        if (smallerTrigger)
        {
            //2차 방정식을 활용하여
            //y = -1/2x + 1 을 사용한다.
            //여기서 y는 플에이어의 스케일(localScale), x는 유저가 터널을 들어가는 깊이정도(distance)

            //터널에 들어가 있는 유저의 깊이 정도
            float x = Mathf.InverseLerp(StartGate.transform.position.z + SafeOffset, EndGate.transform.position.z - SafeOffset,
                Player.transform.position.z);

            //작아지는 정도는 절반(Start가 End의 2배이기 때문)
            float y = -0.5f * x + Startgate_Scale;

            //플레이어 크기 조절
            Player.transform.localScale = OldPlayerScale * y;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //기준값을 가져온다.
            OldPlayerScale = other.transform.localScale;

            Player = other.gameObject;

            smallerTrigger = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            smallerTrigger = false;

            //초기화
            Player = null;
        }
    }
}
