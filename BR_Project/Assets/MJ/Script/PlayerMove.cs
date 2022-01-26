using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Rigidbody2D rigid; //�����̵��� ���� ���� ���� 
    SpriteRenderer sRend;
    Animator playerAnim;

    public float jumpHeight = 15f;
    public float moveSpeed = 0.3f;
    private bool isJumping = false;
    float moveX;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); //���� �ʱ�ȭ 
        sRend = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetTrigger("idle");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + moveX, transform.position.y);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //sRend.flipX = false;
            playerAnim.SetBool("isRun", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //sRend.flipX = true;
            playerAnim.SetBool("isRun", true);
        }
        else
        {
            playerAnim.SetBool("isRun", false);
        }

        // JUMP 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!isJumping)
            {
                //Debug.Log("Anim: isJump = true");
                playerAnim.SetBool("isJump", true);
                rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                isJumping = true;
                SoundManager.Instance.Play_PlayerJumpSound();
            }
            //rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
        }

        //// ��ư���� ���� ���� ���� �ܹ����� Ű���� �Է��� FixedUpdate���� Update�� ���°� Ű���� �Է��� ������ Ȯ���� ������
        ////Stop speed 
        //if (Input.GetButtonUp("Horizontal"))
        //{   // ��ư���� ���� ���� ��� 
        //    // normalized : ���� ũ�⸦ 1�� ���� ���� (�������� : ũ�Ⱑ 1�� ����)
        //    // ���ʹ� ����� ũ�⸦ ���ÿ� �����µ� ũ��(- : �� , + : ��)�� �����ϱ� ���Ͽ� ��������(1,-1)�� ������ �˼� �ֵ��� �������͸� ���� 
        //    rigid.velocity = new Vector2(moveSpeed * rigid.velocity.normalized.x, rigid.velocity.y);
        //}
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "ground")
        {
            isJumping = false;
            //Debug.Log("Anim: isJump = false");
            playerAnim.SetBool("isJump", false);
        }
        if(coll.gameObject.tag == "Obstacle")
        {
            playerAnim.SetTrigger("hurt");
            SoundManager.Instance.Play_PlayerHitSound();
            Destroy(coll.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            playerAnim.SetTrigger("hurt");
            //SoundManager.Instance.Play_PlayerHitSound();
        }
    }
}
