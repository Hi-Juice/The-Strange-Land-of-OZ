using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Rigidbody2D rigid; //물리이동을 위한 변수 선언 
    SpriteRenderer sRend;
    Animator playerAnim;

    public float jumpHeight = 15f;
    public float moveSpeed = 0.3f;
    private bool isJumping = false;
    float moveX;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); //변수 초기화 
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

        //// 버튼에서 손을 떄는 등의 단발적인 키보드 입력은 FixedUpdate보다 Update에 쓰는게 키보드 입력이 누락될 확률이 낮아짐
        ////Stop speed 
        //if (Input.GetButtonUp("Horizontal"))
        //{   // 버튼에서 손을 때는 경우 
        //    // normalized : 벡터 크기를 1로 만든 상태 (단위벡터 : 크기가 1인 벡터)
        //    // 벡터는 방향과 크기를 동시에 가지는데 크기(- : 왼 , + : 오)를 구별하기 위하여 단위벡터(1,-1)로 방향을 알수 있도록 단위벡터를 곱함 
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
