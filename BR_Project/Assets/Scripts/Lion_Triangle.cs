using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion_Triangle : MonoBehaviour
{
    public float moveSpeed;
    public float stopSeconds;
    Animator triAnim;
    bool canMove = true;

    private void Start()
    {
        triAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(moveSpeed, 0, 0);
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

    IEnumerator KeepCurrentPosition()
    {
        canMove = false;
        triAnim.speed = 0;
        yield return new WaitForSeconds(stopSeconds);
        canMove = true;
        triAnim.speed = 1;
    }
}
