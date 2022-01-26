using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion_Triangle : MonoBehaviour
{
    public float moveSpeed;
    public float stopSeconds;
    Animator triAnim;
    bool canMove = true;

    public void Start()
    {
        triAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(moveSpeed*Time.deltaTime, 0, 0);
        }
        if(transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }
    }

    public void StopMove()
    {
        StartCoroutine(KeepCurrentPosition());
    }

    public IEnumerator KeepCurrentPosition()
    {
        if(triAnim == null)
        {
            triAnim = GetComponent<Animator>();
        }
        canMove = false;
        triAnim.speed = 0;
        yield return new WaitForSeconds(stopSeconds);
        canMove = true;
        triAnim.speed = 1;
        //triAnim.speed = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
