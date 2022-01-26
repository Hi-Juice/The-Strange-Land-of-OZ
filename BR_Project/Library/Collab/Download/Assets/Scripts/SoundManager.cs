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
    

    public AudioClip BGM_1; // Ÿ��Ʋ ����
    public AudioClip BGM_2; // ��ȭ ����
    public AudioClip BGM_3; // ��ŸƮ ����
    public AudioClip BGM_4; // ���� ���� ����
    public AudioClip BGM_5; // ���� ���� ����
    public AudioClip BGM_6; // ���� ����
    public AudioClip BGM_7; // ���� ����
    public AudioClip BGM_8; // ���ν� ����
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
            Play_StartDialogSound();
        }
        else if (SceneManager.GetActiveScene().name == "TinWoodman")
        {
            Play_StartDialogSound();
        }
        else if (SceneManager.GetActiveScene().name == "ScareCrow")
        {
            Play_StartDialogSound();
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


    #region �÷��̾� ���� �Ҹ�

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

    #region ���� ���� ���� �Ҹ�
    public void Play_LionPattern1Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionPattern2Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionPattern3Sound()
    {
        myAudio.PlayOneShot(test01);
    }
    public void Play_LionDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region ����ƺ� ���� ���� �Ҹ�


    public void Play_ScareCrowPatternCrowSound()
    {
        myAudio.PlayOneShot(StartCrow); // ���
    }
    public void Play_ScareCrowPatternForkSound()
    {
        int temp = 0;
        temp = Random.Range(0, 3);
        if(temp == 0)
        {
            myAudio.PlayOneShot(Fork_1); // ��ũ��
        }
        else if(temp == 1)
        {
            myAudio.PlayOneShot(Fork_2); // ��ũ��
        }
        else if(temp == 2)
        {
            myAudio.PlayOneShot(Fork_3); // ��ũ��
        }
    }
    public void Play_ScareCrowPattern3Sound()
    {
        myAudio.PlayOneShot(test01); // ¤ ������
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

    #region ��ö������ ���� ���� �Ҹ�
    public void Play_TinWoodManPattern1Sound()
    {
        myAudio.PlayOneShot(test01); // ��� ����߸���
    }
    public void Play_TinWoodManPattern2Sound()
    {
        myAudio.PlayOneShot(test01); // ���� ����
    }
    public void Play_TinWoodManPattern3Sound()
    {
        myAudio.PlayOneShot(test01); // ��Ʈ ����
    }
    public void Play_TinWoodManDieSound()
    {
        myAudio.PlayOneShot(test01);
    }
    #endregion

    #region �ý��� ���� �Ҹ�
    public void Play_MenuChoose()
    {
        myAudio.PlayOneShot(test01); // �޴� ����
    }
    public void Play_PlayerGameOver()
    {
        myAudio.PlayOneShot(test01); // ���� ����
    }
    public void Play_Clear()
    {
        myAudio.PlayOneShot(test01); // Ŭ����
    }
    #endregion

    #region �������


    public void Play_BGM1()
    {
        myAudio.PlayOneShot(BGM_1); // �������1
    }
    public void Play_BGM2()
    {
        myAudio.PlayOneShot(test01); // �������2
    }
    public void Play_BGM3()
    {
        myAudio.PlayOneShot(test01); // �������3
    }
    #endregion

}
