using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScareCrow : MonoBehaviour
{
    private enum BossState
    {
        Move,
        Dead,
        Throw_Straw,
        Crow,
        Fork,
    }

    Rigidbody2D rb;
    public Animator Crow_Animator;
    public GameObject Parent;
    Animator ani;


    float speed = 2f;
    public int boss_MaxHp = 1000;
    public int boss_HP = 1000;
    


    public string move_Direct = "";

    public GameObject straw_Make_Object;
    public GameObject straw_Bullet;
    public GameObject straw_Spawn_Point;
    public GameObject Player;
    public bool isPattern = false;
    public bool isMove;
    public bool isStart;
    public bool isDie = false;
    public bool isFork;

    public GameObject crow_Make_Object;
    public GameObject crow_Bullet;
    public GameObject crow_Spawn_Point_L;
    public GameObject crow_Spawn_Point_R;
   
    public GameObject crow_CAW;


    public GameObject Fork;
    public GameObject Fork_Attack_Point;
    public GameObject Origin_Move_Point;
    public Fork Fork_Component;

    public GameObject[] Die_Effect_Set;

    public Slider BossHP_Slider;
    int Pattern;

    Transform initialTransform;
    Animator hitAnim;
    BossState bossState = BossState.Move;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = Parent.GetComponent<Animator>();
        ani.SetBool("isJump", true);
        
        straw_Spawn_Point = GameObject.Find("Straw_Spawn_Point");
        straw_Make_Object = GameObject.Find("Straw_Make_Point");

        crow_Make_Object = GameObject.Find("Crow_Make_Point");
        crow_Spawn_Point_L = GameObject.Find("Crow_Spawn_Point_L"); 
        crow_Spawn_Point_R = GameObject.Find("Crow_Spawn_Point_R");
        crow_CAW = GameObject.Find("CAW");
        crow_CAW.SetActive(false);
        Fork = GameObject.Find("Fork");
        Fork_Component = Fork.GetComponent<Fork>();
        Origin_Move_Point = GameObject.Find("Origin_Move_Point");
        BossHP_Slider = GameObject.Find("BossHpSlider").GetComponent<Slider>();
        boss_HP = boss_MaxHp;

        isPattern = true;
        
        Pattern = Random.Range(0, 3);
        hitAnim = GetComponent<Animator>();
        initialTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie == false)
        {
            if (isMove == true)
            {
                if (move_Direct == "Left")
                {
                    // rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                else if (move_Direct == "Right")
                {
                    // rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }

                if (transform.transform.position.x > 8 || transform.transform.position.x < -8)
                {
                    if (move_Direct == "Left")
                    {
                        move_Direct = "Right";
                    }
                    else
                    {
                        move_Direct = "Left";
                    }
                }
            }


            if (isPattern == false)
            {

                Update_State();
                if (bossState == BossState.Move)
                {
                    StartCoroutine("Move");
                    isPattern = true;
                }
                if (bossState == BossState.Crow)
                {
                    StartCoroutine("Make_Crow");
                    isPattern = true;
                }
                if (bossState == BossState.Throw_Straw)
                {
                    StartCoroutine("Make_Straw");
                    isPattern = true;
                }
                if (bossState == BossState.Fork)
                {
                    StartCoroutine("Make_Fork");
                    isPattern = true;
                }
            }
        }
        else
        {
            Debug.Log("죽음");
            //this.transform.position = new Vector3(0, 0.58f, 0);
            this.transform.position = new Vector3(0, transform.position.y, 0);
            Parent.transform.position = new Vector3(0, Parent.transform.position.y, 0);
        }
    }

    public void Make_Straw()
    {
        StartCoroutine("Start_Straw");
    }

    public void Make_Crow()
    {
        StartCoroutine("Start_Crow");
    }

    public void Make_Fork()
    {
        StartCoroutine("Start_Fork");
    }


    private void Update_State()
    {
        
        if (Pattern == 0)
        {
            bossState = BossState.Fork;
            Pattern++;
        }
        else if (Pattern == 1)
        {
            bossState = BossState.Throw_Straw;
            Pattern++;
        }
        else if (Pattern == 2)
        {
            bossState = BossState.Crow;
            Pattern = 0;
        }
        /*
        else
        {
            bossState = BossState.Move;
        }
        */

        if(Input.GetKey(KeyCode.Q))
        {
            bossState = BossState.Fork;
        }
        if (Input.GetKey(KeyCode.W))
        {
            bossState = BossState.Throw_Straw;
        }
        if (Input.GetKey(KeyCode.E))
        {
            bossState = BossState.Crow;
        }
        if (Input.GetKey(KeyCode.R))
        {
            bossState = BossState.Move;
        }
    }


    public void StartGame()
    {
        if(isStart == false)
        {
            isStart = true;
            StartCoroutine(StartWait());
        }
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(3.5f);
        isPattern = false;
    }

    private IEnumerator Move()
    {
        if(!isDie)
        {
            if (isStart == false)
            {
                yield return new WaitForSeconds(2f);
            }
            int Pattern_Time = 0;
            int Random_num = Random.Range(0, 2);
            if (Random_num == 0)
            {
                move_Direct = "Left";
            }
            else
            {
                move_Direct = "Right";
            }

            isMove = true;

            while (true)
            {
                yield return new WaitForSeconds(1f);
                Pattern_Time++;

                if (Pattern_Time > 6f)
                {
                    isPattern = false;
                    isMove = false;
                    StopCoroutine(Move());
                    break;
                }
            }
        }
        
    }

    private IEnumerator Start_Straw()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }
        int stack = 0;
        while (stack < 20)
        {
            GameObject straw = Instantiate(straw_Bullet, straw_Make_Object.transform);
            straw.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            stack++;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("Attack_Straw");    
    }

    private IEnumerator Attack_Straw()
    {
        int stack = 0;
        while (stack < 20)
        {
            Transform spawnPoint = straw_Spawn_Point.transform;

            float x = Random.Range(-9f, 9f);

            GameObject straw = Instantiate(straw_Bullet, spawnPoint);
            straw.transform.position = new Vector3(x, straw.transform.position.y, straw.transform.position.z);
            straw.GetComponent<StrawObject>().isAttack = true;
            straw.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            stack++;
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(2f);
        isPattern = false;
    }

    
    private IEnumerator Start_Crow()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }

        Crow_Animator.SetBool("isCAW", true);
        SoundManager.Instance.Play_ScareCrowPatternCrowSound();
        crow_CAW.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack_Crow());
    }

    
    private IEnumerator Attack_Crow()
    {
        int stack = 0;

        while (stack < 12)
        {
            Transform spawnPoint_L = crow_Spawn_Point_L.transform;
            Transform spawnPoint_R = crow_Spawn_Point_R.transform;

            int x = Random.Range(0,2);

            float y = Player.transform.position.y;

            if(x == 0)
            {
                Debug.Log("공격 생성");
                GameObject crow = Instantiate(crow_Bullet, spawnPoint_L);
                crow.transform.position = new Vector3(crow.transform.position.x, y, crow.transform.position.z);
                crow.GetComponent<Crow_Object>().isAttack = true;
                crow.GetComponent<Crow_Object>().dir = "Left";

                stack++;
            }
            else
            {
                Debug.Log("공격 생성");
                GameObject crow = Instantiate(crow_Bullet, spawnPoint_R);
                crow.transform.position = new Vector3(crow.transform.position.x, y, crow.transform.position.z);
                crow.GetComponent<Crow_Object>().isAttack = true;
                crow.GetComponent<Crow_Object>().dir = "Right";

                stack++;
            }
            
            yield return new WaitForSeconds(1.5f);
        }
        
        yield return new WaitForSeconds(2f);
        crow_CAW.SetActive(false);
        Crow_Animator.SetBool("isCAW", false);
        isPattern = false;
    }

    private IEnumerator Start_Fork()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }

        isFork = true;
        Fork_Component.Start_Attack();
        int Random_num = Random.Range(0, 2);
        if (Random_num == 0)
        {
            move_Direct = "Left";
        }
        else
        {
            move_Direct = "Right";
        }
        isMove = true;
    }


    private void Boss_Die()
    {
        isDie = true;
        isFork = false;
   
        Fork_Component.Stop_Attack();
        StopAllCoroutines();
        StartCoroutine(Boss_Die_Effect());
    }
    private IEnumerator Boss_Die_Effect()
    {
        int check = 0;
        DialogSystem.Instance.BossClear();
        PlayerPrefs.SetInt("ScareCrowClear", 1);
        while (check < 20)
        {
            SoundManager.Instance.Play_ScareCrowHitSound();
            GameObject effect = Instantiate(Die_Effect_Set[Random.Range(0, 3)], transform);
            
            effect.transform.position = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-2, 4), transform.position.z);
            
            effect = Instantiate(Die_Effect_Set[Random.Range(0, 3)], transform);
            
            effect.transform.position = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-2, 4), transform.position.z);
            
            check++;
            
            yield return new WaitForSeconds(0.5f);
        }
        
    }


    public void GetHit()
    {
        boss_HP--;
        BossHP_Slider.value = (float)(float)(boss_HP / (float)boss_MaxHp);
        hitAnim.SetTrigger("IsHit");
        SoundManager.Instance.Play_ScareCrowHitSound();
        if (boss_HP <= 0)
        {
            if (isDie == false)
            {
                Boss_Die();
            }
        }

    }



}


