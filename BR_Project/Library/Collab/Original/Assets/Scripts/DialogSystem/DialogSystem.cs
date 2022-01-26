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
        TextPanel.SetActive(false);
        BossTextIndex = BossTextData.Length;
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
                    NextSentence();
                    TextPanel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isPang");
                }
            }
            
            else
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
                if (isStart == false)
                {
                    SoundManager.Instance.Play_StartCountSound();
                    StartCoroutine(WaitAttack());
                    StartCoroutine(CountDown());
                    isStart = true;
                    ShowText = "";
                }
                
            }

            

        }
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(8f);
        playerAttack.isStart = true;
    }
    IEnumerator CountDown()
    {
        int temp_count = 1;

        yield return new WaitForSeconds(6f);
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
        ShowText = "";
        TextLen = BossTextData[now_Sentence].Length;
        StartCoroutine(NextSentence_Play());
        
    }


    IEnumerator NextSentence_Play()
    {
        isDialog = true;
        int temp = 0;
        while(temp < TextLen)
        {
            ShowText += BossTextData[now_Sentence][temp];
            temp++;

            BossText.text = ShowText;
            yield return new WaitForSeconds(delay);
        }
        now_Sentence++;
        isDialog = false;
    }

}
