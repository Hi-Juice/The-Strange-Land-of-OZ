using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;

    float Speed = 2f;

    public string move_direct = "";

    public GameObject straw_Make_Object;
    public GameObject straw_Bullet;
    public GameObject straw_Spawn_Point;
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
            move_direct = "Left";
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move_direct = "Right";
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        else
        {
            move_direct = "";
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Make_Straw();
        }
    }


    public void Make_Straw()
    {
        StartCoroutine("Shoot_Straw");
    }
    private IEnumerator Move()
    {
        while(true)
        {
            ani.SetBool("isJump", true);
            if (move_direct == "Left")
            {
                //rb.AddForce(Vector2.left * Speed, ForceMode2D.Force);
            }
            else if (move_direct == "Right")
            {
                //rb.AddForce(Vector2.right * Speed, ForceMode2D.Force);
            }
            else
            {
            
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Shoot_Straw()
    {
        int stack = 0;
        while (stack < 20)
        {
            Instantiate(straw_Bullet, straw_Make_Object.transform);
            stack++;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("Throw_Straw");    
    }

    private IEnumerator Throw_Straw()
    {
        int stack = 0;
        while (stack < 20)
        {
            Transform spawnPoint = straw_Spawn_Point.transform;

            float x = Random.Range(-4f, 4f);

            Instantiate(straw_Bullet, new Vector3(x, spawnPoint.position.y, 0), Quaternion.identity);
            stack++;
            yield return new WaitForSeconds(0.4f);


            
        }
    }
}
