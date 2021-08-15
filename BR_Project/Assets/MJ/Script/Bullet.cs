using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float distance;
    public LayerMask isLayer;
    Transform target;

    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 3f, rotSpeed = 2f; 
    Quaternion rotTarget; 
    Vector3 dir; 
    Rigidbody2D rb;

    bool mode_guided = true;
    private Vector3 prevPosition;

    public int bounceForce = 1000;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.position * speed;
        Invoke("DestroyBullet", 2);
        target = GameObject.FindGameObjectWithTag("Boss").transform;
        rb.AddForce(Vector2.up * bounceForce); // ó���� ��� Ƣ����� 

    }

    public int bulletDir;

    float time;

    public float targetingTime = 0.5f; // �Ѿ��� �� �Ŀ� Ÿ������ �ϴ��� 
    void Update()
    {
        time += Time.deltaTime;
        if(time > targetingTime)
        {
            if (Transform.FindObjectOfType<SubMob_Tree>() != null)
            {
                target = Transform.FindObjectOfType<SubMob_Tree>().transform;
            }
            else
            {
                target = GameObject.Find("AttackPoint").transform;
            }
            GuidedMissile();
        }


        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "Boss")
            {
                Debug.Log("����!");
            }
            DestroyBullet();
        }
    }

    public int cosAngle = 45;

    void GuidedMissile()
    {
        dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward); // Vector3.forward => target.position 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
        //transform.Rotate(new Vector3(Mathf.Cos(cosAngle * Mathf.PI / 180.0f), 0, 0));
        //rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Boss" || collision.transform.tag == "Obstacle")
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //public void SetBulletDirection(int dir)
    //{
    //    SetNormalMode();
    //    if (dir == 0)
    //    {
    //        transform.Translate(transform.right * -1 * speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        transform.Translate(transform.right * speed * Time.deltaTime);
    //    }
    //}

}
