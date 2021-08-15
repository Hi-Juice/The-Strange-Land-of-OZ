using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMob_Tree : MonoBehaviour
{
    // TreeMob은 왼쪽 혹은 오른쪽에서 spawn되며
    // 플레이어에게 부딪힐 경우 플레이어의 HP를 줄어들게 한다
    // 따라서 플레이어는 TreeMob이 자신한테 오기 전에 처치해야 한다
    // - 계속해서 공격해야 한다
    // - Tree_Mob이 전장에 있을 경우 bullet의 타겟은 TreeMob이어야 한다
    // Start is called before the first frame update

    enum TreeState
    {
        Move,
        Die
    }
    public int defaultHp = 1;
    public float moveSpeed = 2.4f;

    int hp;

    Transform player;
    TreeState tState;
    float posY;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posY = transform.position.y;
        player = Transform.FindObjectOfType<PlayerMove>().transform;
        hp = defaultHp;
        tState = TreeState.Move;
    }

    void Update()
    {
        switch(tState)
        {
            case TreeState.Move:
                Move();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            if(hp > 0)
            {
                hp--;
            }
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if((player.transform.position.x - this.transform.position.x) > 0 )// 양수이면 플레이어가 몹의 오른쪽에 있는 것 
        {
            if(distance > 1)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(distance > 1)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        //transform.position = new Vector3(transform.position.x, posY, transform.position.z);

        if(Random.Range(0, 10) > 5)
        {
            Jump();
        }
    }

    void Jump()
    {

        rb.AddForce(Vector2.up);
    }
}
