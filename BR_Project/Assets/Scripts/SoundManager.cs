using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    
    
    AudioSource myAudio;
    AudioSource BgmAudio;
    
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


    public AudioClip Axe;
    public AudioClip Oil;
    public AudioClip Heart;

    public AudioClip MeowPunch;
    public AudioClip Crown;
    public AudioClip TreeDie1;
    public AudioClip TreeDie2;
    public AudioClip TreeDie3;
    public AudioClip TreeDie4;
    public AudioClip SmallLion_1;
    public AudioClip SmallLion_2;
    public AudioClip LionYell;
    public AudioClip TreeHit;

    public AudioClip DialogTap;
    public AudioClip DialogSay;
    public AudioClip BGM_1; // 타이틀 음악
    public AudioClip BGM_2; // 사자
    public AudioClip BGM_3; // 양철나무꾼
    public AudioClip BGM_4; // 허수아비
    public AudioClip BGM_5; // 보스 선택 음악
    public AudioClip BGM_6; // 엔딩 음악
    public AudioClip BGM_7; // 시작 음악
    public AudioClip BGM_8; // 도로시 음악
    public bool isPlay = false;

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
        BgmAudio = transform.GetChild(0).GetComponent<AudioSource>();
        BgmAudio.loop = true;
        if(SceneManager.GetActiveScene().name == "Title")
        {
            BgmAudio.clip = BGM_1;
            BgmAudio.Play();
        }
        else if(SceneManager.GetActiveScene().name == "MoveScene")
        {
            BgmAudio.clip = BGM_5;
            BgmAudio.Play();
        }
        else if(SceneManager.GetActiveScene().name == "Lion")
        {
            //Play_StartDialogSound();
            BgmAudio.clip = BGM_2;
            BgmAudio.Play();
        }
        else if (SceneManager.GetActiveScene().name == "TinWoodman")
        {
            //Play_StartDialogSound();
            BgmAudio.clip = BGM_3;
            BgmAudio.Play();
        }
        else if (SceneManager.GetActiveScene().name == "ScareCrow")
        {
            //Play_StartDialogSound();
            BgmAudio.clip = BGM_4;
            BgmAudio.Play();
        }
        else if (SceneManager.GetActiveScene().name == "EndingStory")
        {
            BgmAudio.clip = BGM_6;
            BgmAudio.Play();
        }
        else if (SceneManager.GetActiveScene().name == "StartStory")
        {
            BgmAudio.clip = BGM_7;
            BgmAudio.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            BgmAudio.clip = BGM_8;
            BgmAudio.Play();
        }
    }

    private void Update()
    {
        if(BgmAudio.isPlaying == false)
        {
            isPlay = false;
        }
        else
        {
            isPlay = true;
        }
    }



    /*
    public void Play_StartDialogSound()
    {
        BgmAudio.clip = BGM_2;
        BgmAudio.Play();
    }
    public void Play_StartCountSound()
    {
        BgmAudio.clip = BGM_3;
        BgmAudio.loop = false;
        BgmAudio.Play();

        StartCoroutine(StartCount());
        
    }
    

    IEnumerator StartCount()
    {

        yield return new WaitForSeconds(3f);
        Play_StartMainBossBgm();
    }


    public void Play_StartMainBossBgm()
    {
        BgmAudio.clip = BGM_4;
        BgmAudio.loop = true;
        BgmAudio.Play();
    }
    */

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
    public void Play_LionCrownSound()
    {
        myAudio.PlayOneShot(Crown);
    }
    public void Play_LionMeowSound()
    {
        myAudio.PlayOneShot(MeowPunch);
    }
    public void Play_LionTreeSound()
    {
        myAudio.PlayOneShot(LionYell);
    }
    public void Play_LionDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_SmallLionSound()
    {
        int temp = Random.Range(0, 2);
        if(temp == 0)
        {
            myAudio.PlayOneShot(SmallLion_1);
        }
        else
        {
            myAudio.PlayOneShot(SmallLion_2);
        }
    }

    public void Play_TreeDidSound()
    {
        int temp = Random.Range(0, 4);
        if (temp == 0)
        {
            myAudio.PlayOneShot(TreeDie1);
        }
        else if(temp == 1)
        {
            myAudio.PlayOneShot(TreeDie2);
        }
        else if(temp ==2)
        {
            myAudio.PlayOneShot(TreeDie3);
        }
        else
        {
            myAudio.PlayOneShot(TreeDie4);
        }
        
    }
    public void Play_TreeHitSound()
    {
        myAudio.PlayOneShot(TreeHit);
    }

    #endregion

    #region 허수아비 보스 관련 소리


    public void Play_ScareCrowPatternCrowSound()
    {
        myAudio.PlayOneShot(StartCrow); // 까마귀
    }
    public void Play_ScareCrowPatternForkSound()
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
    public void Play_ScareCrowPattern3Sound()
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
    public void Play_TinWoodManAxeSound()
    {
        myAudio.PlayOneShot(Axe); // 사과 떨어뜨리기
    }
    public void Play_TinWoodManOilSound()
    {
        myAudio.PlayOneShot(Oil); // 오일 범람
    }
    public void Play_TinWoodManHeartSound()
    {
        myAudio.PlayOneShot(Heart); // 하트 공격
    }
    public void Play_TinWoodManDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region 시스템 관련 소리

    public void Play_DialogTapSound()
    {
        myAudio.PlayOneShot(DialogTap);
    }
    public void Play_DialogSaySound()
    {
        myAudio.PlayOneShot(DialogSay);
    }

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

    

}
