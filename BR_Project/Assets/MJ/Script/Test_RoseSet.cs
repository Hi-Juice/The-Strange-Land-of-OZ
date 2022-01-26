using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_RoseSet : MonoBehaviour
{
    // 4개의 꽃
    // 패턴이 시작되면,
    // - 밑으로 내려가는데 땅을 찍고 내려온다
    // - n번(random)부터 먼저 내려간다 
    // - n초(random) 후에 다른 애가 내려간다
    // - 좌우로 조금씩 움직인다
    public GameObject[] roses;
    int idx;
    Transform roseTr;

    public float fallRoseSpeed = 0.1f;
    bool isStartRoseP = false;
    // Update is called once per frame
    void Update()
    {
        if(!isStartRoseP)
        {
            isStartRoseP = true;
            idx = Random.Range(0, 4);
            StartCoroutine(StartRosePattern());
        }
        
    }

    public int rosePCount = 10;
    public int rosePwaitTime = 2;
    IEnumerator StartRosePattern()
    {
        for (int i=0; i<rosePCount; i++)
        {
            for (int j = 0; j < roses.Length; j++)
            {
                
                float waitTime = Random.Range(0, 2f);

                roseTr = roses[SetRoseIdx(idx)].transform;
                // 떨어지기

                StartCoroutine(DownAndUpRose(roseTr));
                
                yield return new WaitForSeconds(waitTime);
               
                idx++;
            }

            // x값 리셋
            yield return new WaitForSeconds(rosePwaitTime);
        }
        
    }

    int SetRoseIdx(int tmpIdx)
    {
        if(tmpIdx > 3)
        {
            idx = 0;
            return 0;
        }
        else
        {
            return tmpIdx;
        }
    }

    int tmpRoseRan;
    IEnumerator DownAndUpRose(Transform rose)
    {
        float savedXval = rose.position.x;
        tmpRoseRan++;
        bool isGround = false;
        int xDifVal = Random.Range(0, 2);
        while (rose.position.y >= 1 && !isGround)
        {
            rose.position -= new Vector3(0, fallRoseSpeed, 0);
            if(tmpRoseRan % 2 == 0)
            {
                rose.position -= new Vector3(fallRoseSpeed / 4, 0, 0);
            }
            else
            {
                rose.position += new Vector3(fallRoseSpeed / 4, 0, 0);
            }
            
            //Debug.Log("1: roseTr.position.y = " + rose.position.y);
            if (rose.position.y <= 1)
            {
                isGround = true;
            }
            yield return null;
        }

        while (rose.position.y < 10 && isGround)
        {
            rose.position += new Vector3(0, fallRoseSpeed, 0);
            if (tmpRoseRan % 2 == 0)
            {
                rose.position -= new Vector3(fallRoseSpeed / 4, 0, 0);
            }
            else
            {
                rose.position += new Vector3(fallRoseSpeed / 4, 0, 0);
            }
            yield return null;
            //Debug.Log("2: roseTr.position.y = " + rose.position.y);
        }

        rose.position = new Vector3(savedXval, rose.position.y, rose.position.z);
    }
}
