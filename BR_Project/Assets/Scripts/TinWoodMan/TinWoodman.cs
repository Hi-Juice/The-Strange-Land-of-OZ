using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinWoodman : MonoBehaviour
{
    private enum BossState
    {
        Move,
        Dead,
        SwingAxe,
        OilFlooding,
        Apple,
        HeartAttack
    }

    private enum TreeNum
    {
        First,
        Second,
        Third,
        Four
    }

    Rigidbody2D rb;
    public Animator Arm_Animator;
    Animator ani;

    float speed = 2f;
    public int boss_HP = 50;
    public float boss_MoveSpeed = 2f;


    //public GameObject straw_Make_Object;
    public GameObject Heart_Bullet;
    public GameObject HeartAttack_Spawn_Point;
    public GameObject Tree_Object;

    public GameObject Player;
    public GameObject Oil_Object;

    public bool isPatten = false;
    public bool isStart;
    public bool isDie = false;
    public bool isMove = false;

    public string dir = "";

    public GameObject Apple_Bullet;
    public GameObject Apple_Spawn_Point;

    public GameObject[] Die_Effect_Set;
    public GameObject[] Move_Transform_Obejct;
    public SpriteRenderer spriteRenderer;

    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject LeftAxeAttackPoint;
    public GameObject RightAxeAttackPoint;

    public GameObject LeftOil;
    public GameObject RightOil;

    public Sprite Arm_Axe;
    public Sprite Arm_Hand;

    public GameObject Axe;
    public GameObject Middle_Floor;


    BossState bossState = BossState.Move;
    TreeNum bossTreeState = TreeNum.First;
    public GameObject Parent;

    public Vector3 direction;
    float angle;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInParent<Animator>();
        ani.SetBool("isJump", true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossTreeState = TreeNum.First;
        bossState = BossState.Move;
        isPatten = true;
        StartCoroutine(Move());
        

    }

    // Update is called once per frame
    void Update()
    {
        direction = (Player.transform.position - HeartAttack_Spawn_Point.transform.position);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        
        //transform.rotation = angleAxis;

        if (isDie == false)
        {
            if (isPatten == false)
            {
                Update_State();
                //bossState = BossState.Move; // 테스트용 줄 삭제 필수


                if (Input.GetKey(KeyCode.Q))
                {
                    bossState = BossState.Move;
                }
                
                else if(Input.GetKey(KeyCode.W))
                {
                    bossState = BossState.HeartAttack;
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    bossState = BossState.OilFlooding;
                }
                else if (Input.GetKey(KeyCode.R))
                {
                    bossState = BossState.Apple;
                }
                else if(Input.GetKey(KeyCode.T))
                {
                    bossState = BossState.SwingAxe;
                }


                isPatten = true;
                if (bossState == BossState.Move)
                {
                    
                    StartCoroutine(Move());
                }
                else if (bossState == BossState.HeartAttack)
                {
                    StartCoroutine("Start_HeartAttack");
                    
                }
                else if (bossState == BossState.OilFlooding)
                {
                    StartCoroutine("Start_OilFlooding");
                }
                else if (bossState == BossState.Apple)
                {
                    StartCoroutine("Start_Apple");
                }
                
            }
        }

        if(isMove && dir == "Left")
        {
            Parent.transform.Translate(Vector2.left * boss_MoveSpeed * Time.deltaTime);
        }
        else if(isMove && dir == "Right")
        {
            Parent.transform.Translate(Vector2.right * boss_MoveSpeed * Time.deltaTime);
        }

    }

    private void Update_State()
    {
        int patten = Random.Range(0, 5);
        if (patten == 0)
        {
            bossState = BossState.HeartAttack;
        }
        else if (patten == 1)
        {
            bossState = BossState.OilFlooding;
        }
        else if (patten == 2)
        {
            bossState = BossState.Apple;
        }
        else
        {
            bossState = BossState.Move;
        }
    }


    private IEnumerator Move() //위치 변경
    {
        Debug.Log("$#");
       
        int Random_num = 0;
        isMove = true;
        if (bossTreeState == TreeNum.First)
        {
            Debug.Log("첫번째 지점에서 다른 지점으로 이동");
            dir = "Left";
            yield return new WaitForSeconds(4f);

            /*
            while (Random_num == 0)
            {
                Random_num = Random.Range(0, 4);
            }
            */
            Random_num = 3;
        }
        /*
        else if(bossTreeState == TreeNum.Second)
        {
            Debug.Log("두번째 지점에서 다른 지점으로 이동");
            dir = "Right";
            yield return new WaitForSeconds(1f);

            while (Random_num == 1)
            {
                Random_num = Random.Range(0, 4);
            }
        }
        else if (bossTreeState == TreeNum.Third)
        {
            Debug.Log("세번째 지점에서 다른 지점으로 이동");
            dir = "Left";
            yield return new WaitForSeconds(1f);

            while (Random_num == 2)
            {
                Random_num = Random.Range(0, 4);
            }
        }
        */
        else if (bossTreeState == TreeNum.Four)
        {
            Debug.Log("네번째 지점에서 다른 지점으로 이동");
            dir = "Right";
            yield return new WaitForSeconds(4f);

            /*
            while (Random_num == 3)
            {
                Random_num = Random.Range(0, 4);
            }
            */
            Random_num = 0;
        }
        yield return new WaitForSeconds(2f);
        bossTreeState = return_Tree(Random_num);
        Parent.transform.position = Move_Transform_Obejct[Random_num].transform.position;
        Debug.Log("이동!");
        if (Random_num == 0 || Random_num == 2)
        {
            Debug.Log("첫번째 또는 세번째 지점에서 등장");
            dir = "Right";
            leftArm.SetActive(false);
            rightArm.SetActive(true);
            transform.rotation = Quaternion.Euler(0, 0, -30);
            spriteRenderer.flipX = true;
        }
        else if((Random_num == 1 || Random_num == 3))
        {
            Debug.Log("두번째 또는 네번째 지점에서 등장");
            dir = "Left";
            leftArm.SetActive(true);
            rightArm.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 30);
            spriteRenderer.flipX = false;
        }
        yield return new WaitForSeconds(3.5f);
        isMove = false;
        yield return new WaitForSeconds(1f);
        isPatten = false;

    }


    private TreeNum return_Tree(int value)
    {

        if(value == 0)
        {
            return TreeNum.First;
        }
        else if (value == 1)
        {
            return TreeNum.Second;
        }
        else if (value == 2)
        {
            return TreeNum.Third;
        }
        else if (value == 3)
        {
            return TreeNum.Four;
        }
        else
        {
            return TreeNum.First;
        }

    }


    private IEnumerator Start_HeartAttack()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }
        //하트 문 열리는 애니메이션
        HeartAttack_Spawn_Point.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack_HeartAttack());
    }

    private IEnumerator Attack_HeartAttack()
    {
        int stack = 0;
        while (stack < 20)
        {
            Transform spawnPoint = HeartAttack_Spawn_Point.transform;


            Quaternion angleAxis = Quaternion.Euler(0, 0, angle - 90);
            GameObject HeartAttack = Instantiate(Heart_Bullet, spawnPoint.transform.position, angleAxis);


            HeartAttack.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            /*
            if(dir == "Right")
            {
                HeartAttack.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-60, -241));
            }
            else
            {
                HeartAttack.transform.rotation = Quaternion.Euler(0, 0, Random.Range(60, 200));
            }
            */



            stack++;

            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(2f);
        HeartAttack_Spawn_Point.SetActive(false);
        isPatten = false;
    }


    private IEnumerator Start_OilFlooding()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }

        
        if(dir == "Left")
        {
            LeftOil.SetActive(true);
            leftArm.GetComponent<SpriteRenderer>().sprite = Arm_Hand;
        }
        else
        {
            RightOil.SetActive(true);
            rightArm.GetComponent<SpriteRenderer>().sprite = Arm_Hand;
        }
        
        yield return new WaitForSeconds(3f); // 3초뒤 실행
        Axe.SetActive(true);
        StartCoroutine(Attack_OilFlooding());
    }


    private IEnumerator Attack_OilFlooding()
    {

        //오일 올라오기
        Oil_Object.GetComponent<OilFlooding>().isPatten = true;
        Middle_Floor.SetActive(true);
        
        yield return new WaitForSeconds(15f);
        Oil_Object.GetComponent<OilFlooding>().isPatten = false;
        Middle_Floor.SetActive(false);
        LeftOil.SetActive(false);
        RightOil.SetActive(false);
        rightArm.GetComponent<SpriteRenderer>().sprite = Arm_Axe;
        leftArm.GetComponent<SpriteRenderer>().sprite = Arm_Axe;
        Axe.SetActive(false);
        isPatten = false;
    }



    private IEnumerator Start_Apple()
    {
        if (isStart == false)
        {
            yield return new WaitForSeconds(2f);
        }

        // 나무 때리는 애니메이션
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack_Apple());
    }


    private IEnumerator Attack_Apple()
    {
        if (dir == "Left")
        {
            LeftAxeAttackPoint.SetActive(true);
            leftArm.GetComponent<Animator>().SetBool("isAttack", true);
        }
        else if (dir == "Right")
        {
            RightAxeAttackPoint.SetActive(true);
            rightArm.GetComponent<Animator>().SetBool("isAttack", true);
        }

        int stack = 0;
        while (stack < 30)
        {
            Transform spawnPoint = Apple_Spawn_Point.transform;

            float x = Random.Range(-9f, 9f);

            GameObject apple = Instantiate(Apple_Bullet, spawnPoint);
            apple.transform.position = new Vector3(x, apple.transform.position.y, apple.transform.position.z);
            apple.GetComponent<AppleObject>().isAttack = true;
            apple.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            stack++;
            yield return new WaitForSeconds(0.3f);
        }
        LeftAxeAttackPoint.SetActive(false);
        RightAxeAttackPoint.SetActive(false);
        leftArm.GetComponent<Animator>().SetBool("isAttack", false);
        rightArm.GetComponent<Animator>().SetBool("isAttack", false);
        yield return new WaitForSeconds(2f);
       
        isPatten = false;
    }








    private void Boss_Die()
    {
        isDie = true;
        StopAllCoroutines();
        StartCoroutine(Boss_Die_Effect());
    }
    private IEnumerator Boss_Die_Effect()
    {
        int check = 0;
        while (check < 20)
        {
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

        if (boss_HP <= 0)
        {
            if (isDie == false)
            {
                Boss_Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack") == true)
        {
            //Destroy(collision.gameObject);
            
        }
    }
}
