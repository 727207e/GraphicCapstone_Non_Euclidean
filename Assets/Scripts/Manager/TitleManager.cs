using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject clickToContinue;
    bool Continue = false;

    Coroutine coroutine_Blink;


    // Start is called before the first frame update
    void Start()
    {
        if(clickToContinue != null)
        coroutine_Blink = StartCoroutine(Blink()); // press any button 깜박임

    }

    // Update is called once per frame
    void Update()
    {
        //아직 아무키도 못눌렀다면
        if (!Continue)
        {
            //아무키나 누르면 진행
            if (Input.anyKey)
            {
                SceneManager.LoadScene("GameScene");
            }
        }

    }

    IEnumerator Blink()
    {
        while (!Continue)
        {
            clickToContinue.SetActive(true);
            yield return new WaitForSeconds(1f);
            clickToContinue.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

}
