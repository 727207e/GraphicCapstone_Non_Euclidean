using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtr : MonoBehaviour
{
    public float speed = 6.0f;      // 캐릭터 움직임 스피드.
    public float jumpSpeed; // 캐릭터 점프 힘.
    public float gravity;    // 캐릭터에게 작용하는 중력.
    float rotSpeed;  //마우스 회전 속도
    public bool isJumping = false; // 점프 트리거

    float mx; // 마우스 x각도
    float my; // 마우스 y각도

    public Transform myCamera;

    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.
    private Rigidbody charRG;

    public float Jump_power;

    public bool Gravity_fine = true;

    //발소리
    IEnumerator footStep_;

    float foot_count = 0;
    float foot_countLimit = 0.5f;

    //레이케스트
    RaycastHit hit;
    float MaxDistance = 15f;

    void Start()
    {
        jumpSpeed = 8.0f;
        gravity = 9.8f;
        rotSpeed = 400.0f;

        MoveDir = Vector3.zero;
        charRG = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //캐릭터 이동
        CharMove();
        //캐릭터 카메라 회전
        CharWatch();
    }


    void CharMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // 위, 아래 움직임 셋팅. 
        MoveDir.x = inputX;
        MoveDir.z = inputZ;

        //// 스피드 증가.
        MoveDir *= speed;

        //// 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        //// 캐릭터 움직임.
        //charRG.velocity = transform.TransformDirection(MoveDir);

        //초기화
        MoveDir.y = 0;

        transform.Translate((new Vector3(inputX, 0, inputZ) * speed) * Time.deltaTime);





        if (!isJumping)
        {
            //움직이고 있는 경우
            if(inputX != 0 || inputZ != 0)
            {
                if(footStep_ == null)
                {
                    footStep_ = footStepSoundCoroutine();

                    StartCoroutine(footStep_);
                }
            }

            //움직이지 않고 발자국 코루틴이 있는경우
            else
            {
                if(footStep_ != null)
                {
                    StopCoroutine(footStep_);
                    footStep_ = null;
                }
            }



            //중력에 문제가 없을경우
            if (Gravity_fine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    charRG.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    charRG.AddForce(Vector3.down * jumpSpeed, ForceMode.Impulse);
                }

            }
        }

    }

    void CharWatch()
    {       
        // 마우스 인풋
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //전환
        mx = h * rotSpeed * Time.deltaTime;
        my = v * rotSpeed * Time.deltaTime;

        //최댓값 최솟값 지정
        my = Mathf.Clamp(my, -90, 90);

        //카메라 각도 변환
        myCamera.transform.Rotate(-my, 0, 0);

        // 각도 변환
        transform.Rotate(0, mx, 0);

        //마우스 커서가 보이지 않게 함
        Cursor.visible = false;

        //마우스 커서 잠금
        Cursor.lockState = CursorLockMode.Locked;

        //레이 케스트
        if(Physics.Raycast(transform.position, myCamera.forward,out hit, MaxDistance))
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    IEnumerator footStepSoundCoroutine()
    {

        while(foot_countLimit > foot_count)
        {
            foot_count += Time.deltaTime;

            yield return null;

        }

        //초기화
        foot_count = 0;

        //발소리
        AudioManager.instance.PlaySound("footStep", transform.position);

        footStep_ = null;

    }
}
 
