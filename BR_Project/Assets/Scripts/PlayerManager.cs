using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Sprite heart_black;
    public Sprite heart_purple;

    public Image[] image_hpImgs;
    int hp = 4; // 목숨은 항상 다섯개

    private CameraEffectManager ceManager;
    public bool isHit = false;
    private void Start()
    {
        ceManager = GameObject.FindObjectOfType<CameraEffectManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Monster")
        {
            SetHpVal(-1);
        }
    }


    int imgIdx = 0;

    void SetHpVal(int num)
    {
        if(isHit == false)
        {
            SoundManager.Instance.Play_PlayerHitSound();
            isHit = true;
            StartCoroutine(HitDelay());
            if (hp > 0)
            {
                hp += num;
                image_hpImgs[imgIdx].sprite = heart_black; // 검정색 하트로 바꿈
                imgIdx++;
            }
            else
            {
                GameManager.Instance.PlayerDie();
                ceManager.SetGrayScaleEffect();
                Time.timeScale = 0;
            }
        }
        
    }




    IEnumerator HitDelay()
    {
        
        yield return new WaitForSeconds(1f);
        isHit = false;
    }
}
