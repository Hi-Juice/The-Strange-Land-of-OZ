using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lion : MonoBehaviour
{
    public enum LState
    {
        Start, // ����
        P_Roaring,
        P_CrownBomb,
        P_TreeMob,
        Dead
    }

    LState lState;

    Animator bossAnim;
    public GameObject sub_obstacle_triangle;
    public Transform spawnPoint_triangle;
    public Transform[] spawnPoints_treeMob;

    public GameObject obstacle_crown;
    public Animator crownAnim;
    public Transform[] parent_crownSpawnPoints;  // 2���� �θ�
    public float timeBetweenWarningAndRain = 2.0f;
    public int crownCount = 3;
    public int tri_spawnTimer = 1;
    public int repeatNum = 5;

    Transform parentCSP;
    Transform[] child_crownSpawnPoints; // 4���� �ڽ� 
    
    bool isSpawn = false;
    float timer;
    int waitingTime = 2;
    int spawnPointIdx;
    int check = 0;
    bool isTriSpawn = false;
    int exeNum;
    public bool isTreeMobP = false;

    public int boss_HP;
    public bool isDie = false;

    public GameObject[] Die_Effect_Set;

    void Start()
    {
        bossAnim = GetComponent<Animator>();
        lState = LState.Start;
    }

    void Update()
    {
        switch (lState)
        {
            case LState.Start:
                lStart();
                break;
            case LState.P_Roaring:
                P_Roaring();
                break;
            case LState.P_CrownBomb:
                P_CrownBomb();
                break;
            case LState.P_TreeMob:
                P_TreeMob();
                break;
            case LState.Dead:
                Boss_Die();
                break;
        }

    }

    public bool isCrownRain = false;
    void lStart()
    {
        if(isCrownRain)
        {
            lState = LState.P_CrownBomb;
        }
        if (isTreeMobP)
        {
            lState = LState.P_TreeMob;
        }
    }


    void P_Roaring()
    {
        // Roaring : ħ����� ������� ����
        // 1. Roaring �ϴ� ������ ����� ��Ÿ����
        // - ���¢�� �Ҹ��� ���鼭 ȭ���� ��鸰��. 
        // 2. �׸��� ������� ������ ��������
        // 3. �÷��̾�� ������� ���ؾ� �Ѵ�

    }

    void P_CrownBomb()    
    {
        if(!isTriSpawn)
            StartCoroutine(SpawnTriangle());// ���� ��ƾ ����
        
        child_crownSpawnPoints = new Transform[4];

        // CrownRain : ���ڰ� ���� �ִ� Ŀ�ٶ� �հ��� �÷��̾����� ���ƿ��� ����
        // 1. ���ڰ� ���� �ִ� �հ��� ���������� ��½�δ�
        crownAnim.SetTrigger("trig_Warning");
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            check++;
            if(check % 2 == 0)
            {
                spawnPointIdx = 0;
            }
            else
            {
                spawnPointIdx = 1;
            }

            // 2. �հ��� ������ ��ġ�� ���� ������ Ÿ���� �ȴ� 
            // 3. �׸��� �� ���� ��������
            for (int i = 0; i < crownCount; i++)
            {
                StartCoroutine(StartCrownPattern(spawnPointIdx));
            }

            if(exeNum < repeatNum) // repeatNum��ŭ�� �ݺ�
            {
                timer = 0; 
                exeNum++;
            }
            else
            {
                // ���� �������� �ѱ� 
                lState = LState.P_TreeMob;
            }
            
        }
    }

    IEnumerator StartCrownPattern(int idx)
    {     
        parentCSP = parent_crownSpawnPoints[idx];
        parentCSP.gameObject.SetActive(true);
        for (int i = 0; i < parentCSP.childCount; i++)
        {
            child_crownSpawnPoints[i] = parentCSP.GetChild(i);
        }
        SpawnCrown();
        yield return null;
    }

    void SpawnCrown()
    {
        bossAnim.SetTrigger("Trig_CrownSpawn");
        for (int i = 0; i < child_crownSpawnPoints.Length; i++)
        {
            GameObject obs = Instantiate(obstacle_crown);
            obs.transform.position = child_crownSpawnPoints[i].position;
        }
        isSpawn = true;
        Invoke("CSPDisable", 1.0f);
    }

    void CSPDisable()
    {
        parentCSP.gameObject.SetActive(false);
    }

    public GameObject TreeMob;
    public int tree_spawnTime = 3;
    bool isTreeSpawn = false;

    void P_TreeMob()
    {
        if(!isTreeSpawn)
        {
            StartCoroutine(SpawnTreeMob());
            isTreeSpawn = true;
        }
    }

    IEnumerator SpawnTreeMob()
    {
        int spIdx = Random.Range(0, spawnPoints_treeMob.Length);
        GameObject tmob = Instantiate(TreeMob);
        tmob.transform.position = spawnPoints_treeMob[spIdx].position;
        yield return new WaitForSeconds(tree_spawnTime);
        isTreeSpawn = false;
    }

    private void Boss_Die()
    {
        
        if(isDie == false)
        {
            isDie = true;
            StopAllCoroutines();
            StartCoroutine(Boss_Die_Effect());
        }
        
    }
    private IEnumerator Boss_Die_Effect()
    {
        int check = 0;
        while (check < 40)
        {
            GameObject effect = Instantiate(Die_Effect_Set[Random.Range(0, 3)], transform);

            effect.transform.position = new Vector3(transform.position.x + Random.Range(-2, 3), transform.position.y + Random.Range(-2, 4), transform.position.z);

            effect = Instantiate(Die_Effect_Set[Random.Range(0, 3)], transform);

            effect.transform.position = new Vector3(transform.position.x + Random.Range(-2, 3), transform.position.y + Random.Range(-2, 5), transform.position.z);
            
            check++;

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void GetHit()
    {
        boss_HP--;

        if (boss_HP <= 0)
        {
            if (isDie == false)
            {
                lState = LState.Dead;
                Debug.Log("����!");
            }
        }
    }

        // �÷��̾ �� ���ž��� ����� ��������
        IEnumerator SpawnTriangle()
    {
        isTriSpawn = true;
        GameObject tri = Instantiate(sub_obstacle_triangle);
        tri.transform.position = spawnPoint_triangle.position;
        yield return new WaitForSeconds(tri_spawnTimer);
        isTriSpawn = false;
    }
   

    

    
}

