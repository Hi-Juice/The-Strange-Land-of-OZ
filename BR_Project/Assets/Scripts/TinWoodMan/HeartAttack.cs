using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAttack : MonoBehaviour
{
    Rigidbody2D rb;
    float throwPower_up = 50f;
    float throwPower_side = 50f;

    public bool isAttack = false;

    void Start()
    {
        transform.parent = null;
        
    }
    private void OnEnable()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    { 
        transform.Translate(Vector2.up * 12 * Time.deltaTime);
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (isAttack == false)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
