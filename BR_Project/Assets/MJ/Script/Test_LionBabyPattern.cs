using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LionBabyPattern : MonoBehaviour
{

    // 왼쪽과 오른쪽에서 spawn된다
    // - 몇초마다 spawn될지 
    // - 총 몇마리 spawn될지
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isPattern = false;
    void Update()
    {
        if(!isPattern)
        {
            isPattern = true;
            StartCoroutine(StartBabyLionPattern());
        }
    }

    public Transform[] spawnPoints_babyLion;
    public GameObject[] babyLions;
    public int babyLionCount = 10;
    float baby_spawnTime;
    
    IEnumerator StartBabyLionPattern()
    {
        for (int i = 0; i < babyLionCount; i++)
        {
            baby_spawnTime = Random.Range(2, 3);
            SpawnBabyLion();
            yield return new WaitForSeconds(baby_spawnTime);
        }
        //yield return new WaitForSeconds(waitingTimeAtBreakTime);
        //isPattern = false;
    }

    void SpawnBabyLion()
    {
        int spIdx = Random.Range(0, spawnPoints_babyLion.Length);
        GameObject babyLionClone;
        if (spIdx == 0)
        {
            babyLionClone = Instantiate(babyLions[0]);
            babyLionClone.GetComponent<BabyLion>().SetFlag(0);
        }
        else
        {
            babyLionClone = Instantiate(babyLions[1]);
            babyLionClone.GetComponent<BabyLion>().SetFlag(1);
        }
        
        babyLionClone.transform.position = spawnPoints_babyLion[spIdx].position;
    }
}
