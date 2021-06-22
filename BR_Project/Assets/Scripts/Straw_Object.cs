using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straw_Object : MonoBehaviour
{
    Rigidbody2D rb;
    float throwPower_up = 50f;
    float throwPower_side = 50f;


    void Start()
    {
        
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.parent = null;
        throwPower_up = Random.Range(1200f, 1500f);
        throwPower_side = Random.Range(400f, 500f);
        
        

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
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
