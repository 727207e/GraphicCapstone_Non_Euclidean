using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject Player;

    bool is_in = false;

    float theSpeed = 0f;

    float Oldgravity = 0f;
    float OldjumpSpeed = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //단 한번만 가능. 아직 실시가 안되었다면 진행
            if (!is_in)
            {
                Player.gameObject.transform.Find("Main Camera").GetComponent<CameraGravityInfo>().
                    CameraIsInGravity = true;
                Change_Player_Gravity();
            }
        }
    }

    void Change_Player_Gravity()
    {
        CharCtr charctr = Player.GetComponent<CharCtr>();

        Oldgravity = charctr.gravity;
        OldjumpSpeed = charctr.jumpSpeed;

        //한번 활성화되면 두번다시 못하게 막기위한 트리거
        is_in = true;

        //이동제어
        charctr.Gravity_fine = false;

        //코루틴으로 유저를 회전한다
        StartCoroutine(Player_Head_Rotation());
    }

    IEnumerator Player_Head_Rotation()
    {
        bool trigger = true;
        while (trigger)
        {
            //유저가 180도가 될때까지 계속 회전한다
            if (Player.transform.eulerAngles.x <= 180)
                Player.transform.Rotate(1f, 0, 0);

            //180도가 된다면
            else
            {
                //잠금
                trigger = false;

                //다시한번 정확하게 각도를 수정해준다
                Player.transform.eulerAngles = new Vector3(0, Player.transform.eulerAngles.y, 180f);

                //이전에 들고있던 속도를 부여
                Player.GetComponent<CharCtr>().gravity = Oldgravity;
                Player.GetComponent<CharCtr>().jumpSpeed = OldjumpSpeed;

                //속도제어 비활성화
                Player.GetComponent<CharCtr>().Gravity_fine = false;

                Physics.gravity = new Vector3(0, 9.81f, 0);
            }
            yield return null;
        }
    }
}
