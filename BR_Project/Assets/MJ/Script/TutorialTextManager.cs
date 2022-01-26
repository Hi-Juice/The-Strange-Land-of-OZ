using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextManager : MonoBehaviour
{
    [TextArea]
    public string[] storyTextData;
    public int storyTextIndex;

    public int TextLen;
    public string ShowText;
    public Text storyText;
    public float delay = 0.2f;

    public int now_Sentence = 0;
    private bool isDialog = false;

    public GameObject dialogPanel;
    public GameObject getMagicEffect;
    public GameObject magicStaff;

    public GameObject example_Target;
    public GameObject spaceBar;
    public GameObject jumpFloor;
    public GameObject xKey;
    public GameObject zKey;
    PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        storyTextIndex = storyTextData.Length;
        dialogPanel.SetActive(false);
        magicStaff.SetActive(false);
        example_Target.SetActive(false);
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        spaceBar.SetActive(false);
        jumpFloor.SetActive(false);
        xKey.SetActive(false);
        zKey.SetActive(false);
        
    }
    public int attack_StoryMsgIdx;
    public int jump_StoryMsgIdx;
    bool isCompleted_Opening = false;

    bool isFirst = true;


    // Update is called once per frame
    void Update()
    {
        if (isCompleted_Opening)
        {
            if (isFirst)
            {
                isFirst = false;
                NextSentence();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (now_Sentence < storyTextIndex)
                {
                    if (isDialog == false)
                    {

                        if (!isDelay)
                        {
                            NextSentence();
                        }
                        /* 1. 공격 튜토리얼 */
                        if (now_Sentence == attack_StoryMsgIdx)
                        {
                            Tutorial_Attack();
                        }
                        /* 2. 점프 튜토리얼 */
                        if (now_Sentence == jump_StoryMsgIdx)
                        {
                            Tutorial_Jump();
                        }
                    }
                }
                else
                {
                    GameManager.Instance.MoveChooseBoss();
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                SetBool_isDelay(false);
            }
        }
        
    }

    bool isDelay = false;
    void Tutorial_Attack()
    {
        magicStaff.SetActive(true);
        getMagicEffect.GetComponent<ParticleSystem>().Play();
        example_Target.SetActive(true);
        playerAttack.isStart = true;
        xKey.SetActive(true);
        SetBool_isDelay(true);
        spaceBar.SetActive(false);
        Debug.Log("스페이스바 없앰");
    }

    void Tutorial_Jump()
    {
        jumpFloor.SetActive(true);
        zKey.SetActive(true);
        SetBool_isDelay(true);
        spaceBar.SetActive(false);
    }

    public void SetBool_isDelay(bool sig)
    {
        if(sig)
        {
            isDelay = true;
        }
        else
        {
            xKey.SetActive(false);
            zKey.SetActive(false);
            spaceBar.SetActive(true);
            isDelay = false;
        }
    }

    void NextSentence()
    {
        spaceBar.SetActive(false);
        dialogPanel.SetActive(true);
        if (dialogPanel.GetComponent<Animator>() != null)
        {
            dialogPanel.GetComponent<Animator>().SetTrigger("isPang_Doroci");
            
        }
        ShowText = "";
        TextLen = storyTextData[now_Sentence].Length;
        StartCoroutine(NextSentence_Play());

    }


    IEnumerator NextSentence_Play()
    {
        isDialog = true;
        int temp = 0;
        SoundManager.Instance.Play_DialogTapSound();
        while (temp < TextLen)
        {
            
            ShowText += storyTextData[now_Sentence][temp];
            temp++;

            storyText.text = ShowText;
            yield return new WaitForSeconds(delay);
        }

        now_Sentence++;
        isDialog = false;
        if(now_Sentence-1 != attack_StoryMsgIdx && now_Sentence-1 != jump_StoryMsgIdx)
        {
            spaceBar.SetActive(true);
        }
        
        Debug.Log("스페이스바 보이게함");
    }

    public void SetBool_isCompleted_Opening(bool sig)
    {
        if (sig)
        {
            isCompleted_Opening = true;
        }
        else
        {
            isCompleted_Opening = false;
        }
    }

}
