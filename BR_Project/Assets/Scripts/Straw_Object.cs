using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straw_Object : MonoBehaviour
{
    Rigidbody2D rb;
    float throwPower_up = 50f;
    float throwPower_side = 50f;

    public bool isAttack = false;

    public bool end = false;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.parent = null;
        throwPower_up = Random.Range(800f, 1000f);
        throwPower_side = Random.Range(600f, 800f);
        

        
    }
    // Update is called once per frame
    void Update()
    {
        if(end == false)
        {
            if (isAttack == false)
            {
                if (Random.Range(0, 2) == 1)
                {
                    rb.AddForce(Vector2.up * throwPower_up, ForceMode2D.Force);
                    rb.AddForce(Vector2.left * throwPower_side, ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(Vector2.up * throwPower_up);
                    rb.AddForce(Vector2.right * throwPower_side);
                }
                rb.AddTorque(500);
            }

            else
            {
                rb.AddTorque(500);
            }
            end = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if(isAttack == false)
            {
                Destroy(this.gameObject);
            }
            
        }
    }
}
