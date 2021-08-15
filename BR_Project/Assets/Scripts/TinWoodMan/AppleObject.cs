using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleObject : MonoBehaviour
{
    float throwPower_up = 50f;
    float throwPower_side = 50f;

    public bool isAttack = false;
    public CircleCollider2D Apple_Collider;

    public bool end = false;
    void Start()
    {
        Destroy(this.gameObject, 5f);
        transform.parent = null;
        Apple_Collider = GetComponent<CircleCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 200, Space.Self);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (isAttack == false)
            {
                Destroy(this.gameObject);
            }
            if (collision.CompareTag("Player"))
            {
                 Apple_Collider.enabled = false;
            }
        }
    }
}
