using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_Object : MonoBehaviour
{
    public float speed = 6f;
    private float runningTime = 0f;
    private float yPos = 0f;
    [SerializeField] [Range(0f, 10f)] private float length = 3f;
    public string dir;

    public bool isAttack = false;

    public SpriteRenderer spriteRender;
    public CircleCollider2D Crow_Collider;
    void Start()
    {
        Destroy(this.gameObject, 10f);
        spriteRender = GetComponent<SpriteRenderer>();
        Crow_Collider = GetComponent<CircleCollider2D>();
        if (dir == "Left")
        {
            spriteRender.flipX = false;
            spriteRender.flipY = false;
        }
        else
        {
            spriteRender.flipX = true;
            spriteRender.flipY = false;
        }


    }

    private void OnEnable()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        Crow_Collider = GetComponent<CircleCollider2D>();
        if (dir == "Left")
        {
            spriteRender.flipX = false;
            spriteRender.flipY = false;
        }
        else
        {
            spriteRender.flipX = true;
            spriteRender.flipY = false;
        }


    }

    void Update()
    {

        /*
        runningTime += Time.deltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        Debug.Log(yPos);
        //this.transform.position = new Vector2(0, yPos);
        this.transform.position = new Vector2(transform.position.x, yPos);
        */

        
        if (dir == "Left")
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Crow_Collider.enabled = false;
        }
    }
}
