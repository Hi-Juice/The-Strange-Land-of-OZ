using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLion : MonoBehaviour
{
    int dirFlag = -1; // 어느 방향으로 이동할 것인지
    Animator pos_anim;
    public Animator sprite_anim;
    public float moveSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        pos_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite_anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Lion_Baby_run"))
        {            
            if (dirFlag == 0)
            {
                if(pos_anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Lion_Baby_Idle_L"))
                {
                    pos_anim.enabled = false;
                }
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else if(dirFlag == 1)
            {
                if (pos_anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Lion_Baby_Idle_R"))
                {
                    pos_anim.enabled = false;
                }
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
        }     
    }

    public void SetFlag(int dir)
    {
        if(dir == 0)
        {
            dirFlag = 0;
        }
        else
        {
            dirFlag = 1;
        }

    }
}
