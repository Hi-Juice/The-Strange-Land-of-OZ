using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public GameObject HpUI;
    public GameObject DieUI;

    private bool isTitleScene = false;
    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            isTitleScene = true;
        }
    }

    

    private void Update()
    {
        if(isTitleScene == true && Input.GetKeyDown(KeyCode.Space))
        {
            MoveStartStory();
        }

        if(Input.GetKey(KeyCode.F1))
        {
            PlayerPrefs.SetInt("TinWoodClear", 1);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            PlayerPrefs.SetInt("LionClear", 1);
        }
        if (Input.GetKey(KeyCode.F3))
        {
            PlayerPrefs.SetInt("ScareCrowClear", 1);
        }
    }

    public void Move(int value)
    {
        switch(value)
        {
            case 0:
                MoveScareCrow();
                break;

            case 1:
                MoveTinWoodMan();
                break;

            case 2:
                MoveLion();
                break;

            default:
                break;

        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MoveMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MoveStartStory()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void MoveTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void MoveChooseBoss()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }
    
    public void MoveLion()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
    public void MoveTinWoodMan()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
    }
    public void MoveScareCrow()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
    }
    public void MoveEndingStory()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(7);
    }

    public void PlayerDie()
    {
        DieUI.SetActive(true);
        HpUI.SetActive(false);
    }

}
