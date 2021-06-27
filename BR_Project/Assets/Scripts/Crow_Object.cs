using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_Object : MonoBehaviour
{
    public float speed = 4f;
    private float runningTime = 0f;
    private float yPos = 0f;
    [SerializeField] [Range(0f, 10f)] private float length = 3f;
    public string dir;


    public SpriteRenderer spriteRender;
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        if (dir == "Left")
        {
            spriteRender.flipX = false;
        }
        else
        {
            spriteRender.flipX = true;
        }


    }

    private void OnEnable()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        if (dir == "Left")
        {
            spriteRender.flipX = false;
        }
        else
        {
            spriteRender.flipX = true;
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
}
