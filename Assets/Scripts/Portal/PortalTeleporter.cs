using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

	public Transform player;
	public Transform reciever;

	public bool playerIsOverlapping = false;

	// Update is called once per frame
	void Update()
	{
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{

				//중력 확인
				if (!player.transform.Find("Main Camera").GetComponent<CameraGravityInfo>().CameraIsInGravity)
				{
					//////거리
					///

					//플레이어와 포탈의 거리를 잰다.
					Vector3 playerOffsetFromPortal = player.position - transform.position;

					////해당 스크립트를 가지고 있는 카메라가 비추는 정면의 포탈 각도를 구한다. 
					Vector3 therot = reciever.transform.eulerAngles - transform.eulerAngles;

					//포탈이 180도 돌아간 상태로 (in, out)이 설정되어 있음으로 그 값은 빼준다.
					therot.y += 180;

					//포탈의 거리 벡터를 회전각도만큼 같이 회전시킨다.
					playerOffsetFromPortal = Quaternion.Euler(therot.x, therot.y, therot.z) * playerOffsetFromPortal;

					//플레이어 위치 이동
					player.transform.position = reciever.transform.position + playerOffsetFromPortal;

					//////회전
					///


					//바라보는 각도
					Vector3 PlayerLookPoint = Quaternion.Euler(therot.x, therot.y, therot.z) * player.forward;

					player.transform.rotation = Quaternion.LookRotation(PlayerLookPoint, Vector3.up);

				}

                else
				{
					//////거리
					///

					//플레이어와 포탈의 거리를 잰다.
					Vector3 playerOffsetFromPortal = player.position - transform.position;

					////해당 스크립트를 가지고 있는 카메라가 비추는 정면의 포탈 각도를 구한다. 
					Vector3 therot = reciever.transform.eulerAngles - transform.eulerAngles;

					//포탈이 180도 돌아간 상태로 (in, out)이 설정되어 있음으로 그 값은 빼준다.
					therot.y += 180;

					//포탈의 거리 벡터를 회전각도만큼 같이 회전시킨다.
					//playerOffsetFromPortal = Quaternion.Euler(therot.x, therot.y, therot.z) * playerOffsetFromPortal;

					//플레이어 위치 이동
					player.transform.position = reciever.transform.position + playerOffsetFromPortal;



					//////회전
					///


					player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 180f);

				}

				playerIsOverlapping = false;

			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
