using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Bomb : MonoBehaviour
{
    public enum BombState
    {
        Move,
        Explode
    }

    BombState bState;
    Animator bAnim;
    public float moveSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        bState = BombState.Move;
        bAnim = GetComponent<Animator>();
    }

    float initialYval;
    // Update is called once per frame
    void Update()
    {
        switch(bState)
        {
            case BombState.Move:
                Move();
                break;
            case BombState.Explode:
                Explode();
                break;
        }
    }

    void Move()
    {
        initialYval = transform.position.y;
        transform.position += new Vector3(moveSpeed, 0, 0);
        float transiTime = Random.Range(3f, 7f);
        Invoke("GoExplodeState", transiTime);
    }

    void GoExplodeState()
    {
        bState = BombState.Explode;
    }

    void Explode()
    {
        bAnim.SetTrigger("Trig_Explode");
        Invoke("DestroyBomb", 1.0f);
    }

    void DestroyBomb()
    {
        Destroy(this.gameObject);
    }

    
    void Jump()
    {
        if(transform.position.y < 0)
        {
            transform.position += new Vector3(0, 1, 0);
        }
        //transform.Translate(this.transform.position.x, transform.position.y + 2, transform.position.z);
    }

    void BackToGround()
    {
        transform.Translate(this.transform.position.x, initialYval, transform.position.z);
    }

}
