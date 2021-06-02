using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown_InactiveTerrain : MonoBehaviour
{
    public GameObject Terrain;

    TerrainCollider terrainCollider;


    // Start is called before the first frame update
    void Start()
    {
        terrainCollider = Terrain.GetComponent<TerrainCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //유저가 들어오면 바닥이 꺼진다
        if(other.tag == "Player")
        {
            terrainCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //유저가 나가면 원래대로 돌린다
        if (other.tag == "Player")
        {
            terrainCollider.enabled = true;
        }
    }
}
