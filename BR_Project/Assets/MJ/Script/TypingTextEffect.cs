using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TypingTextEffect : MonoBehaviour
{
 
    [TextArea]
    public string[] storyTextData;
    public int storyTextIndex;


    public Sprite[] cutScene;

    public int TextLen;
    public string ShowText;
    public Text storyText;
    public float delay = 0.2f;

    public GameObject cutScene_Object;

    public int now_Sentence = 0;
    private bool isDialog = false;

    private bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        storyTextIndex = storyTextData.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isEnd == false)
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
                    StartCoroutine(EndWait());
                }
            }
            
        }
    }


    IEnumerator EndWait()
    {

        yield return new WaitForSeconds(5f);

        if (SceneManager.GetActiveScene().name == "StartStory")
            GameManager.Instance.MoveTutorial();
        if (SceneManager.GetActiveScene().name == "EndingStory")
            GameManager.Instance.MoveMainMenu();
    }

    void NextSentence()
    {
        ShowText = "";
        TextLen = storyTextData[now_Sentence].Length;
        cutScene_Object.GetComponent<Image>().sprite = cutScene[now_Sentence];
        StartCoroutine(NextSentence_Play());
        
    }

    IEnumerator NextSentence_Play()
    {
        isDialog = true;
        int temp = 0;
        while (temp < TextLen)
        {
            //SoundManager.Instance.Play_DialogTapSound();
            ShowText += storyTextData[now_Sentence][temp];
            temp++;

            storyText.text = ShowText;
            yield return new WaitForSeconds(delay);
        }
        now_Sentence++;
        isDialog = false;
    }

}
