using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Lion : MonoBehaviour
{
    public enum LionState
    {
        Start, // ����
        P_CatPunch,
        P_CrownBomb,
        P_TreeMob,
        Dead
    }

    LionState lState;
    CameraEffect mainCam;
    Animator bossAnim;
    public GameObject sub_obstacle_triangle;
    public Transform spawnPoint_triangle;
    public Transform[] spawnPoints_treeMob;

    public GameObject obstacle_crown;
    public Animator crownAnim;
    public Transform[] parent_crownSpawnPoints;  // 2���� �θ�
    public float timeBetweenWarningAndRain = 2.0f;
    public int crownPatternRepeatNum = 3;
    public int tri_spawnTimer = 1;
    public int repeatNum = 5;

    public int boss_MaxHp = 1000;
    public int boss_HP;
    public Slider BossHP_Slider;
    Transform parentCSP;
    Transform[] child_crownSpawnPoints; // 4���� �ڽ� 
    
    bool isPlayPattern_crownBomb = false;
    float timer;
    int waitingTime = 2;
    int spawnPointIdx;
    int check = 0;
    bool isTriSpawn = false;
    int exeNum;
    public bool isTreeMobP = false;
    public bool isCrownRain = false;
    public bool isCatPunch = false;

    public bool isDie = false;
    bool isPattern = false;
    public float cpbetTime = 4f;
    public GameObject BG_lianaSet;

    // �ٳ��� § �κ�
    public bool isStart = false;
    public bool isEndDialog = false;
    public GameObject[] Die_Effect_Set;

    public GameObject tears;
    int pattern;
    void Start()
    {
        boss_HP = boss_MaxHp;
        bossAnim = GetComponent<Animator>();
        lState = LionState.Start;

        for (int i = 0; i < catPunches.Length; i++)
        {
            catPunches[i].SetActive(false);   
        }

        pattern = 0;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffect>();
        tears.SetActive(false);
    }

    // �ٳ��� § �κ�
    public void StartGame()
    {
        if (isEndDialog == false)
        {
            isEndDialog = true;
            
            StartCoroutine(StartWait());
        }
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(3.5f);
        isStart = true;
    }

    void Update()
    {
        if(!isPattern)
        {
            Update_State();
        }
        // �ٳ��� § �κ�(isStart�� ������)
        if (isStart)
        {
            switch (lState)
            {
                case LionState.Start:
                    lStart();
                    isPattern = true;
                    break;
                case LionState.P_CatPunch:
                    P_CatPunch();
                    isPattern = true;
                    break;
                case LionState.P_CrownBomb:
                    P_CrownBomb();
                    isPattern = true;
                    break;
                case LionState.P_TreeMob:
                    isPattern = true;
                    P_TreeMob();
                    break;
                case LionState.Dead:
                    isPattern = true;
                    Boss_Die();
                    break;
            }
        }
        

    }

    private void Update_State()
    {
        if (pattern == 0)
        {
            lState = LionState.P_CatPunch;
            pattern++;
        }
        else if (pattern == 1)
        {
            lState = LionState.P_CrownBomb;
            pattern++;
        }
        else if (pattern == 2)
        {
            lState = LionState.P_TreeMob;
            pattern = 0;
        }
    }

    // �׽�Ʈ��
    void lStart()
    {
        if(isCrownRain)
        {
            lState = LionState.P_CrownBomb;
        }
        if (isTreeMobP)
        {
            lState = LionState.P_TreeMob;
        }
        if(isCatPunch)
        {
            lState = LionState.P_CatPunch;
        }
    }


    public GameObject[] catPunches;
    public int cp_repeatNum = 10;

    bool isPlayPattern_CatPunch = false;
    bool isSpawnBabyLion = false;
    float time_cp;
    int delayTime_cp = 5;
    bool isCompleted_catPunch = false;

    void P_CatPunch()
    {
        if(!isCompleted_catPunch)
        {
            isCompleted_catPunch = true;
            BG_lianaSet.GetComponent<Animator>().SetTrigger("Trig_Spawn");
        }
        
        time_cp += Time.deltaTime;
        if(time_cp > delayTime_cp)
        {
            if (!isSpawnBabyLion)
            {
                isSpawnBabyLion = true;
                StartCoroutine(StartBabyLionPattern());
            }
            // CatPunch : �ɳ���ġ�� ������� ����
            // �� �ټ����� �������� Ƣ��� �� �ִ�
            // ���� ���� ���� ���ǥ�ô� ���� �ʴ´�
            // - ��� �ش� ���⿡�� ��¦ ���Դٰ� ��� ���� �������� �Ѵ�
            // - ���� ���� ��¦ ������ ������ �� ���⿡�� ���� �Ŷ� �� �÷��̾ �� �� �ִ�

            if (!isPlayPattern_CatPunch)
            {
                isPlayPattern_CatPunch = true;
                StartCoroutine(PlayCatPunch());
            }
        }
    }

    

    IEnumerator PlayCatPunch()
    {
        int cpIdx_1;
        int cpIdx_2;
        for (int i = 0; i < cp_repeatNum; i++) // �ݺ� Ƚ����ŭ
        {
            cpIdx_1 = Random.Range(0, catPunches.Length); // ��ġ ���� ������ �������� ����
            cpIdx_2 = Random.Range(0, catPunches.Length); 
            while (cpIdx_2 == cpIdx_1 || (cpIdx_1 == 0 && cpIdx_2 == 4) || (cpIdx_2 == 0 && cpIdx_1 == 4))
            {
                cpIdx_2 = Random.Range(0, catPunches.Length); // ��ġ ���� ������ �������� ����
            }

            for (int j = 0; j < catPunches.Length; j++)
            {
                if (j == cpIdx_1 || j == cpIdx_2)
                {
                    catPunches[j].SetActive(true);
                    catPunches[j].GetComponentInChildren<Animator>().SetTrigger("IsAttack");
                    SoundManager.Instance.Play_LionMeowSound();

                }
                else
                {
                    catPunches[j].SetActive(false);
                }
            }
            yield return new WaitForSeconds(cpbetTime);
        }
        yield return new WaitForSeconds(waitingTimeAtBreakTime);
        isPattern = false;
        BG_lianaSet.GetComponent<Animator>().SetTrigger("Trig_End");

    }

    void P_CrownBomb()    
    {
        if(!isTriSpawn)
        {
            isTriSpawn = true;
            StartCoroutine(SpawnTriangle());// ���� ��ƾ ����
        }

        child_crownSpawnPoints = new Transform[4];

        // CrownRain : ���ڰ� ���� �ִ� Ŀ�ٶ� �հ��� �÷��̾����� ���ƿ��� ����
        // 1. ���ڰ� ���� �ִ� �հ��� ���������� ��½�δ�
        crownAnim.SetTrigger("trig_Warning");

        // waiting Time���� �����ư��鼭 �հ��� spawn �ȴ�
        if(!isPlayPattern_crownBomb)
        {
            isPlayPattern_crownBomb = true;
            StartCoroutine(StartCrownPattern());
        }  
    }

    IEnumerator StartCrownPattern()
    {
        for (int i = 0; i < crownPatternRepeatNum; i++)
        {
            check++;
            if (check % 2 == 0)
            {
                spawnPointIdx = 0;
            }
            else
            {
                spawnPointIdx = 1;
            }
            parentCSP = parent_crownSpawnPoints[spawnPointIdx];
            parentCSP.gameObject.SetActive(true);

            for (int j = 0; j < parentCSP.childCount; j++)
            {
                child_crownSpawnPoints[j] = parentCSP.GetChild(j);
            }

            SpawnCrown();
            yield return new WaitForSeconds(waitingTime);
        }
        yield return new WaitForSeconds(waitingTimeAtBreakTime);
        isPattern = false;
    }

    void SpawnCrown()
    {
        SoundManager.Instance.Play_LionCrownSound();
        bossAnim.SetTrigger("Trig_CrownSpawn");
        for (int i = 0; i < child_crownSpawnPoints.Length; i++)
        {
            GameObject obs = Instantiate(obstacle_crown);
            obs.transform.position = child_crownSpawnPoints[i].position;
        }
        
        Invoke("CSPDisable", 1f);
    }

    void CSPDisable()
    {   
        parentCSP.gameObject.SetActive(false);
    }

    public GameObject TreeMob;
    public float tree_spawnTime = 3;
    public int treeMobCount = 13;

    bool isPlayPattern_treeMob = false;

    bool isCompleted_RoseP = false;
    bool isCompleted_TreeP = false;
    void P_TreeMob()
    {
        if (!isStartRoseP) /* Sub Pattern */
        {
            isStartRoseP = true;
            StartCoroutine(StartRosePattern());
        }

        if (!isPlayPattern_treeMob)
        {
            isPlayPattern_treeMob = true;
            StartCoroutine(StartTreeMobPattern());
        }

        if(isCompleted_RoseP && isCompleted_TreeP)
        {
            isPattern = false;
        }
    }

    public float waitingTimeAtBreakTime = 5f;
    IEnumerator StartTreeMobPattern()
    {
        SoundManager.Instance.Play_LionTreeSound();
        for(int i=0; i<treeMobCount; i++)
        {
            tree_spawnTime = Random.Range(0.5f, 1.2f);
            SpawnTreeMob();
            yield return new WaitForSeconds(tree_spawnTime);
        }
        yield return new WaitForSeconds(waitingTimeAtBreakTime);
        isCompleted_TreeP = true;
    }

    void  SpawnTreeMob()
    {   
        int spIdx = Random.Range(0, spawnPoints_treeMob.Length);
        GameObject tmob = Instantiate(TreeMob);
        tmob.transform.position = spawnPoints_treeMob[spIdx].position;        
    }

    private void Boss_Die()
    {
        
        if(isDie == false)
        {
            tears.SetActive(true);
            isDie = true;
            StopAllCoroutines();
            StartCoroutine(Boss_Die_Effect());
        }
        
    }
    private IEnumerator Boss_Die_Effect()
    {
        int check = 0;
        DialogSystem.Instance.BossClear();
        PlayerPrefs.SetInt("LionClear", 1);
        while (check < 40)
        {
            SoundManager.Instance.Play_ScareCrowHitSound();
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
        bossAnim.SetTrigger("Trig_Hit");
        boss_HP--;
        SoundManager.Instance.Play_ScareCrowHitSound();
        BossHP_Slider.value = (float)(float)(boss_HP / (float)boss_MaxHp);
        if (boss_HP <= 0)
        {
            if (isDie == false)
            {
                lState = LionState.Dead;
                Debug.Log("����!");
            }
        }
    }

    public int triangleCount = 7;
    // �÷��̾ �� ���ž��� ����� ��������
    IEnumerator SpawnTriangle()
    {
        for(int i=0; i< triangleCount; i++)
        {
            GameObject tri = Instantiate(sub_obstacle_triangle);
            tri.transform.position = spawnPoint_triangle.position;
            yield return new WaitForSeconds(tri_spawnTimer);
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
        SoundManager.Instance.Play_SmallLionSound();
        babyLionClone.transform.position = spawnPoints_babyLion[spIdx].position;
    }

    // 4���� ��
    // ������ ���۵Ǹ�,
    // - ������ �������µ� ���� ��� �����´�
    // - n��(random)���� ���� �������� 
    // - n��(random) �Ŀ� �ٸ� �ְ� ��������
    // - �¿�� ���ݾ� �����δ�
    public GameObject[] roses;
    
    Transform roseTr;

    public float fallRoseSpeed = 3f;
    bool isStartRoseP = false;

    public int rosePCount = 10;
    public int rosePwaitTime = 2;
    int roseIdx;

    IEnumerator StartRosePattern()
    {
        roseIdx = Random.Range(0, 4);
        for (int i = 0; i < rosePCount; i++)
        {
            for (int j = 0; j < roses.Length; j++)
            {
                float waitTime = Random.Range(1, 3f);

                roseTr = roses[SetRoseIdx(roseIdx)].transform;
                // ��������

                StartCoroutine(DownAndUpRose(roseTr));

                yield return new WaitForSeconds(waitTime);

                roseIdx++;
            }
            yield return new WaitForSeconds(rosePwaitTime);
        }
        isCompleted_RoseP = true;
    }

    int SetRoseIdx(int tmpIdx)
    {
        if (tmpIdx > 3)
        {
            roseIdx = 0;
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

        while (rose.position.y >= 1 && !isGround)
        {
            rose.position -= new Vector3(0, fallRoseSpeed * Time.deltaTime, 0);
            if (tmpRoseRan % 2 == 0)
            {
                rose.position -= new Vector3(fallRoseSpeed * Time.deltaTime / 4, 0, 0);
            }
            else
            {
                rose.position += new Vector3(fallRoseSpeed * Time.deltaTime / 4, 0, 0);
            }

            if (rose.position.y <= 1)
            {
                isGround = true;
            }
            yield return null;
        }

        while (rose.position.y < 10 && isGround)
        {
            rose.position += new Vector3(0, fallRoseSpeed * Time.deltaTime, 0);
            if (tmpRoseRan % 2 == 0)
            {
                rose.position -= new Vector3(fallRoseSpeed * Time.deltaTime / 4, 0, 0);
            }
            else
            {
                rose.position += new Vector3(fallRoseSpeed * Time.deltaTime / 4, 0, 0);
            }
            yield return null;
        }

        rose.position = new Vector3(savedXval, rose.position.y, rose.position.z); // x�� ����
    }

    public void ShakeCamera()
    {
        mainCam.PlayCameraBounce();
    }

}

