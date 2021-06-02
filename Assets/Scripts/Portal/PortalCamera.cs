using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{

	public Transform playerCamera;
	public Transform portal;
	public Transform otherPortal;


	// Update is called once per frame
	void Update()
	{
		//////거리
		///

		//플레이어와 포탈의 거리를 잰다.
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;

		//해당 스크립트를 가지고 있는 카메라가 비추는 정면의 포탈 각도를 구한다. 
		Vector3 therot = portal.transform.eulerAngles - otherPortal.transform.eulerAngles;

		//포탈의 거리 벡터를 회전각도만큼 같이 회전시킨다.
		playerOffsetFromPortal = Quaternion.Euler(therot.x, therot.y, therot.z) * playerOffsetFromPortal;

		//카메라가 비출 포탈에서 유저-포탈 만큼의 거리를 둔다.
		transform.position = portal.position + playerOffsetFromPortal;

		//////회전
		///

		Vector3 CameraLookPoint = Quaternion.Euler(therot.x, therot.y, therot.z) * playerCamera.forward;

		transform.rotation = Quaternion.LookRotation(CameraLookPoint, Vector3.up);

		if (playerCamera.GetComponent<CameraGravityInfo>().CameraIsInGravity)
        {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
				180f);

		}



    }
}
