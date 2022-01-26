using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningText : MonoBehaviour
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

    public Animator Opening_Set_Anim;
    private bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        storyTextIndex = storyTextData.Length;
    }

    float time;
    public float delayTime_betweenMent = 1f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > delayTime_betweenMent)
        {
            if (isEnd == false)
            {
                if (now_Sentence < storyTextIndex)
                {
                    if (isDialog == false)
                    {
                        NextSentence();
                    }
                }
                else
                {
                    isEnd = true;
                }
            }
            else
            {
                Opening_Set_Anim.SetTrigger("IsFadeOut");
                GameObject.FindObjectOfType<TutorialTextManager>().SetBool_isCompleted_Opening(true);
            }

        }
       
    }

    void NextSentence()
    {
        ShowText = "";
        TextLen = storyTextData[now_Sentence].Length;
        StartCoroutine(NextSentence_Play());

    }

    IEnumerator NextSentence_Play()
    {
        isDialog = true;
        int temp = 0;
        while (temp < TextLen)
        {
            SoundManager.Instance.Play_DialogSaySound();
            ShowText += storyTextData[now_Sentence][temp];
            temp++;

            storyText.text = ShowText;
            yield return new WaitForSeconds(delay);
        }
        now_Sentence++;
        isDialog = false;
        time = 0;
    }

}
