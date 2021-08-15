using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Sprite heart_black;
    public Sprite heart_purple;

    public Image[] image_hpImgs;
    int hp = 5; // 목숨은 항상 다섯개

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            SetHpVal(-1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            SetHpVal(-1);
        }
    }

    int imgIdx = 0;
    void SetHpVal(int num)
    {
        if(hp > 0)
        {
            hp += num;
            image_hpImgs[imgIdx].sprite = heart_black; // 검정색 하트로 바꿈
            imgIdx++;
        }
    }
}
