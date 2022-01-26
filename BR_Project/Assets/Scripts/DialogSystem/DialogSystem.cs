using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    private static DialogSystem instance;
    public static DialogSystem Instance { get { return instance; } }


    [TextArea]
    public string[] BossTextData;
    [TextArea]
    public string[] EndBossTextData;
    public int BossTextIndex;

    public int TextLen;
    public string ShowText;
    public Text BossText;
    public float delay = 0.2f;

    public int now_Sentence = 0;
    public bool isStart = false;

    public bool isEnd = false;

    private bool isDialog = false;

    Boss_Lion boss_Lion = null;
    ScareCrow boss_ScareCrow = null;
    TinWoodman boss_TinWoodMan = null;
    public GameObject enemy;
    public PlayerAttack playerAttack;

    public GameObject TextPanel;


    public GameObject CountPanel;
    public Text CountText;
    public Slider CountSlider;
    public bool isSliderMove = false;

    public GameObject spaceBar;
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

        if (enemy.tag == "TinWood")
        {
            boss_TinWoodMan = enemy.GetComponent<TinWoodman>();
        }
        else if (enemy.tag == "ScareCrow")
        {
            boss_ScareCrow = enemy.GetComponent<ScareCrow>();
        }
        else if (enemy.tag == "Boss")
        {
            boss_Lion = enemy.GetComponentInParent<Boss_Lion>();
        }

        spaceBar.SetActive(false);
        BossTextIndex = BossTextData.Length;
        NextSentence();
    }

    
    void Update()
    {
        if(isSliderMove == true)
        {
            CountSlider.value = Mathf.Lerp(CountSlider.value, 1, 10 * Time.deltaTime);
        }
        else
        {
            CountSlider.value = Mathf.Lerp(CountSlider.value, 0, 10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if(isStart == false)
            {
                TextPanel.SetActive(true);
            }
            
            if (now_Sentence < BossTextIndex)
            {
                if(isDialog == false)
                {
                    SoundManager.Instance.Play_DialogTapSound();
                    NextSentence();
                    
                }
            }
            
            else
            {
                spaceBar.SetActive(false);
                if(isEnd == false)
                {
                    if (boss_TinWoodMan != null)
                    {
                        boss_TinWoodMan.StartGame();
                    }
                    else if (boss_ScareCrow != null)
                    {
                        boss_ScareCrow.StartGame();
                    }
                    else if (boss_Lion != null)
                    {
                        boss_Lion.StartGame();
                    }
                }
                else
                {
                    
                    if(PlayerPrefs.GetInt("TinWoodClear") == 1 && PlayerPrefs.GetInt("LionClear") == 1 && PlayerPrefs.GetInt("ScareCrowClear") == 1)
                    {
                        GameManager.Instance.MoveEndingStory();
                    }
                    else
                    {
                        GameManager.Instance.MoveChooseBoss();
                    }
                    
                }
                
                if (isStart == false)
                {
                    
                    StartCoroutine(WaitAttack());
                    StartCoroutine(CountDown());
                    isStart = true;
                    ShowText = "";
                }   
            }
        }
    }

    public void BossClear()
    {
        isEnd = true;
        ShowText = "[space]를 눌러 진행";
        BossText.text = "[space]를 눌러 진행";
        BossTextIndex = EndBossTextData.Length;
        now_Sentence = 0;
        TextPanel.SetActive(true);
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(3f);
        playerAttack.isStart = true;
    }
    IEnumerator CountDown()
    {
        int temp_count = 1;

        
        TextPanel.SetActive(false);
        CountPanel.SetActive(true);

        CountText.text = temp_count.ToString();
        isSliderMove = true;
        //CountSlider.value = Mathf.Lerp(0, 1, 1);
        
        
        yield return new WaitForSeconds(0.7f);
        temp_count++;
        CountText.text = temp_count.ToString();
        isSliderMove = false;
        //CountSlider.value = Mathf.Lerp(0, 1, 1);


        yield return new WaitForSeconds(0.9f);
        temp_count++;
        CountText.text = temp_count.ToString();
        isSliderMove = true;

        yield return new WaitForSeconds(1f);
        CountPanel.SetActive(false);
       
    }

    void NextSentence()
    {
        TextPanel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isPang");
        spaceBar.SetActive(false);
        if (isEnd == false)
        {
            ShowText = "";
            TextLen = BossTextData[now_Sentence].Length;
            StartCoroutine(NextSentence_Play());
        }
        else
        {
            ShowText = "";
            TextLen = EndBossTextData[now_Sentence].Length;
            StartCoroutine(NextSentence_Play());
        }
        
        
    }


    IEnumerator NextSentence_Play()
    {
        isDialog = true;
        int temp = 0;
        
        while (temp < TextLen)
        {
            
            if(isEnd == false)
            {
                ShowText += BossTextData[now_Sentence][temp];
                temp++;

                BossText.text = ShowText;
                yield return new WaitForSeconds(delay);
            }
            else
            {
                ShowText += EndBossTextData[now_Sentence][temp];
                temp++;

                BossText.text = ShowText;
                yield return new WaitForSeconds(delay);
            }
        }
        now_Sentence++;
        isDialog = false;
        spaceBar.SetActive(true);
    }

}
