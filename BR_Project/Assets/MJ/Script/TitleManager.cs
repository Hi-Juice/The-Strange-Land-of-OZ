using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject panel_howToPlay;

    private void Start()
    {
        PlayerPrefs.SetInt("TinWoodClear", 0);
        PlayerPrefs.SetInt("LionClear", 0);
        PlayerPrefs.SetInt("ScareCrowClear", 0);
        panel_howToPlay.SetActive(false);
    }

    public void OnClick_HowToPlay()
    {
        panel_howToPlay.SetActive(true);
    }

    public void OnClick_exit()
    {
        panel_howToPlay.SetActive(false);
    }

    public void OnClick_quit()
    {
        Application.Quit();
    }
}
