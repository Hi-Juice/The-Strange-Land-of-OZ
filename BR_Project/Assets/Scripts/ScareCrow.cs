using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Animator ani;


    float speed = 2f;

    public int boss_HP = 50;


    public string move_Direct = "";

    public GameObject straw_Make_Object;
    public GameObject straw_Bullet;
    public GameObject straw_Spawn_Point;


    public GameObject crow_Make_Object;
    public GameObject crow_Bullet;
    public GameObject crow_Spawn_Point;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInParent<Animator>();
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            move_Direct = "Left";
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move_Direct = "Right";
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            move_Direct = "";
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Make_Straw();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Make_Crow();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Make_Fork();
        }
    }


    public void Make_Straw()
    {
        StartCoroutine("Start_Straw");
    }

    public void Make_Crow()
    {
        StartCoroutine("Start_Straw");
    }

    public void Make_Fork()
    {
        StartCoroutine("Start_Fork");
    }


    private IEnumerator Move()
    {
        while(true)
        {
            ani.SetBool("isJump", true);
            if (move_Direct == "Left")
            {
                //rb.AddForce(Vector2.left * Speed, ForceMode2D.Force);
            }
            else if (move_Direct == "Right")
            {
                //rb.AddForce(Vector2.right * Speed, ForceMode2D.Force);
            }
            else
            {
            
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Start_Straw()
    {
        int stack = 0;
        while (stack < 20)
        {
            Instantiate(straw_Bullet, straw_Make_Object.transform);
            
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
            straw.GetComponent<Straw_Object>().isAttack = true;
            straw.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            stack++;
            yield return new WaitForSeconds(0.4f);
        }
    }

    
    private IEnumerator Start_Crow()
    {
        int stack = 0;
        while (stack < 20)
        {
            Instantiate(straw_Bullet, straw_Make_Object.transform);

            stack++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator Attack_Crow()
    {
        int stack = 0;
        while (stack < 10)
        {
            Transform spawnPoint = crow_Spawn_Point.transform;

            int x = Random.Range(0,2);
            
            if(x == 0)
            {
                GameObject crow = Instantiate(straw_Bullet, spawnPoint);
                crow.transform.position = new Vector3(x, crow.transform.position.y, crow.transform.position.z);
                crow.GetComponent<Straw_Object>().isAttack = true;
                crow.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                stack++;
            }
            else
            {
                GameObject crow = Instantiate(straw_Bullet, spawnPoint);
                crow.transform.position = new Vector3(x, crow.transform.position.y, crow.transform.position.z);
                crow.GetComponent<Straw_Object>().isAttack = true;
                crow.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                stack++;
            }
            
            yield return new WaitForSeconds(0.4f);
        }
    }
}
