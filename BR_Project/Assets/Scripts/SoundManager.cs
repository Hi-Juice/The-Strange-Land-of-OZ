using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    AudioSource myAudio;
    public AudioClip test01;
    public AudioClip WalkSound;
    public AudioClip PlayerHit;
    public AudioClip PlayerAttack;
    public AudioClip PlayerDie;
    public AudioClip PlayerJump;

    public AudioClip StartCrow;
    public AudioClip ScareCrowHit;
    public AudioClip Fork_1;
    public AudioClip Fork_2;
    public AudioClip Fork_3;
    public AudioClip BGM_1;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayOneShot(BGM_1);
    }

    #region 플레이어 관련 소리

    public void Play_PlayerHitSound()
    {
        myAudio.PlayOneShot(PlayerHit);
    }
    public void Play_PlayerAttackSound()
    {
        myAudio.PlayOneShot(PlayerAttack);
    }

    public void Play_PlayerJumpSound()
    {
        myAudio.PlayOneShot(PlayerJump);
    }
    #endregion

    #region 사자 보스 관련 소리
    public void Play_LionPatten1Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionPatten2Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionPatten3Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region 허수아비 보스 관련 소리


    public void Play_ScareCrowPattenCrowSound()
    {
        myAudio.PlayOneShot(StartCrow); // 까마귀
    }
    public void Play_ScareCrowPattenForkSound()
    {
        int temp = 0;
        temp = Random.Range(0, 3);
        if(temp == 0)
        {
            myAudio.PlayOneShot(Fork_1); // 포크질
        }
        else if(temp == 1)
        {
            myAudio.PlayOneShot(Fork_2); // 포크질
        }
        else if(temp == 2)
        {
            myAudio.PlayOneShot(Fork_3); // 포크질
        }
    }
    public void Play_ScareCrowPatten3Sound()
    {
        myAudio.PlayOneShot(test01); // 짚 던지기
    }
    public void Play_ScareCrowHitSound()
    {
        myAudio.PlayOneShot(ScareCrowHit);
    }
    public void Play_ScareCrowDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region 양철나무꾼 보스 관련 소리
    public void Play_TinWoodManPatten1Sound()
    {
        myAudio.PlayOneShot(test01); // 사과 떨어뜨리기
    }
    public void Play_TinWoodManPatten2Sound()
    {
        myAudio.PlayOneShot(test01); // 오일 범람
    }
    public void Play_TinWoodManPatten3Sound()
    {
        myAudio.PlayOneShot(test01); // 하트 공격
    }
    public void Play_TinWoodManDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region 시스템 관련 소리
    public void Play_MenuChoose()
    {
        myAudio.PlayOneShot(test01); // 메뉴 선택
    }
    public void Play_PlayerGameOver()
    {
        myAudio.PlayOneShot(test01); // 게임 오버
    }
    public void Play_Clear()
    {
        myAudio.PlayOneShot(test01); // 클리어
    }
    #endregion

    #region 배경음악


    public void Play_BGM1()
    {
        myAudio.PlayOneShot(BGM_1); // 배경음악1
    }
    public void Play_BGM2()
    {
        myAudio.PlayOneShot(test01); // 배경음악2
    }
    public void Play_BGM3()
    {
        myAudio.PlayOneShot(test01); // 배경음악3
    }
    #endregion

}
